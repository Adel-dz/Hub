using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Threading;
using System.Diagnostics;


namespace easyLib.DB
{
    public interface IDataTable: IDataSource, IDisposable
    {
        string FilePath { get; }
        DateTime LastAccessTime { get; }
        DateTime CreationTime { get; }
        DateTime LastWriteTime { get; }
        uint Version { get; }        
        bool IsDisposed { get; }

        void Connect();
    }



    public abstract partial class DataTable<T>: IDataTable
        where T : IStorable, IDatum
    {
        readonly string m_filePath;
        System.IO.FileStream m_dataFile;
        TableReader m_reader;
        TableWriter m_writer;
        readonly object m_rwLock = new object();


        public event Action<int> RowDeleting;
        public event Action<int> RowDeleted;
        public event Action<int , IDatum> RowInserted;
        public event Action<int , IDatum> RowReplacing;
        public event Action<int , IDatum> RowReplaced;
        public event Action TableResetted;


        public DataTable(uint id , string name , string filePath)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Debug.Assert(!string.IsNullOrWhiteSpace(filePath));

            Name = name;
            m_filePath = filePath;
            ID = id;
        }


        public uint ID { get; }
        public string Name { get; }
        public bool IsDisposed => IsConnected;
        public IDatumProvider DataProvider => new RowProvider(this);

        public string FilePath => m_filePath;

        public bool IsConnected
        {
            get
            {
                lock (m_rwLock)
                    return m_dataFile != null;
            }
        }

        public IDataColumn[] Columns
        {
            get
            {
                lock (m_rwLock)
                    return GetColumns();
            }
        }

        public DateTime CreationTime
        {
            get
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    return Header.CreationTime;

            }
        }

        public int RowCount
        {
            get
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    return GetDataCount();
            }
        }

        public uint Version
        {
            get
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    return Header.TableVersion;
            }

            protected set
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    Header.TableVersion = value;
            }
        }

        public DateTime LastAccessTime
        {
            get
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    return Header.LastAccessTime;
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                Debug.Assert(IsConnected);

                lock (m_rwLock)
                    return Header.LastWriteTime;
            }
        }

        public void Connect()
        {
            Debug.Assert(!IsConnected);
            Debug.Assert(!IsDisposed);

            lock (m_rwLock)
            {
                try
                {
                    Open();
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    TextLogger.Warning(ex.Message);
                }
                catch (Exception ex)
                {
                    TextLogger.Error(ex.Message);
                    throw;
                }


                if (!IsConnected)
                    try
                    {
                        Create();

                    }
                    catch (Exception ex)
                    {
                        TextLogger.Error(ex.Message);
                        throw;
                    }


                Init();
            }
        }

        public uint CreateUniqID()
        {
            Debug.Assert(IsConnected);

            lock (m_rwLock)
                return Header.NextDatumID++;
        }

        public T GetRow(int ndxItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem < RowCount);
            Debug.Assert(ndxItem >= 0);

            lock (m_rwLock)
            {
                T item = DoRead(ndxItem);
                Header.LastAccessTime = DateTime.Now;

                return item;
            }
        }

        public void InsertRow(T item)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(item != null);


            lock (m_rwLock)
            {
                int ndx = DoInsert(item);
                Header.LastAccessTime = Header.LastWriteTime = DateTime.Now;
                RowInserted?.Invoke(ndx , item);
            }
        }

        public void ReplaceRow(int ndxItem , T newItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem >= 0);
            Debug.Assert(ndxItem < RowCount);
            Debug.Assert(newItem != null);


            lock (m_rwLock)
            {
                T oldItem = GetRow(ndxItem);
                RowReplacing?.Invoke(ndxItem , oldItem);

                int ndx = DoReplace(ndxItem , newItem);
                Header.LastAccessTime = Header.LastWriteTime = DateTime.Now;

                if (ndx != ndxItem)
                {
                    RowDeleted?.Invoke(ndxItem);
                    RowInserted?.Invoke(ndx , newItem);
                }
                else
                    RowReplaced?.Invoke(ndx , newItem);
            }
        }

        public void DeleteRow(int ndxItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem >= 0);
            Debug.Assert(ndxItem < RowCount);

            lock (m_rwLock)
            {
                RowDeleting?.Invoke(ndxItem);
                DoDelete(ndxItem);
                Header.LastWriteTime = Header.LastAccessTime = DateTime.Now;
                RowDeleted?.Invoke(ndxItem);
            }
        }

        public void Flush()
        {
            lock (m_rwLock)
            {
                if (IsConnected)
                    if (Header.IsDirty)
                    {
                        ++Header.TableVersion;
                        Writer.Position = 0;
                        Header.Store(Writer);
                    }

                m_dataFile.Flush();
            }        
    
        }        

        public void Dispose()
        {
            if (IsConnected)
            {
                Debug.Assert(IsConnected == IsDisposed);

                Flush();

                lock (m_rwLock)
                {
                    DoDispose();

                    RowInserted = RowReplaced = RowReplacing = null;
                    RowDeleting = RowDeleted = null;

                    m_dataFile.Dispose();
                    m_dataFile = null;

                }
            }
        }

        public override string ToString() => Name;
        
        
        
        //protected:
        
        protected abstract TableHeader Header { get; }
        protected abstract int GetDataCount();
        protected abstract void Init();
        protected abstract int DoInsert(T item);
        protected abstract T DoRead(int ndxItem);
        protected abstract void DoDelete(int ndxItem);
        protected abstract int DoReplace(int ndxItem , T item);
        protected abstract void DoDispose();
        protected abstract IDataColumn[] GetColumns();        

        protected ITableReader Reader
        {
            get { return m_reader; }
        }

        protected ITableWriter Writer
        {
            get { return m_writer; }
        }

        protected IDisposable AcquireLock()
        {
            Monitor.Enter(m_rwLock);

            return new AutoReleaser(() => Monitor.Exit(m_rwLock));
        }

        protected void Reset()
        {
            Debug.Assert(IsConnected);

            lock(m_rwLock)
            {
                Header.Reset();
                m_dataFile.SetLength(0);
                Header.Store(m_writer);

                TableResetted?.Invoke();
            }            
        }
        

        //private:
        void Create()
        {
            const int BUFFER_SIZE = 4096;

            TextLogger.Debug("Création de fichier {0}.", m_filePath);

            m_dataFile = new System.IO.FileStream(m_filePath ,
             System.IO.FileMode.CreateNew ,
             System.IO.FileAccess.ReadWrite ,
             System.IO.FileShare.Read ,
             BUFFER_SIZE ,
             System.IO.FileOptions.RandomAccess);

            Header.Reset();

            var tw = new TableWriter(m_dataFile);
            Header.Store(tw);

            m_reader = new TableReader(m_dataFile);
            m_writer = new TableWriter(m_dataFile);
        }

        void Open()
        {
            const int BUFFER_SIZE = 4096;

            TextLogger.Debug("Ouverture du fichier {0}.", m_filePath);

            m_dataFile = new System.IO.FileStream(m_filePath ,
             System.IO.FileMode.Open ,
             System.IO.FileAccess.ReadWrite ,
             System.IO.FileShare.Read ,
             BUFFER_SIZE ,
             System.IO.FileOptions.RandomAccess);

            var tr = new TableReader(m_dataFile);

            try
            {
                Header.Load(tr);
            }
            catch (Exception ex)
            {
                TextLogger.Error(ex.Message);

                m_dataFile.Dispose();
                m_dataFile = null;

                throw new CorruptedFileException(m_filePath , innerException: ex);
            }

            m_reader = new TableReader(m_dataFile);
            m_writer = new TableWriter(m_dataFile);
        }
    }
}
