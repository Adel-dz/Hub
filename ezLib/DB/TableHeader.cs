using System;
using System.Diagnostics;


namespace easyLib.DB
{
    public abstract class TableHeader
    {
        DateTime m_creationTime, m_lastAccessTime, m_lastWriteTime;
        uint m_nextId;


        //public:
        public TableHeader()
        {
            m_nextId = TableVersion = 1;
        }


        public byte[] Signature
        {
            get { return GetSignature(); }
        }

        public DateTime CreationTime
        {
            get
            {
                return m_creationTime;
            }

            set
            {
                m_creationTime = value;
                IsDirty = true;
            }
        }

        public DateTime LastWriteTime
        {
            get { return m_lastWriteTime; }
            set
            {
                m_lastWriteTime = value;
                IsDirty = true;
            }
        }

        public DateTime LastAccessTime
        {
            get { return m_lastAccessTime; }
            set
            {
                m_lastAccessTime = value;
                IsDirty = true;
            }
        }

        public uint TableVersion { get; set; }

        public long DataPosition { get; set; }

        public bool IsDirty { get; protected set; }

        public uint NextDatumID
        {
            get { return m_nextId; }

            set
            {
                m_nextId = value;
                IsDirty = true;
            }
        }


        public void Load(ITableReader reader)
        {
            Debug.Assert(reader != null);

            foreach (byte b in Signature)
                if (reader.ReadByte() != b)
                    throw new CorruptedStreamException();
                        
            DataPosition = reader.ReadLong();
            TableVersion = reader.ReadUInt();
            DateTime creation = reader.ReadTime();
            DateTime access = reader.ReadTime();
            DateTime write = reader.ReadTime();
            uint nextId = reader.ReadUInt();

            DoLoad(reader);

            LastWriteTime = write;
            LastAccessTime = access;
            CreationTime = creation;
            m_nextId = nextId;

            IsDirty = false;
        }

        public void Store(ITableWriter writer)
        {
            writer.Write(Signature);            
            long dpPos = writer.Position;
            writer.Write(TableVersion);
            writer.Write(DataPosition);            
            writer.Write(CreationTime);
            writer.Write(LastAccessTime);
            writer.Write(LastWriteTime);
            writer.Write(m_nextId);

            DoStore(writer);                        

            DataPosition = writer.Position;
            writer.Position = dpPos;
            writer.Write(DataPosition);
            writer.Position = DataPosition;

            IsDirty = false;
        }

        public void Reset()
        {
            CreationTime = LastAccessTime = LastWriteTime = DateTime.Now;
            TableVersion = 1;
            m_nextId = 1;

            DoReset();
        }


        //protected:
        protected abstract byte[] GetSignature();
        protected abstract void DoLoad(ITableReader reader);
        protected abstract void DoStore(ITableWriter writer);
        protected abstract void DoReset();
    }
}
