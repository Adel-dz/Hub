using easyLib;
using easyLib.DB;
using System;
using System.Text;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    partial class DBTable<T>
    {

        class Header
        {
            const string m_signature = "DBTBL1";
            volatile int m_frameCount;
            volatile int m_ndxFirstDelFrame;
            DateTime m_tmCreation;
            DateTime m_tmLastWrite;
            uint m_ver;
            uint m_tag;
            
            public Header(int szDatum = 0, uint tag = 0)
            {
                CreationTime = LastWriteTime = DateTime.Now;
                FrameSize = szDatum;
                Tag = tag;
                FirstDeletedFrameIndex = NULL_INDEX;
            }


            public bool IsDirty { get; private set; }
            public int FrameSize { get; private set; }
            public long DataOffset { get; private set; }

            public int FrameCount
            {
                get { return m_frameCount; }
                set
                {
                    m_frameCount = value;
                    IsDirty = true;
                }
            }

            public int FirstDeletedFrameIndex
            {
                get { return m_ndxFirstDelFrame; }
                set
                {
                    m_ndxFirstDelFrame = value;
                    IsDirty = true;
                }
            }
  
            public DateTime CreationTime
            {
                get { return m_tmCreation; }
                set
                {
                    m_tmCreation = value;
                    IsDirty = true;
                }
            }

            public DateTime LastWriteTime
            {
                get { return m_tmLastWrite; }
                set
                {
                    m_tmLastWrite = value;
                    IsDirty = true;
                }
            }

            public uint Version
            {
                get { return m_ver; }
                set
                {
                    m_ver = value;
                    IsDirty = true;
                }
            }

            public uint Tag
            {
                get { return m_tag; }
                set
                {
                    m_tag = value;
                    IsDirty = true;
                }
            }

            public void Load(ITableReader reader)
            {
                Assert(reader != null);

                byte[] sign = Signature;
                byte[] bytes = reader.ReadBytes(sign.Length);

                for (int i = 0; i < sign.Length; ++i)
                    if (sign[i] != bytes[i])
                        throw new CorruptedStreamException();

                uint ver = reader.ReadUInt();
                uint tag = reader.ReadUInt();
                int nTotal = reader.ReadInt();
                int ndxDeleted = reader.ReadInt();
                int sz = reader.ReadInt();

                if (nTotal <= ndxDeleted || sz <= 0 )
                    throw new CorruptedStreamException();

                CreationTime = reader.ReadTime();
                LastWriteTime = reader.ReadTime();

                Version = ver;
                Tag = tag;
                FrameCount = nTotal;
                FirstDeletedFrameIndex = ndxDeleted;
                FrameSize = sz;
                DataOffset = reader.Position;

                IsDirty = false;
            }

            public void Store(ITableWriter writer)
            {
                Assert(writer != null);
                Assert(FrameSize > 0);

                writer.Write(Signature);
                writer.Write(Version);
                writer.Write(Tag);
                writer.Write(FrameCount);
                writer.Write(FirstDeletedFrameIndex);
                writer.Write(FrameSize);
                writer.Write(CreationTime);
                writer.Write(LastWriteTime);

                DataOffset = writer.Position;
                IsDirty = false;

            }

            public void Reset()
            {
                Tag = Version = 0;
                FrameCount = 0;
                FirstDeletedFrameIndex = NULL_INDEX;
                CreationTime = LastWriteTime = DateTime.Now;
                IsDirty = false;
            }


            //private:
            static byte[] Signature => Encoding.UTF8.GetBytes(m_signature);
        }
        
    }
}
