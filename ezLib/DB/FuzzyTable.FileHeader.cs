using System.Text;

namespace easyLib.DB
{
    partial class FuzzyTable<T>
    {
        protected class FileHeader: TableHeader
        {
            const string SIGNATURE = "EZDBFZ1";
            static readonly byte[] m_signature;
            volatile int m_dataCount;


            //public:
            static FileHeader()
            {
                m_signature = Encoding.UTF8.GetBytes(SIGNATURE);
            }

            public int DataCount
            {
                get { return m_dataCount; }
                set
                {
                    m_dataCount = value;
                    IsDirty = true;
                }
            }

            
            //protected:
            protected override void DoLoad(ITableReader reader)
            {
                int count = (int)reader.ReadLong();

                DataCount = count;
            }

            protected override void DoReset()
            {
                DataCount = 0;
            }

            protected override void DoStore(ITableWriter writer)
            {
                writer.Write((long)DataCount);
            }

            protected override byte[] GetSignature()
            {
                return m_signature;
            }       
            
        }
    }
}
