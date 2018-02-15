using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{

    public interface IDBTable: IDBSource, IDBTableInfo, IDisposable
    {
        bool IsConnected { get; }
        new IDBTableProvider DataProvider { get; }
        new uint Version { get; set; }
        void Create(int szDatum);
        void Clear();
        void Flush();
        void Connect();
        void Disconnect();
    }



    public abstract partial class DBTable<T>: IDBTable
        where T : IDataRow, new()
    {
        const int NULL_INDEX = -1;

        readonly object m_rwLock = new object();
        readonly List<int> m_recycledData = new List<int>();
        Header m_header;
        FileStream m_dataFile;
        byte[] m_buffer;
        TableReader m_reader;
        TableWriter m_writer;


        public DBTable(string filePath , string name , uint id)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));

            FilePath = filePath;
            Name = name;
            ID = id;
        }


        public uint ID { get; }
        public string Name { get; }
        public string FilePath { get; }
        public IDBTableProvider DataProvider => new RowProvider(this);

        public DateTime CreationTime
        {
            get
            {
                Assert(IsConnected);

                Monitor.Enter(m_rwLock);
                DateTime ct = m_header.CreationTime;
                Monitor.Exit(m_rwLock);

                return ct;
            }
        }

        public DateTime LasttWriteTime
        {
            get
            {
                Assert(IsConnected);

                Monitor.Enter(m_rwLock);
                DateTime lwt = m_header.LastWriteTime;
                Monitor.Exit(m_rwLock);

                return lwt;
            }
        }

        public uint Version
        {
            get
            {
                Assert(IsConnected);


                Monitor.Enter(m_rwLock);
                uint ver = m_header.Version;
                Monitor.Exit(m_rwLock);

                return ver;
            }

            set
            {
                Assert(IsConnected);

                Monitor.Enter(m_rwLock);
                m_header.Version = value;
                Monitor.Exit(m_rwLock);
            }
        }

        public int DatumSize
        {
            get
            {
                Assert(IsConnected);

                Monitor.Enter(m_rwLock);
                int szFrame = m_header.FrameSize;
                Monitor.Exit(m_rwLock); ;

                return szFrame;
            }
        }

        public bool IsConnected
        {
            get
            {
                Monitor.Enter(m_rwLock);
                bool connected = m_dataFile != null;
                Monitor.Exit(m_rwLock);

                return connected;
            }
        }

        public int RowCount
        {
            get
            {
                Assert(IsConnected);

                Monitor.Enter(m_rwLock);
                int count = m_header.FrameCount - m_recycledData.Count;
                Monitor.Exit(m_rwLock);

                return count;
            }
        }

        public void Create(int szDatum)
        {
            Assert(!IsConnected);
            Assert(szDatum > 0);

            lock (m_rwLock)
            {
                const int BUFFER_SIZE = 4096;

                szDatum = Math.Max(szDatum , sizeof(long));

                EventLogger.Debug($"Création de la table {Path.GetFileName(Name)}");

                m_dataFile = new FileStream(FilePath ,
                    FileMode.CreateNew ,
                    FileAccess.ReadWrite ,
                    FileShare.Read ,
                    BUFFER_SIZE ,
                    FileOptions.RandomAccess);


                m_header = new Header(szDatum);
                var writer = new TableWriter(m_dataFile);
                m_header.Store(writer);
                m_buffer = new byte[szDatum];

                var ms = new MemoryStream(m_buffer , true);
                m_reader = new TableReader(ms);
                m_writer = new TableWriter(ms);
            }
        }

        public void Connect()
        {
            lock (m_rwLock)
            {
                Open();
            }
        }

        public void Disconnect()
        {
            lock (m_rwLock)
            {
                if (m_dataFile == null)
                    return;

                Flush();

                m_dataFile.Dispose();
                m_recycledData.Clear();

                m_dataFile = null;
                DataDeleted = null;
                DataInserted = null;
                DatumDeleted = null;
                DatumInserted = DatumReplaced = null;
                SourceCleared = null;
            }
        }

        public void Flush()
        {
            Assert(IsConnected);

            lock (m_rwLock)
            {
                if (m_header.IsDirty)
                {
                    var bw = new TableWriter(m_dataFile);
                    m_dataFile.Position = 0;
                    m_header.Store(bw);
                }

                m_dataFile.Flush();
            }
        }

        public void Dispose() => Disconnect();

        public void Insert(T datum)
        {
            Assert(IsConnected);
            Assert(datum != null);


            lock (m_rwLock)
            {
                int ndxFrame = m_header.FirstDeletedFrameIndex == NULL_INDEX ?
                      m_header.FrameCount :
                      m_header.FirstDeletedFrameIndex;

                if (m_recycledData.Count > 0)
                {
                    m_recycledData.RemoveAt(m_recycledData.BinarySearch(ndxFrame));

                    long framePos = GetFramePosition(ndxFrame);

                    if (m_recycledData.Count > 0)
                    {
                        ReadFrame(framePos);
                        m_reader.Position = 0;
                        m_header.FirstDeletedFrameIndex = m_reader.ReadInt();
                    }
                    else
                        m_header.FirstDeletedFrameIndex = NULL_INDEX;

                    m_writer.Position = 0;
                    datum.Write(m_writer);
                    WriteFrame(framePos);
                }
                else
                {
                    m_writer.Position = 0;
                    datum.Write(m_writer);

                    long framePos = GetFramePosition(ndxFrame);
                    WriteFrame(framePos);

                    ++m_header.FrameCount;
                }

                m_header.LastWriteTime = DateTime.Now;
                int ndxRow = FrameIndexToRowIndex(ndxFrame);
                DatumInserted?.Invoke(ndxRow , datum);
            }
        }

        public void Insert(T[] data)
        {
            Assert(IsConnected);
            Assert(data != null);
            Assert(data.Count(d => d == null) == 0);


            var rowIndices = new int[data.Length];

            lock (m_rwLock)
            {
                for (int i = 0; i < data.Length; ++i)
                {
                    int ndxFrame = m_header.FirstDeletedFrameIndex == NULL_INDEX ?
                        m_header.FrameCount : m_header.FirstDeletedFrameIndex;


                    if (m_header.FirstDeletedFrameIndex == NULL_INDEX)
                    {
                        m_writer.Position = 0;
                        data[i].Write(m_writer);

                        long framePos = GetFramePosition(ndxFrame);
                        WriteFrame(framePos);
                        ++m_header.FrameCount;
                    }
                    else
                    {
                        int pos = m_recycledData.BinarySearch(ndxFrame);
                        m_recycledData.RemoveAt(pos);

                        long framePos = GetFramePosition(ndxFrame);

                        if (m_recycledData.Count > 0)
                        {
                            ReadFrame(framePos);
                            m_reader.Position = 0;
                            m_header.FirstDeletedFrameIndex = m_reader.ReadInt();
                        }
                        else
                            m_header.FirstDeletedFrameIndex = NULL_INDEX;

                        m_writer.Position = 0;
                        data[i].Write(m_writer);
                        WriteFrame(framePos);
                    }

                    int ndxNew = FrameIndexToRowIndex(ndxFrame);

                    for (int j = 0; j < i; ++j)
                        if (ndxNew <= rowIndices[j])
                            ++rowIndices[j];

                    rowIndices[i] = ndxNew;
                }

                m_header.LastWriteTime = DateTime.Now;
                DataInserted?.Invoke(rowIndices , data);
            }


        }

        public void Replace(int ndx , T datum)
        {
            Assert(IsConnected);
            Assert(ndx < RowCount);
            Assert(0 <= ndx);
            Assert(datum != null);

            lock (m_rwLock)
            {
                m_writer.Position = 0;
                datum.Write(m_writer);

                int ndxFrame = RowIndexToFrameIndex(ndx);
                long framePos = GetFramePosition(ndxFrame);
                WriteFrame(framePos);

                m_header.LastWriteTime = DateTime.Now;
                DatumReplaced?.Invoke(ndx , datum);
            }


        }

        public void Delete(int ndx)
        {
            Assert(IsConnected);
            Assert(ndx < RowCount);
            Assert(0 <= ndx);

            lock (m_rwLock)
            {
                int ndxFrame = RowIndexToFrameIndex(ndx);

                if (m_header.FirstDeletedFrameIndex != NULL_INDEX)
                {
                    m_writer.Position = 0;
                    m_writer.Write(m_header.FirstDeletedFrameIndex);

                    long framePos = GetFramePosition(ndxFrame);
                    WriteFrame(framePos);
                }

                m_header.FirstDeletedFrameIndex = ndxFrame;
                int pos = m_recycledData.BinarySearch(ndxFrame);

                Assert(pos < 0);
                m_recycledData.Insert(~pos , ndxFrame);

                m_header.LastWriteTime = DateTime.Now;
                DatumDeleted?.Invoke(ndx);
            }


        }

        public void Delete(int[] ndxData)
        {
            Assert(IsConnected);
            Assert(ndxData != null);
            Assert(ndxData.Count(x => x < 0 || RowCount <= x) == 0);

            var indices = ndxData.OrderByDescending(x => x).ToArray();

            lock (m_rwLock)
            {
                for (int i = 0; i < indices.Length; ++i)
                {
                    m_writer.Position = 0;
                    m_writer.Write(m_header.FirstDeletedFrameIndex);

                    int ndxFrame = RowIndexToFrameIndex(indices[i]);
                    long framePos = GetFramePosition(ndxFrame);
                    WriteFrame(framePos);
                    m_header.FirstDeletedFrameIndex = ndxFrame;

                    int loc = m_recycledData.BinarySearch(ndxFrame);

                    Assert(loc < 0);
                    m_recycledData.Insert(~loc , ndxFrame);
                }

                m_header.LastWriteTime = DateTime.Now;
                DataDeleted?.Invoke(indices);
            }


        }

        public void Clear()
        {
            Assert(IsConnected);

            lock (m_rwLock)
            {
                m_header.Reset();

                var writer = new TableWriter(m_dataFile);
                m_header.Store(writer);

                SourceCleared?.Invoke();                
            }

        }

        public T Get(int ndx)
        {
            Assert(IsConnected);
            Assert(ndx < RowCount);
            Assert(0 <= ndx);

            lock (m_rwLock)
            {
                int ndxFrame = RowIndexToFrameIndex(ndx);
                long framePos = GetFramePosition(ndxFrame);
                ReadFrame(framePos);

                m_reader.Position = 0;

                var datum = new T();
                datum.Read(m_reader);

                return datum;
            }
        }

        public T[] Get(int[] indices)
        {
            Assert(IsConnected);
            Assert(indices != null);
            Assert(indices.Any(x => 0 < x || RowCount <= x) == false);


            lock (m_rwLock)
            {
                var data = new T[indices.Length];

                for (int i = 0; i < indices.Length; ++i)
                {
                    int ndx = indices[i];
                    int ndxFrame = RowIndexToFrameIndex(ndx);
                    long framePos = GetFramePosition(ndxFrame);
                    ReadFrame(framePos);

                    m_reader.Position = 0;

                    var datum = new T();
                    datum.Read(m_reader);

                    data[i] = datum;
                }

                return data;
            }
        }
        

        //private:
        event Action<int[]> DataDeleted;
        event Action<int[] , T[]> DataInserted;
        event Action<int> DatumDeleted;
        event Action<int , T> DatumInserted;
        event Action<int , T> DatumReplaced;
        event Action SourceCleared;


        void Open()
        {
            const int BUFFER_SIZE = 4096;

            EventLogger.Debug($"Ouverture de la table {Name}.");

            m_dataFile = new FileStream(FilePath ,
             FileMode.Open ,
             FileAccess.ReadWrite ,
             FileShare.Read ,
             BUFFER_SIZE ,
             FileOptions.RandomAccess);

            var reader = new TableReader(m_dataFile);
            m_header = new Header();
            m_header.Load(reader);

            m_buffer = new byte[m_header.FrameSize];
            var ms = new MemoryStream(m_buffer , true);
            m_reader = new TableReader(ms);
            m_writer = new TableWriter(ms);

            Init(reader);
        }

        void Init(ITableReader reader)
        {
            int ndx = m_header.FirstDeletedFrameIndex;

            Assert(m_recycledData.Count == 0);

            while (ndx != NULL_INDEX)
            {
                m_recycledData.Add(ndx);
                reader.Position = GetFramePosition(ndx);
                ndx = reader.ReadInt();
            }

            m_recycledData.Sort();
        }

        long GetFramePosition(int ndxFrame) => m_header.DataOffset + ndxFrame * m_header.FrameSize;

        int FrameIndexToRowIndex(int ndxFrame)
        {
            int k = m_recycledData.Count;

            Assert(ndxFrame < m_header.FrameCount);

            if (k == 0 || ndxFrame < m_recycledData[0])
                return ndxFrame;

            if (m_recycledData[k - 1] < ndxFrame)
                return ndxFrame - k;

            int pos = m_recycledData.BinarySearch(ndxFrame);

            Assert(pos < 0);
            return ndxFrame - ~pos;
        }

        int RowIndexToFrameIndex(int ndxRow)
        {
            int k = m_recycledData.Count;

            if (k == 0 || ndxRow < m_recycledData[0])
                return ndxRow;

            if (ndxRow >= m_recycledData[k - 1] - k + 1)
                return ndxRow + k;

            int ndx = 1;

            while (m_recycledData[ndx] - ndx <= ndxRow)
                ++ndx;

            Assert(m_recycledData[ndx - 1] - ndx + 1 <= ndxRow);

            return ndxRow + ndx;
        }

        void ReadFrame(long position)
        {
            m_dataFile.Position = position;
            m_dataFile.Read(m_buffer , 0 , m_buffer.Length);
        }

        void WriteFrame(long position)
        {
            m_dataFile.Position = position;
            m_dataFile.Write(m_buffer , 0 , m_buffer.Length);
        }

        //explict impl.
        IDBProvider IDBSource.DataProvider => DataProvider;

    }
}
