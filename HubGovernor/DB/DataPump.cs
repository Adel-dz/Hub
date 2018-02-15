using DGD.HubCore.DB;
using easyLib.DB;
using System;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.DB
{
    sealed class DataPump: IDisposable
    {
        public class Item
        {
            public Item(IDataRow datum, int ndx)
            {
                Row = datum;
                Index = ndx;
            }


            public IDataRow Row { get; }
            public int Index { get; }
        }




        readonly IDatumProvider m_src;
        int m_ndxNextItem;


        public DataPump(IDatumProvider provider)
        {
            Assert(provider != null);

            m_src = provider;            
        }

        public bool IsConnected { get; private set; }

        public IDataRow NextRow
        {
            get
            {
                Assert(IsConnected);

                if (m_ndxNextItem < m_src.Count)
                    return m_src.Get(m_ndxNextItem++) as IDataRow;

                return null;
            }
        }

        public Item NextItem
        {
            get
            {
                Assert(IsConnected);

                int ndx = m_ndxNextItem;

                return ndx < m_src.Count ? new Item(NextRow , ndx) : null;
            }
        }

        public void Connect()
        {
            if(!IsConnected)
            {
                m_src.Connect();
                RegisterHandlers();
                IsConnected = true;
            }
        }

        public void Close()
        {
            if(IsConnected)
            {
                UnregisterHandlers();
                m_src.Close();
                m_ndxNextItem = 0;

                IsConnected = false;                
            }
        }

        public void Dispose() => Close();


        //private:
        void RegisterHandlers()
        {
            m_src.DatumDeleted += Source_DatumDeleted;
            m_src.DatumInserted += Source_DatumInserted;
        }

        private void UnregisterHandlers()
        {
            m_src.DatumDeleted -= Source_DatumDeleted;
            m_src.DatumInserted -= Source_DatumInserted;
        }

        //handlers:
        private void Source_DatumDeleted(int ndx)
        {
            if (ndx < m_ndxNextItem)
                --m_ndxNextItem;
        }

        void Source_DatumInserted(int ndx , IDatum datum)
        {
            if (ndx <= m_ndxNextItem)
                ++m_ndxNextItem;
        }
    }
}
