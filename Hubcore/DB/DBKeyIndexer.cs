using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{

    public interface IDBKeyIndexer: IDisposable
    {
        IDBProvider Source { get; }
        IEnumerable<uint> Keys { get; }
        IDataRow this[uint key] { get; }

        IDataRow Get(uint key);
        int IndexOf(uint key);
    }



    public sealed class DBKeyIndexer: IDBKeyIndexer
    {
        readonly object m_lock = new object();
        readonly ITablesManager m_tblManager;
        readonly Dictionary<int , IDataRow> m_cache = new Dictionary<int , IDataRow>();
        readonly Dictionary<uint , int> m_dataIndices = new Dictionary<uint , int>();
        readonly uint m_tableID;
        bool m_processingMode;

        public DBKeyIndexer(ITablesManager tblManager , uint tableID)
        {
            Assert(tblManager != null);

            m_tblManager = tblManager;
            m_tableID = tableID;
        }

        public IDataRow this[uint key] => Get(key);
        public bool IsConnected { get; private set; }
        public IDBProvider Source => m_tblManager.GetDataProvider(m_tableID);

        public IEnumerable<uint> Keys
        {
            get
            {
                Assert(IsConnected);

                Monitor.Enter(m_lock);
                var keys = m_dataIndices.Keys;
                Monitor.Exit(m_lock);

                return keys;
            }
        }


        public void Connect()
        {
            Assert(!IsConnected);

            lock (m_lock)
                if (!IsConnected)
                {
                    RegisterHandlers();
                    LoadData();

                    m_tblManager.BeginTableProcessing += TableManager_BeginTableProcessing;
                    m_tblManager.EndTableProcessing += TableManager_EndTableProcessing;

                    IsConnected = true;
                }
        }

        public void Disconnect()
        {
            lock (m_lock)
                if (IsConnected)
                {
                    UnregisterHandlers();

                    m_tblManager.BeginTableProcessing -= TableManager_BeginTableProcessing;
                    m_tblManager.EndTableProcessing -= TableManager_EndTableProcessing;

                    IsConnected = false;
                }
        }

        public void Dispose() => Disconnect();

        public IDataRow Get(uint key)
        {
            Assert(IsConnected);

            lock (m_lock)
            {
                int ndx = GetKeyIndex(key);
                return ndx < 0 ? null : m_tblManager.GetDataProvider(m_tableID).Get(ndx) as IDataRow;
            }
        }

        public int IndexOf(uint key)
        {
            Assert(IsConnected);

            Monitor.Enter(m_lock);
            int ndx = GetKeyIndex(key);
            Monitor.Exit(m_lock);

            return ndx;
        }

        public IDisposable Lock()
        {
            Monitor.Enter(m_lock);
            return new AutoReleaser(() => Monitor.Exit(m_lock));
        }


        //private:
        int GetKeyIndex(uint key)
        {
            int ndx;
            return m_dataIndices.TryGetValue(key , out ndx) ? ndx : -1;
        }

        void LoadData()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);
            for (int ndx = 0; ndx < dp.Count; ++ndx)
                m_dataIndices.Add((dp.Get(ndx) as IDataRow).ID , ndx);
        }

        void RegisterHandlers()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            dp.DatumDeleted += DataProvider_DatumDeleted;
            dp.DatumDeleting += DataProvider_DatumDeleting;
            dp.DatumInserted += DataProvider_DatumInserted;
            dp.DatumReplaced += DataProvider_DatumReplaced;
            dp.DatumReplacing += DataProvider_DatumReplacing;
            dp.DataDeleted += DataProvider_DataDeleted;
            dp.DataDeleting += DataProvider_DataDeleting;
            dp.DataInserted += DataProvider_DataInserted;
            dp.SourceCleared += DataProvider_SourceCleared;
        }

        void UnregisterHandlers()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            dp.DatumDeleted -= DataProvider_DatumDeleted;
            dp.DatumDeleting -= DataProvider_DatumDeleting;
            dp.DatumInserted -= DataProvider_DatumInserted;
            dp.DatumReplaced -= DataProvider_DatumReplaced;
            dp.DatumReplacing -= DataProvider_DatumReplacing;
            dp.DataDeleted -= DataProvider_DataDeleted;
            dp.DataDeleting -= DataProvider_DataDeleting;
            dp.DataInserted -= DataProvider_DataInserted;
            dp.SourceCleared -= DataProvider_SourceCleared;
        }

        void DataProvider_DatumInserted(int ndxSource , IDataRow datum)
        {
            Assert(!m_dataIndices.ContainsKey(datum.ID));

            Monitor.Enter(m_lock);

            var keys = from kv in m_dataIndices
                       where kv.Value >= ndxSource
                       select kv.Key;

            keys.ToList().ForEach(k => ++m_dataIndices[k]);

            m_dataIndices.Add(datum.ID , ndxSource);

            Monitor.Exit(m_lock);
        }

        void DataProvider_DataInserted(int[] srcIndices , IDataRow[] data)
        {
            Monitor.Enter(m_lock);

            for (int i = 0; i < srcIndices.Length; ++i) //le traitement en une seule passe est invalide.
            {
                IDataRow datum = data[i];

                Assert(!m_dataIndices.ContainsKey(datum.ID));

                int ndx = srcIndices[i];
                var keys = from kv in m_dataIndices
                           where kv.Value >= ndx
                           select kv.Key;

                keys.ToList().ForEach(k => ++m_dataIndices[k]);

                m_dataIndices.Add(datum.ID , ndx);
            }

            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumDeleting(int srcIndex)
        {
            var datum = m_tblManager.GetDataProvider(m_tableID).Get(srcIndex);

            Monitor.Enter(m_lock);
            m_cache.Add(srcIndex , datum);
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumDeleted(int srcIndex)
        {
            Assert(m_cache.ContainsKey(srcIndex));

            Monitor.Enter(m_lock);

            IDataRow datum = m_cache[srcIndex];
            m_cache.Remove(srcIndex);
            m_dataIndices.Remove(datum.ID);

            var keys = from kv in m_dataIndices
                       where kv.Value > srcIndex
                       select kv.Key;

            keys.ToList().ForEach(k => --m_dataIndices[k]);

            Monitor.Exit(m_lock);
        }

        void DataProvider_DataDeleting(int[] srcIndices)
        {
            lock (m_lock)
                for (int i = 0; i < srcIndices.Length; ++i)
                {
                    int srcIndex = srcIndices[i];
                    var datum = m_tblManager.GetDataProvider(m_tableID).Get(srcIndex);

                    m_cache.Add(srcIndex , datum);
                }
        }

        void DataProvider_DataDeleted(int[] srcIndices)
        {
            Monitor.Enter(m_lock);
            for (int i = 0; i < srcIndices.Length; ++i)
            {
                int srcIndex = srcIndices[i];

                Assert(m_cache.ContainsKey(srcIndex));

                IDataRow datum = m_cache[srcIndex];
                m_cache.Remove(srcIndex);
                m_dataIndices.Remove(datum.ID);

                var keys = from kv in m_dataIndices
                           where kv.Value > srcIndex
                           select kv.Key;

                keys.ToList().ForEach(k => --m_dataIndices[k]);
            }

            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumReplacing(int srcIndex , IDataRow datum)
        {
            Monitor.Enter(m_lock);
            m_cache.Add(srcIndex , datum);
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumReplaced(int srcIndex , IDataRow datum)
        {
            Assert(m_dataIndices.ContainsKey(datum.ID));
            Assert(m_dataIndices[datum.ID] == srcIndex);
            Assert(m_cache.ContainsKey(srcIndex));

            //IDataRow.ID is immutable => juste remove from cache
            Monitor.Enter(m_lock);
            m_cache.Remove(srcIndex);
            Monitor.Exit(m_lock);
        }

        void TableManager_EndTableProcessing(uint tableID)
        {
            Monitor.Enter(m_lock);

            if (tableID == m_tableID && m_processingMode)
            {
                RegisterHandlers();
                m_processingMode = false;
            }

            Monitor.Exit(m_lock);

        }

        void TableManager_BeginTableProcessing(uint tableID)
        {
            Monitor.Enter(m_lock);

            if (tableID == m_tableID)
            {
                UnregisterHandlers();
                m_processingMode = true;
            }

            Monitor.Exit(m_lock);
        }

        private void DataProvider_SourceCleared()
        {
            Monitor.Enter(m_lock);
            m_cache.Clear();
            m_dataIndices.Clear();
            Monitor.Exit(m_lock);
        }
    }
}
