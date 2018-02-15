using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IDBColumnIndexer<T>: IDisposable
    {
        IDBProvider Source { get; }
        IEnumerable<T> Attributes { get; }
        IEnumerable<IDataRow> this[T column] { get; }

        IEnumerable<IDataRow> Get(T column);
        IEnumerable<int> IndexOf(T column);
    }




    public sealed class DBColumnIndexer<T>: IDBColumnIndexer<T>
    {
        readonly object m_lock = new object();
        readonly Dictionary<T , List<int>> m_dataIndices;  //mapping col -> list of datum ndxs
        readonly List<Tuple<int , IDataRow>> m_cache;
        readonly Func<IDataRow , T> m_selector;
        readonly ITablesManager m_tblManager;
        readonly uint m_tableID;
        bool m_processingMode;


        public DBColumnIndexer(ITablesManager tblManager , uint tableID , Func<IDataRow , T> selector , IEqualityComparer<T> comparer)
        {
            Assert(tblManager != null);
            Assert(selector != null);
            Assert(comparer != null);

            m_selector = selector;
            m_tblManager = tblManager;
            m_tableID = tableID;
            m_dataIndices = new Dictionary<T , List<int>>(comparer);
            m_cache = new List<Tuple<int , IDataRow>>();
        }


        public DBColumnIndexer(ITablesManager tblManager , uint tableID , Func<IDataRow , T> selector) :
            this(tblManager , tableID , selector , EqualityComparer<T>.Default)
        { }


        public bool IsConnected { get; private set; }
        public IDBProvider Source => m_tblManager.GetDataProvider(m_tableID);
        public IEnumerable<IDataRow> this[T column] => Get(column);

        public IEnumerable<T> Attributes
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

        public IEnumerable<IDataRow> Get(T attr)
        {
            Assert(attr != null);

            List<int> locs;

            Monitor.Enter(m_lock);
            m_dataIndices.TryGetValue(attr , out locs);
            Monitor.Exit(m_lock);

            if (locs == null)
                return Enumerable.Empty<IDataRow>();

            return YieldData(locs.ToArray());
        }

        public IEnumerable<int> IndexOf(T attr)
        {
            Assert(attr != null);

            List<int> locs;

            Monitor.Enter(m_lock);
            m_dataIndices.TryGetValue(attr , out locs);
            Monitor.Exit(m_lock);

            if (locs == null)
                return Enumerable.Empty<int>();

            return locs.ToArray();
        }

        public IDisposable Lock()
        {
            Monitor.Enter(m_lock);

            return new AutoReleaser(() => Monitor.Exit(m_lock));
        }

        public void Connect()
        {
            Assert(!IsConnected);

            lock (m_lock)
            {
                RegisterHandlers();

                m_tblManager.BeginTableProcessing += TableManager_BeginTableProcessing;
                m_tblManager.EndTableProcessing += TableManager_EndTableProcessing;

                LoadData();
                IsConnected = true;
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                Monitor.Enter(m_lock);

                m_tblManager.BeginTableProcessing -= TableManager_BeginTableProcessing;
                m_tblManager.EndTableProcessing -= TableManager_EndTableProcessing;

                UnregisterHandlers();
                m_dataIndices.Clear();
                m_cache.Clear();
                IsConnected = false;
                Monitor.Exit(m_lock);
            }
        }

        public void Dispose() => Disconnect();


        //private:
        IEnumerable<IDataRow> YieldData(int[] indices)
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            foreach (int ndx in indices)
                yield return dp.Get(ndx);
        }

        void RegisterHandlers()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            dp.DatumInserted += DataProvider_DatumInserted;
            dp.DatumDeleted += DataProvider_DatumDeleted;
            dp.DatumReplaced += DataProvider_DatumReplaced;
            dp.DatumDeleting += DataProvider_DatumDeleting;
            dp.DataDeleting += DataProvider_DataDeleting;
            dp.DatumReplacing += DataProvider_DatumReplacing;
            dp.SourceCleared += DataProvider_SourceCleared;
        }
            
        void UnregisterHandlers()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            dp.DatumInserted -= DataProvider_DatumInserted;
            dp.DatumDeleted -= DataProvider_DatumDeleted;
            dp.DatumReplaced -= DataProvider_DatumReplaced;
            dp.DatumDeleting -= DataProvider_DatumDeleting;
            dp.DataDeleting -= DataProvider_DataDeleting;
            dp.DatumReplacing -= DataProvider_DatumReplacing;
            dp.SourceCleared -= DataProvider_SourceCleared;
        }

        void LoadData()
        {
            IDBProvider dp = m_tblManager.GetDataProvider(m_tableID);

            for (int i = 0; i < dp.Count; i++)
            {
                IDataRow d = dp.Get(i);
                T key = m_selector(d);

                List<int> lst;

                if (!m_dataIndices.TryGetValue(key , out lst))
                {
                    lst = new List<int>();
                    m_dataIndices[key] = lst;
                }

                lst.Add(i);
            }
        }

        void DataProvider_DatumReplacing(int ndxSource , IDataRow datum)
        {
            Monitor.Enter(m_lock);
            m_cache.Add(Tuple.Create(ndxSource , datum));
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumReplaced(int ndxSource , IDataRow datum)
        {
            IDataRow oldDatum = null;

            Monitor.Enter(m_lock);
            for (int i = 0; i < m_cache.Count; i++)
                if (m_cache[i].Item1 == ndxSource)
                {
                    oldDatum = m_cache[i].Item2;
                    m_cache.RemoveAt(i);
                    break;
                }

            Assert(oldDatum != null);

            T oldKey = m_selector(oldDatum);
            T newKey = m_selector(datum);

            if (!m_dataIndices.Comparer.Equals(oldKey , newKey))
                m_dataIndices[oldKey].Remove(ndxSource);

            List<int> lst;

            if (!m_dataIndices.TryGetValue(newKey , out lst))
            {
                lst = new List<int>();
                m_dataIndices[newKey] = lst;
            }

            lst.Add(ndxSource);
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumDeleting(int srcIndex)
        {
            IDataRow datum = m_tblManager.GetDataProvider(m_tableID).Get(srcIndex);

            Monitor.Enter(m_lock);
            m_cache.Add(Tuple.Create(srcIndex , datum));
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumDeleted(int ndxSource)
        {
            IDataRow datum = null;

            int i = 0;

            Monitor.Enter(m_lock);

            while (i < m_cache.Count)
            {
                int index = m_cache[i].Item1;

                if (ndxSource < index)
                {
                    IDataRow d = m_cache[i].Item2;
                    m_cache[i] = Tuple.Create(index - 1 , d);
                    ++i;
                }
                else if (index == ndxSource)
                {
                    datum = m_cache[i].Item2;
                    m_cache.RemoveAt(i);
                }
            }


            Assert(datum != null);

            T key = m_selector(datum);

            m_dataIndices[key].Remove(ndxSource);

            //adjust indices
            foreach (List<int> lst in m_dataIndices.Values)
                for (int k = 0; k < lst.Count; k++)
                    if (ndxSource < lst[k])
                        --lst[k];
            Monitor.Exit(m_lock);
        }

        void DataProvider_DatumInserted(int ndxSource , IDataRow datum)
        {
            T key = m_selector(datum);

            List<int> lst;

            Monitor.Enter(m_lock);
            if (!m_dataIndices.TryGetValue(key , out lst))
            {
                lst = new List<int>();
                m_dataIndices[key] = lst;
            }

            //datum inserted not at the end => adjust indicies
            if (ndxSource < m_tblManager.GetDataProvider(m_tableID).Count - 1)
                foreach (List<int> l in m_dataIndices.Values)
                    for (int i = 0; i < lst.Count; i++)
                        if (ndxSource <= l[i])
                            ++l[i];
            lst.Add(ndxSource);
            Monitor.Exit(m_lock);
        }

        void DataProvider_DataDeleting(int[] srcIndices)
        {
            Monitor.Enter(m_lock);
            for (int i = 0; i < srcIndices.Length; ++i)
            {
                int srcIndex = srcIndices[i];
                IDataRow datum = m_tblManager.GetDataProvider(m_tableID).Get(srcIndex);

                m_cache.Add(Tuple.Create(srcIndex , datum));
            }
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
            m_dataIndices.Clear();
            m_cache.Clear();
            Monitor.Exit(m_lock);
        }
    }
}
