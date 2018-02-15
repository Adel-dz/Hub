using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{

    public interface IDBProvider: IWeakProvider<IDataRow>, IDisposable
    {
        event Action<int , IDataRow> DatumInserted;
        event Action<int[] , IDataRow[]> DataInserted;
        event Action<int , IDataRow> DatumReplacing;
        event Action<int , IDataRow> DatumReplaced;
        event Action<int> DatumDeleting;
        event Action<int> DatumDeleted;
        event Action<int[]> DataDeleting;
        event Action<int[]> DataDeleted;
        event Action SourceCleared;


        IDBSource DataSource { get; }

        IDataRow[] Get(int[] indices); //pre: IsConnected, indices != null, !indices.Any(x => x < 0 || RowCount <= x)
        void Insert(IDataRow[] data);   // pre: IsConnected, data != null, data.Count(d => d == null) == 0
        void Delete(int[] indices); //pre: IsConnected, indices != null, indices.Count(x=> x >= Count &&  x < 0) == 0
        IDisposable Lock();
    }




    public class DBProvider: IDBProvider
    {
        readonly IDBProvider m_src;
        readonly ProviderMapper<IDataRow> m_mapper;    


        public event Action<int[]> DataDeleting;
        public event Action<int[]> DataDeleted;
        public event Action<int> DatumDeleting;
        public event Action<int> DatumDeleted;
        public event Action<int , IDataRow> DatumInserted;
        public event Action<int[] , IDataRow[]> DataInserted;
        public event Action<int , IDataRow> DatumReplacing;
        public event Action<int , IDataRow> DatumReplaced;
        public event Action SourceCleared;

        
        public DBProvider(DBProvider srcData , Predicate<IDataRow> filter ,
            AggregationMode_t mode = AggregationMode_t.Rejected)
        {
            Assert(srcData != null);
            Assert(filter != null);

            m_src = srcData;
            m_mapper = new ProviderMapper<IDataRow>(srcData , filter , mode);
        }


        public IDBSource DataSource => m_src.DataSource;
        public bool IsConnected { get; private set; }

        public int Count
        {
            get
            {
                Assert(IsConnected);
                return m_src.Count;
            }
        }


        public void Dispose() => m_src.Dispose();

        public void Delete(int ndx)
        {
            Assert(IsConnected);
            Assert(ndx < Count);
            Assert(0 <= ndx);

            int srcIndex = m_mapper.ToSourceIndex(ndx);
            m_src.Delete(srcIndex);
        }

        public void Delete(int[] indices)
        {
            Assert(IsConnected);
            Assert(indices != null);
            Assert(indices.Count(i => Count <= i || i < 0) == 0);

            IDisposable releaser = m_mapper.Lock();
            int[] srcIndices = indices.Select(n => m_mapper.ToSourceIndex(n)).ToArray();
            releaser.Dispose();

            m_src.Delete(srcIndices);
        }

        public IDataRow Get(int ndx)
        {
            Assert(IsConnected);
            Assert(ndx < Count);
            Assert(ndx >= 0);

            int srcIndex = m_mapper.ToSourceIndex(ndx);
            return m_src.Get(srcIndex);
        }

        public IDataRow[] Get(int[] indices)
        {
            Assert(IsConnected);
            Assert(indices != null);
            Assert(!indices.Any(x => x < 0 || Count <= x));

            IDisposable releaser = m_mapper.Lock();
            var srcIndices = indices.Select(x => m_mapper.ToSourceIndex(x)).ToArray();
            releaser.Dispose();

            return m_src.Get(srcIndices);
        }

        public void Insert(IDataRow datum)
        {
            Assert(IsConnected);
            Assert(datum != null);

            m_src.Insert(datum);
        }

        public void Insert(IDataRow[] data)
        {
            Assert(IsConnected);
            Assert(data.Count(d => d != null) == 0);

            m_src.Insert(data);
        }

        public void Replace(int ndx , IDataRow datum)
        {
            Assert(IsConnected);
            Assert(ndx < Count);
            Assert(0 <= ndx);
            Assert(datum != null);

            int srcIndex = m_mapper.ToSourceIndex(ndx);
            m_src.Replace(srcIndex , datum);
        }

        public IEnumerable<IDataRow> Enumerate()
        {
            Assert(IsConnected);

            return m_src.Enumerate();
        }

        public IEnumerable<IDataRow> Enumerate(int ndxFirst)
        {
            Assert(IsConnected);
            Assert(ndxFirst < Count);
            Assert(ndxFirst >= 0);

            return m_src.Enumerate(ndxFirst);
        }
        
        public IDisposable Lock() => m_src.Lock();


        //private:
        void RegisterHandlers()
        {
            m_src.DatumDeleting += Source_DatumDeleting;
            m_src.DatumDeleted += Source_DatumDeleted;
            m_src.DatumInserted += Source_DatumInserted;
            m_src.DatumReplacing += Source_DatumReplacing;
            m_src.DatumReplaced += Source_DatumReplaced;
            m_src.DataDeleting += Source_DataDeleting;
            m_src.DataDeleted += Source_DataDeleted;
            m_src.DataInserted += Source_DataInserted;
            m_src.SourceCleared += Source_Cleared;
        }

        void UnregisterHandlers()
        {
            m_src.DatumDeleting -= Source_DatumDeleting;
            m_src.DatumDeleted -= Source_DatumDeleted;
            m_src.DatumInserted -= Source_DatumInserted;
            m_src.DatumReplacing -= Source_DatumReplacing;
            m_src.DatumReplaced -= Source_DatumReplaced;
            m_src.DataDeleting -= Source_DataDeleting;
            m_src.DataDeleted -= Source_DataDeleted;
            m_src.DataInserted -= Source_DataInserted;
            m_src.SourceCleared -= Source_Cleared;
        }

        void Source_DatumInserted(int srcIndex , IDataRow datum)
        {
            Assert(datum != null);

            var handler = DatumInserted;

            m_mapper.OnSourceItemInserted(srcIndex , datum);

            if (handler != null && m_mapper.Filter(datum))
            {
                int ndx = m_mapper.FromSourceIndex(srcIndex);
                handler(ndx , datum);
            }

        }

        void Source_DataInserted(int[] srcIndices , IDataRow[] data)
        {
            Assert(srcIndices != null);
            Assert(data != null);
            Assert(srcIndices.Length == data.Length);
            Assert(data.Count(d => d == null) == 0);

            for (int i = 0; i < data.Length; ++i)
                m_mapper.OnSourceItemInserted(srcIndices[i] , data[i]);


            var handler = DataInserted;

            if (handler != null)
            {
                var ndxList = new List<int>(srcIndices.Length);
                var dataList = new List<IDataRow>(data.Length);

                for (int i = 0; i < data.Length; ++i)
                    if (m_mapper.Filter(data[i]))
                    {
                        ndxList.Add(m_mapper.FromSourceIndex(srcIndices[i]));
                        dataList.Add(data[i]);
                    }

                handler(ndxList.ToArray() , dataList.ToArray());
            }
        }

        void Source_DatumReplaced(int srcIndex , IDataRow datum)
        {
            Assert(datum != null);

            bool wasIncluded = m_mapper.Accepted(srcIndex);

            if (wasIncluded)
            {
                int ndx = m_mapper.FromSourceIndex(srcIndex);
                m_mapper.OnSourceItemReplaced(srcIndex , datum);

                if (m_mapper.Filter(datum))
                    DatumReplaced?.Invoke(ndx , datum);
                else
                    DatumDeleted?.Invoke(ndx);
            }
            else
            {
                m_mapper.OnSourceItemReplaced(srcIndex , datum);

                if (m_mapper.Filter(datum))
                {
                    int ndx = m_mapper.FromSourceIndex(srcIndex);
                    DatumInserted?.Invoke(ndx , datum);
                }
            }
        }

        void Source_DatumDeleted(int srcIndex)
        {
            var handler = DatumDeleted;

            if (handler != null && m_mapper.Accepted(srcIndex))
            {
                int ndx = m_mapper.FromSourceIndex(srcIndex);

                m_mapper.OnSourceItemDeleted(srcIndex);
                handler(ndx);
            }
            else
                m_mapper.OnSourceItemDeleted(srcIndex);
        }

        void Source_DataDeleted(int[] srcIndices)
        {
            Assert(srcIndices != null);

            var handler = DataDeleted;

            if (handler == null)
                for (int i = 0; i < srcIndices.Length; i++)
                    m_mapper.OnSourceItemDeleted(srcIndices[i]);
            else
            {
                var indices = new List<int>();

                for (int i = 0; i < srcIndices.Length; ++i)
                {
                    int srcIndex = srcIndices[i];

                    if (m_mapper.Accepted(srcIndex))
                        indices.Add(m_mapper.FromSourceIndex(srcIndex));

                    m_mapper.OnSourceItemDeleted(srcIndex);
                }

                if (indices.Count > 0)
                    handler(indices.ToArray());
            }
        }

        void Source_DataDeleting(int[] srcIndices)
        {
            var handler = DataDeleting;

            if (handler != null)
            {
                var lst = new List<int>(srcIndices.Length);

                for (int i = 0; i < srcIndices.Length; ++i)
                {
                    int srcIndex = srcIndices[i];

                    if (m_mapper.Accepted(srcIndex))
                    {
                        int ndx = m_mapper.FromSourceIndex(srcIndex);
                        lst.Add(ndx);
                    }
                }

                if (lst.Count > 0)
                    handler(lst.ToArray());
            }
        }

        void Source_DatumReplacing(int srcIndex , IDataRow datum)
        {
            if (m_mapper.Accepted(srcIndex))
            {
                int ndx = m_mapper.FromSourceIndex(srcIndex);

                if (m_mapper.Filter(datum))
                    DatumReplacing?.Invoke(ndx , datum);
                else
                    DatumDeleting?.Invoke(ndx);
            }
        }

        void Source_DatumDeleting(int ndxSource)
        {
            var handler = DatumDeleting;

            if (handler != null && m_mapper.Accepted(ndxSource))
            {
                int ndx = m_mapper.FromSourceIndex(ndxSource);
                handler(ndx);
            }

        }

        private void Source_Cleared()
        {
            m_mapper.Reset();
            SourceCleared?.Invoke();                                    
        }
    }
}
