using easyLib;
using System;
using System.IO;
using System.Text;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.DB
{
    interface IBinDataManager
    {
        long Add(byte[] data);
        long Replace(byte[] data , long position);
        byte[] Read(long position , int count);
    }




    sealed class BinDataManager: IBinDataManager, IDisposable
    {
        const string FILE_SIGNATURE = "bd1";
        readonly static byte[] m_signature = Encoding.UTF8.GetBytes(FILE_SIGNATURE);
        readonly string m_filePath;
        readonly byte[] m_buff = new byte[sizeof(int)];   //for reading block size. the lock
        FileStream m_fs;


        public BinDataManager(string filePath)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));

            m_filePath = filePath;
        }


        public bool IsDisposed { get; private set; }


        public long Add(byte[] data)
        {
            Assert(data != null);

            lock (m_buff)
                return AppendBlock(data);
        }

        public long Replace(byte[] data , long position)
        {
            Assert(data != null);

            lock (m_buff)
                return WriteBlock(data , position);
        }

        public byte[] Read(long position , int count)
        {
            Assert(position >= 0);
            Assert(count >= 0);

            lock (m_buff)
                return ReadBlock(position , count);
        }

        public static byte[] GetBytes(uint[] data)
        {
            Assert(data != null);

            var bytes = new byte[data.Length * sizeof(uint)];

            Assert(sizeof(uint) == 4);

            for (int i = 0, ndx = 0; i < data.Length; ++i, ndx = i << 2)
            {
                uint n = data[i];

                bytes[ndx] = (byte)n;
                bytes[ndx + 1] = (byte)(n >> 8);
                bytes[ndx + 2] = (byte)(n >> 16);
                bytes[ndx + 3] = (byte)(n >> 24);
            }

            return bytes;
        }

        public static uint[] GetUInts(byte[] data)
        {
            Assert(data != null);
            Assert(data.Length % 4 == 0);

            var vals = new uint[data.Length >> 2];

            for (int i = 0, ndx = 0; i < vals.Length; ++i, ndx = i << 2)
                vals[i] = (uint)(data[ndx] | data[ndx + 1] << 8 | data[ndx + 2] << 16 | data[ndx + 3] << 24);

            return vals;
        }

        public void Dispose()
        {
            if(!IsDisposed)
                lock(m_buff)
                    if(!IsDisposed)
                    {
                        m_fs?.Close();
                        IsDisposed = true;
                    }
        }

        //private:
        FileStream BinStream
        {
            get
            {
                if (m_fs != null)
                    return m_fs;

                try
                {
                    m_fs = File.Open(m_filePath , FileMode.Open , FileAccess.ReadWrite);
                }
                catch (FileNotFoundException)
                {
                    m_fs = File.Create(m_filePath);
                    m_fs.Write(m_signature , 0 , m_signature.Length);
                    m_fs.Seek(0 , SeekOrigin.Begin);
                }

                foreach (byte b in m_signature)
                    if (m_fs.ReadByte() != b)
                        throw new CorruptedFileException(m_filePath);

                return m_fs;
            }
        }

        long AppendBlock(byte[] data)
        {
            var fs = BinStream;

            long result = fs.Seek(0 , SeekOrigin.End);

            WriteSize(data.Length);
            fs.Write(data , 0 , data.Length);
            fs.Flush();

            return result;
        }

        long WriteBlock(byte[] data , long pos)
        {
            var fs = BinStream;

            fs.Position = pos;
            int szBlock = ReadSize();

            // last block => block offset + block size = file length
            if (data.Length <= szBlock)
                fs.Write(data , 0 , data.Length);
            else if (fs.Position + szBlock == fs.Length)
            {
                fs.Position = pos;
                WriteSize(data.Length);
                fs.Write(data , 0 , data.Length);
            }
            else
                pos = AppendBlock(data);

            fs.Flush();

            return pos;
        }

        byte[] ReadBlock(long pos , int count)
        {
            var fs = BinStream;
            fs.Position = pos;
            int szBlock = ReadSize();
            var data = new byte[count];

            if (szBlock < count || fs.Read(data , 0 , count) != count)
                throw new InvalidDataException();

            return data;
        }
        
        int ReadSize()
        {
            if (BinStream.Read(m_buff , 0 , sizeof(int)) != sizeof(int))
                throw new InvalidDataException();

            Assert(sizeof(int) == 4);

            return m_buff[0] | m_buff[1] << 8 | m_buff[2] << 16 | m_buff[3] << 24;
        }

        void WriteSize(int sz)
        {
            Assert(sizeof(int) == 4);

            m_buff[0] = (byte)sz;
            m_buff[1] = (byte)(sz >> 8);
            m_buff[2] = (byte)(sz >> 16);
            m_buff[3] = (byte)(sz >> 24);

            BinStream.Write(m_buff , 0 , 4);
        }

    }
}
