using System.Text;
using System.Diagnostics;


namespace easyLib.DB
{
    partial class FramedTable<T>
    {
        protected class FileHeader: TableHeader
        {
            const string SIGNATURE = "EZDBFR1";
            volatile int m_frameCount;
            volatile int m_ndxFirstDelFrame = NDX_NULL;

            //public:
            public int FrameCount
            {
                get { return m_frameCount; }
                set
                {
                    m_frameCount = value;
                    IsDirty = true;
                }
            }

            public int DeletedFrameHead
            {
                get { return m_ndxFirstDelFrame; }
                set
                {
                    Debug.Assert(value < FrameCount);

                    m_ndxFirstDelFrame = value;
                    IsDirty = true;
                }
            }


            //protected:
            protected override void DoLoad(ITableReader reader)
            {
                int allCount = (int)reader.ReadLong();
                int ndxFirstDelFrame = (int)reader.ReadLong();

                if (allCount <= ndxFirstDelFrame)
                    throw new CorruptedStreamException();

                m_frameCount = allCount;
                m_ndxFirstDelFrame = ndxFirstDelFrame;
            }

            protected override void DoReset()
            {
                m_frameCount = 0;
                m_ndxFirstDelFrame = NDX_NULL;
            }

            protected override void DoStore(ITableWriter writer)
            {
                writer.Write((long)m_frameCount);
                writer.Write((long)m_ndxFirstDelFrame);
            }

            protected override byte[] GetSignature()
            {
                return Encoding.UTF8.GetBytes(SIGNATURE);
            }
        }
    }
}
