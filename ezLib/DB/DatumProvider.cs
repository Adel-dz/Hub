using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace easyLib.DB
{
    public interface IDatumProvider: IWeakProvider<IDatum>, IDisposable
    {
        event Action<int> DatumDeleting;
        event Action<int> DatumDeleted;
        event Action<int , IDatum> DatumInserted;
        event Action<int , IDatum> DatumReplacing;
        event Action<int , IDatum> DatumReplaced;
        event Action SourceResetted;

        bool IsDisposed { get; }
        bool IsConnected { get; }
        IDataSource DataSource { get; }

        void Connect();
        void Close();
    }



    public class DatumProvider: IDatumProvider
    {
        readonly IDatumProvider m_source;
        readonly ProviderMapper<IDatum> m_mapper;   //the lock
        readonly Predicate<IDatum> m_filter;
        int m_refCount;
        readonly AggregationMode_t m_aggMode;


        //public:
        public event Action<int , IDatum> DatumInserted;
        public event Action<int> DatumDeleted;
        public event Action<int> DatumDeleting;
        public event Action<int , IDatum> DatumReplaced;
        public event Action<int , IDatum> DatumReplacing;
        public event Action SourceResetted;


        public DatumProvider(IDatumProvider source , Predicate<IDatum> filter ,
            AggregationMode_t mode = AggregationMode_t.Rejected)
        {
            Debug.Assert(source != null);

            m_source = source;
            m_filter = filter;
            m_aggMode = mode;
            m_mapper = new ProviderMapper<IDatum>(m_source , m_filter , m_aggMode);
        }


        public IDataSource DataSource => m_source.DataSource;
        public bool IsDisposed { get; private set; }
        public bool IsConnected { get; private set; }

        public int Count
        {
            get
            {
                Debug.Assert(IsConnected);

                return m_mapper.AggregationMode == AggregationMode_t.Accepted ? m_mapper.Count :
                    m_source.Count - m_mapper.Count;
            }
        }


        public void Insert(IDatum item)
        {
            Debug.Assert(item != null);
            Debug.Assert(IsConnected);

            m_source.Insert(item);
        }

        public void Connect()
        {
            Debug.Assert(!IsDisposed);

            lock (m_mapper)
            {
                m_source.Connect();

                if (++m_refCount == 1)
                {
                    m_mapper.Connect();
                    RegisterHandlers();
                    IsConnected = true;

                    DebugHelper.RegisterProvider(this , m_source);
                }
            }
        }

        public void Close()
        {
            lock (m_mapper)
            {
                Disconnect();

                if (m_refCount == 0)
                    Dispose();
            }
        }

        public void Delete(int ndxItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem < Count);

            int xsDatum = m_mapper.ToSourceIndex(ndxItem);
            m_source.Delete(xsDatum);
        }

        public IDatum Get(int ndxItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem < Count);

            int xsDatum = m_mapper.ToSourceIndex(ndxItem);
            return m_source.Get(xsDatum);
        }

        public void Replace(int ndxItem , IDatum item)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxItem < Count);
            Debug.Assert(item != null);

            int xsDatum = m_mapper.ToSourceIndex(ndxItem);
            m_source.Replace(xsDatum , item);
        }

        public IEnumerable<IDatum> Enumerate() => Count == 0 ? Enumerable.Empty<IDatum>() : Enumerate(0);

        public IEnumerable<IDatum> Enumerate(int ndxFirstItem)
        {
            Debug.Assert(IsConnected);
            Debug.Assert(ndxFirstItem < Count);

            return Iterate(ndxFirstItem);
        }

        public void Dispose()
        {
            if (!IsDisposed)
                lock (m_mapper)
                {
                    if (m_refCount > 0)
                        Disconnect(true);

                    m_mapper.Disconnect();

                    DatumInserted = DatumReplaced = DatumReplacing = null;
                    DatumDeleted = DatumDeleting = null;
                    IsDisposed = true;

                    DebugHelper.UnregisterProvider(this);
                }
        }


        //private:
        void OnSourceResetted()
        {
            lock (m_mapper)
            {
                m_mapper.Disconnect();
                m_mapper.Connect();
            }

            SourceResetted?.Invoke();
        }

        void OnDatumReplacing(int xsDatum , IDatum datum)
        {
            if (m_mapper.Accepted(xsDatum))
            {
                var handler = DatumReplacing;

                if (handler != null)
                {
                    int xdItem = m_mapper.FromSourceIndex(xsDatum);
                    handler(xdItem , datum);
                }
            }
        }

        void OnDatumReplaced(int xsDatum , IDatum datum)
        {
            bool wasIncluded = m_mapper.Accepted(xsDatum);

            if (wasIncluded)
            {
                int xdItem = m_mapper.FromSourceIndex(xsDatum);
                m_mapper.OnSourceItemReplaced(xsDatum , datum);

                if (m_mapper.Filter(datum))
                    DatumReplaced?.Invoke(xdItem , datum);
                else
                    DatumDeleted?.Invoke(xdItem);
            }
            else
            {
                m_mapper.OnSourceItemReplaced(xsDatum , datum);

                if (m_mapper.Filter(datum))
                {
                    int xdItem = m_mapper.FromSourceIndex(xsDatum);
                    DatumInserted?.Invoke(xdItem , datum);
                }
            }
        }

        void OnDatumInserted(int xsDatum , IDatum datum)
        {
            m_mapper.OnSourceItemInserted(xsDatum , datum);

            if (m_mapper.Filter(datum))
            {
                var handler = DatumInserted;

                int xdItem = m_mapper.FromSourceIndex(xsDatum);
                handler?.Invoke(xdItem , datum);
            }
        }

        void OnDatumDeleted(int xsDatum)
        {
            if (m_mapper.Accepted(xsDatum))
            {
                int xdItem = m_mapper.FromSourceIndex(xsDatum);
                m_mapper.OnSourceItemDeleted(xsDatum);

                DatumDeleted?.Invoke(xdItem);
            }
            else
                m_mapper.OnSourceItemDeleted(xsDatum);
        }

        void OnDatumDeleting(int xsDatum)
        {
            if (m_mapper.Accepted(xsDatum))
            {
                int ndxItem = m_mapper.FromSourceIndex(xsDatum);

                DatumDeleting?.Invoke(ndxItem);
            }
        }

        IEnumerable<IDatum> Iterate(int ndx)
        {
            while (ndx < Count)
                yield return Get(ndx++);
        }

        private void RegisterHandlers()
        {
            m_source.DatumInserted += OnDatumInserted;
            m_source.DatumDeleted += OnDatumDeleted;
            m_source.DatumDeleting += OnDatumDeleting;
            m_source.DatumReplaced += OnDatumReplaced;
            m_source.DatumReplacing += OnDatumReplacing;
            m_source.SourceResetted += OnSourceResetted;
        }

        private void UnregisterHandlers()
        {
            m_source.DatumInserted -= OnDatumInserted;
            m_source.DatumDeleted -= OnDatumDeleted;
            m_source.DatumDeleting -= OnDatumDeleting;
            m_source.DatumReplaced -= OnDatumReplaced;
            m_source.DatumReplacing -= OnDatumReplacing;
            m_source.SourceResetted -= OnSourceResetted;
        }

        void Disconnect(bool closeAll = false)
        {
            lock (m_mapper)
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

                    m_source.Close();
                }
            }
        }
    }
}
