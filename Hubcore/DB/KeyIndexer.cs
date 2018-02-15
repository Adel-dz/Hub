using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace DGD.HubCore.DB
{
    public interface IKeyIndexer : IDisposable
    {
        event Action<IDataRow> DatumInserted;
        event Action<IDataRow> DatumDeleting;
        event Action<IDataRow> DatumDeleted;
        event Action<IDataRow> DatumReplacing;
        event Action<IDataRow> DatumReplaced;

        IDataRow Get(uint idDatum);
        int IndexOf(uint idDatum);
        IEnumerable<uint> Keys { get; }

        void Connect();
        void Close();
    }
    


    public sealed class KeyIndexer : IKeyIndexer
    {
        Dictionary<int , IDataRow> m_cache;
        readonly Dictionary<uint , int> m_ndxTable = new Dictionary<uint , int>();  //the lock
        readonly IDatumProvider m_dataProvider;
        int m_refCount;

        //public:
        public event Action<IDataRow> DatumInserted;
        public event Action<IDataRow> DatumDeleting;
        public event Action<IDataRow> DatumDeleted;
        public event Action<IDataRow> DatumReplacing;
        public event Action<IDataRow> DatumReplaced;


        public KeyIndexer(IDatumProvider provider)
        {
            Debug.Assert(provider != null);

            m_dataProvider = provider;
        }


        public bool IsDisposed { get; private set; }
        public bool IsConnected { get; private set; }
        public IDatumProvider Source => m_dataProvider;

        public IEnumerable<uint> Keys
        {
            get
            {
                Debug.Assert(IsConnected);

                return m_ndxTable.Keys;
            }
        }
        

        public IDataRow Get(uint idDatum)
        {
            Debug.Assert(IsConnected);

            int ndx = IndexOf(idDatum);
            return ndx < 0 ? null : m_dataProvider.Get(ndx) as IDataRow;
        }

        public int IndexOf(uint idDatum)
        {
            Debug.Assert(IsConnected);

            int ndx;
            lock (m_ndxTable)
            {
                return m_ndxTable.TryGetValue(idDatum, out ndx) ? ndx : -1;
            }
        }

        public void Dispose()
        {
            lock (m_ndxTable)
            {
                if (!IsDisposed)
                {
                    Disconnect(true);
                    DatumDeleted = DatumDeleting = DatumInserted =
                        DatumReplaced = DatumReplacing = null;

                    IsDisposed = true;
                } 
            }
        }

        public void Connect()
        {
            lock (m_ndxTable)
            {
                m_dataProvider.Connect();

                if (++m_refCount == 1)
                {
                    RegisterHandlers();
                    LoadData();

                    IsConnected = true;
                }

            }
        }

        public void Close()
        {
            lock (m_ndxTable)
            {
                Disconnect();

                if (m_refCount == 0)
                    Dispose();             
            }
        }


        //private:
        void RegisterHandlers()
        {
            m_dataProvider.DatumInserted += DataProvider_DatumInserted;
            m_dataProvider.DatumDeleted += DataProvider_DatumDeleted;
            m_dataProvider.DatumDeleting += DataProvider_DatumDeleting;
            m_dataProvider.DatumReplaced += DataProvider_DatumReplaced;
            m_dataProvider.DatumReplacing += DataProvider_DatumReplacing;
        }

        void DataProvider_DatumReplacing(int ndx, IDatum datum)
        {
            lock (m_ndxTable)
            {
                if (m_cache == null)
                    m_cache = new Dictionary<int , IDataRow>();

                m_cache.Add(ndx , datum as IDataRow); 
            }

            DatumReplacing?.Invoke(datum as IDataRow);
        }

        void DataProvider_DatumReplaced(int ndxDatum, IDatum datum)
        {
            var d = datum as IDataRow;

            Debug.Assert(m_ndxTable.ContainsKey(d.ID));
            Debug.Assert(m_ndxTable[d.ID] == ndxDatum);
            Debug.Assert(m_cache != null);
            Debug.Assert(m_cache.ContainsKey(ndxDatum));

            lock (m_ndxTable)
            {
                m_cache.Remove(ndxDatum);
            }

            DatumReplaced?.Invoke(d);
        }

        void DataProvider_DatumDeleting(int ndxDatum)
        {
            var row = m_dataProvider.Get(ndxDatum) as IDataRow;

            lock (m_ndxTable)
            {
                if (m_cache == null)
                    m_cache = new Dictionary<int, IDataRow>();

                m_cache.Add(ndxDatum, row);
            }

            DatumDeleting?.Invoke(row);
        }

        void DataProvider_DatumDeleted(int ndxDatum)
        {
            Debug.Assert(m_cache != null && m_cache.ContainsKey(ndxDatum));

            IDataRow row;

            lock (m_ndxTable)
            {
                row = m_cache[ndxDatum];
                m_cache.Remove(ndxDatum);
                m_ndxTable.Remove(row.ID);

                var keys = from kv in m_ndxTable
                           where kv.Value > ndxDatum
                           select kv.Key;

                keys.ToList().ForEach(k => --m_ndxTable[k]);
            }

            DatumDeleted?.Invoke(row);
        }

        void DataProvider_DatumInserted(int ndxDatum, IDatum datum)
        {
            var row = datum as IDataRow;

            Debug.Assert(!m_ndxTable.ContainsKey(row.ID));

            lock (m_ndxTable)
            {
                var keys = from kv in m_ndxTable
                           where kv.Value >= ndxDatum
                           select kv.Key;

                keys.ToList().ForEach(k => ++m_ndxTable[k]);

                m_ndxTable.Add(row.ID, ndxDatum);
            }
            
            DatumInserted?.Invoke(row);
        }

        void LoadData()
        {
            for (int ndx = 0; ndx < m_dataProvider.Count; ++ndx)
                m_ndxTable.Add((m_dataProvider.Get(ndx) as IDataRow).ID, ndx);                
        }

        void UnregisterHandlers()
        {
            m_dataProvider.DatumInserted -= DataProvider_DatumInserted;
            m_dataProvider.DatumDeleted -= DataProvider_DatumDeleted;
            m_dataProvider.DatumDeleting -= DataProvider_DatumDeleting;
            m_dataProvider.DatumReplaced -= DataProvider_DatumReplaced;
            m_dataProvider.DatumReplacing -= DataProvider_DatumReplacing;
        }

        void Disconnect(bool closeAll = false)
        {
            if (m_refCount > 0) 
            {
                if (closeAll)
                    m_refCount = 1;

                if (--m_refCount == 0)
                {
                    UnregisterHandlers();
                    IsConnected = false;
                }
                
                m_dataProvider.Close();
            }
        }
    }

}
