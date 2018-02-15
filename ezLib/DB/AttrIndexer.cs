using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;




namespace easyLib.DB
{
    public interface IAttrIndexer<T> : IDisposable        
    {
        event Action<IDatum> DatumInserted;
        event Action<IDatum> DatumDeleting;
        event Action<IDatum> DatumDeleted;
        event Action<IDatum> DatumReplacing;
        event Action<IDatum> DatumReplaced;
        event Action SourceResetted;

        IEnumerable<T> Attributes { get; }

        void Connect();
        void Close();
        IEnumerable<IDatum> Get(T attr);
        IEnumerable<int> IndexOf(T attr);
        
    }
    


    public class AttrIndexer<T> : IAttrIndexer<T>
    {
        
        readonly Dictionary<T , List<int>> m_ndxTable;// = new Dictionary<T , List<int>>();   //mapping filed -> list of datum ndxs
        readonly List<Tuple<int , IDatum>> m_cache = new List<Tuple<int , IDatum>>();
        readonly Func<IDatum , T> m_selector;
        readonly IDatumProvider m_dataProvider;
        int m_refCount;

        
        //public:
        public event Action<IDatum> DatumInserted;
        public event Action<IDatum> DatumDeleting;
        public event Action<IDatum> DatumDeleted;
        public event Action<IDatum> DatumReplacing;
        public event Action<IDatum> DatumReplaced;
        public event Action SourceResetted;

        

        public AttrIndexer(IDatumProvider provider , Func<IDatum , T> selector, IEqualityComparer<T> comparer)
        {
            Debug.Assert(provider != null);
            Debug.Assert(selector != null);
            Debug.Assert(comparer != null);

            m_selector = selector;
            m_dataProvider = provider;
            m_ndxTable = new Dictionary<T , List<int>>(comparer);
        }

        public AttrIndexer(IDatumProvider provider, Func<IDatum, T> selector):
            this(provider, selector, EqualityComparer<T>.Default)
        { }


        public bool IsConnected { get; private set; }
        public bool IsDisposed { get; private set; }
        public IDatumProvider Source => m_dataProvider;
        public IEnumerable<T> Attributes
        {
            get
            {
                Debug.Assert(IsConnected);

                return m_ndxTable.Keys;
            }
        }


        public IEnumerable<IDatum> Get(T attr)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(attr != null);

            List<int> locs;

            lock (m_ndxTable)
            {
                if (!m_ndxTable.TryGetValue(attr, out locs))
                    return Enumerable.Empty<IDatum>();

                return YieldData(locs.ToArray());
            }            
        }

        public IEnumerable<int> IndexOf(T attr)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(attr != null);

            List<int> locs;

            lock (m_ndxTable)
            {
                if (!m_ndxTable.TryGetValue(attr, out locs))
                    return Enumerable.Empty<int>();

                return locs.ToArray();
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
            Debug.Assert(!IsDisposed);

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
            Debug.Assert(!IsDisposed);

            lock (m_ndxTable)
            {
                Disconnect();

                if (m_refCount == 0)
                    Dispose();
            }
        }


        //private:
        IEnumerable<IDatum> YieldData(int[] indices)
        {            
            foreach (int ndx in indices)
                yield return m_dataProvider.Get(ndx);
        }

        private void Disconnect(bool closeAll = false)
        {
            if (m_refCount > 0 ) 
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

        private void RegisterHandlers()
        {
            m_dataProvider.DatumInserted += DataProvider_DatumInserted;
            m_dataProvider.DatumDeleted += DataProvider_DatumDeleted;
            m_dataProvider.DatumDeleting += DataProvider_DatumDeleting;
            m_dataProvider.DatumReplaced += DataProvider_DatumReplaced;
            m_dataProvider.DatumReplacing += DataProvider_DatumReplacing;
            m_dataProvider.SourceResetted += DataProvider_SourceResetted;
        }

        private void UnregisterHandlers()
        {
            m_dataProvider.DatumInserted -= DataProvider_DatumInserted;
            m_dataProvider.DatumDeleted -= DataProvider_DatumDeleted;
            m_dataProvider.DatumDeleting -= DataProvider_DatumDeleting;
            m_dataProvider.DatumReplaced -= DataProvider_DatumReplaced;
            m_dataProvider.DatumReplacing -= DataProvider_DatumReplacing;
            m_dataProvider.SourceResetted -= DataProvider_SourceResetted;
        }

        private void LoadData()
        {
            for (int i = 0; i < m_dataProvider.Count; i++)
            {
                IDatum d = m_dataProvider.Get(i);
                T key = m_selector(d);

                List<int> lst;

                if (!m_ndxTable.TryGetValue(key, out lst))
                {
                    lst = new List<int>();
                    m_ndxTable[key] = lst;
                }

                lst.Add(i);
            }
        }

        void DataProvider_DatumReplacing(int ndx, IDatum datum)
        {
            /*
             * framed => replacing(ndx, d), replaced(ndx, d')
             * fuzzy => replacing(ndx,d), deleted(ndx), added(ndx', d')
             * */

            //T key = m_selector(datum);

            lock (m_cache)
            {
                m_cache.Add(Tuple.Create(ndx, datum));
            }

            DatumReplacing?.Invoke(datum);
        }

        void DataProvider_DatumReplaced(int ndx, IDatum datum)
        {
            IDatum oldDatum = null;

            lock (m_cache)
            {
                for (int i = 0; i < m_cache.Count; i++)
                    if (m_cache[i].Item1 == ndx)
                    {
                        oldDatum = m_cache[i].Item2;
                        m_cache.RemoveAt(i);
                        break;
                    }
            }

            Debug.Assert(oldDatum != null);

            T oldKey = m_selector(oldDatum);
            T newKey = m_selector(datum);

            if(!m_ndxTable.Comparer.Equals(oldKey,newKey))           
                lock (m_ndxTable)
                {
                    m_ndxTable[oldKey].Remove(ndx);

                    List<int> lst;

                    if (!m_ndxTable.TryGetValue(newKey , out lst))
                    {
                        lst = new List<int>();
                        m_ndxTable[newKey] = lst;
                    }

                    lst.Add(ndx);
                }


            DatumReplaced?.Invoke(datum);
        }

        void DataProvider_DatumDeleting(int ndx)
        {
            IDatum datum = m_dataProvider.Get(ndx);

            lock (m_cache)
            {
                m_cache.Add(Tuple.Create(ndx, datum));
            }

            DatumDeleting?.Invoke(datum);
        }

        void DataProvider_DatumDeleted(int ndx)
        {
            IDatum datum = null;

            lock (m_cache)
            {
                int i = 0;

                while (i < m_cache.Count)
                {
                    int index = m_cache[i].Item1;

                    if (ndx < index)
                    {
                        IDatum d = m_cache[i].Item2;
                        m_cache[i] = Tuple.Create(index - 1, d);
                        ++i;
                    }
                    else if (index == ndx)
                    {
                        datum = m_cache[i].Item2;
                        m_cache.RemoveAt(i);
                    }
                }
            }


            Debug.Assert(datum != null);

            T key = m_selector(datum);


            lock (m_ndxTable)
            {
                m_ndxTable[key].Remove(ndx);

                //adjust indices
                foreach (List<int> lst in m_ndxTable.Values)
                    for (int i = 0; i < lst.Count; i++)
                        if (ndx < lst[i])
                            --lst[i];
            }

            DatumDeleted?.Invoke(datum);
        }        

        void DataProvider_DatumInserted(int ndx, IDatum datum)
        {
            T key = m_selector(datum);

            lock (m_ndxTable)
            {
                List<int> lst;

                if (!m_ndxTable.TryGetValue(key, out lst))
                {
                    lst = new List<int>();
                    m_ndxTable[key] = lst;
                }

                //datum inserted not at the end => adjust indicies
                if (ndx < m_dataProvider.Count - 1)
                    foreach (List<int> l in m_ndxTable.Values)
                        for (int i = 0; i < lst.Count; i++)
                            if (ndx <= l[i])
                                ++l[i];
                lst.Add(ndx);

                DatumInserted?.Invoke(datum);
            }
            
        }

        void DataProvider_SourceResetted()
        {
            lock (m_cache)
                m_cache.Clear();

            lock (m_ndxTable)
                m_ndxTable.Clear();

            SourceResetted?.Invoke();
        }
    }
}
