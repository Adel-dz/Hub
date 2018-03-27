using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{
    partial class DBTable<T>
    {
        class RowProvider: IDBTableProvider
        {
            readonly DBTable<T> m_table;
            readonly object m_lock = new object();


            public event Action<int[]> DataDeleted;
            public event Action<int[] , IDataRow[]> DataInserted;
            public event Action<int> DatumDeleted;
            public event Action<int , IDataRow> DatumInserted;
            public event Action<int , IDataRow> DatumReplaced;
            public event Action<int , IDataRow> DatumReplacing;
            public event Action<int> DatumDeleting;
            public event Action<int[]> DataDeleting;
            public event Action SourceCleared;

            public RowProvider(DBTable<T> table)
            {
                m_table = table;
            }


            public IDBSource DataSource => m_table;
            public bool IsConnected { get; private set; }

            public int Count
            {
                get
                {
                    Assert(IsConnected);

                    return m_table.RowCount;
                }
            }

            public bool AutoFlush { get; set; }


            public void Connect()
            {
                Assert(!IsConnected);

                lock (m_lock)
                {
                    if (!m_table.IsConnected)
                        m_table.Connect();

                    RegisterHandlers();
                    IsConnected = true;

                    ProvidersTracker.RegisterProvider(this , m_table.Name);
                }
            }

            public void Disconnect()
            {
                lock (m_lock)
                    if (IsConnected)
                    {
                        m_table.Flush();

                        m_table.Disconnect();
                        UnregisterHandlers();
                        IsConnected = false;

                        ProvidersTracker.UnregisterProvider(this);
                    }
            }

            public void Delete(int[] indices)
            {
                Assert(IsConnected);
                Assert(indices != null);
                Assert(indices.Count(x => x < 0 || Count <= x) == 0);

                lock (m_lock)
                {
                    DataDeleting?.Invoke(indices);
                    m_table.Delete(indices);

                    if (AutoFlush)
                        m_table.Flush();
                }
            }

            public void Delete(int ndx)
            {
                Assert(IsConnected);
                Assert(ndx < Count);
                Assert(0 <= ndx);

                lock (m_lock)
                {
                    DatumDeleting?.Invoke(ndx);
                    m_table.Delete(ndx);

                    if (AutoFlush)
                        m_table.Flush();
                }
            }

            public IDataRow Get(int ndx)
            {
                Assert(IsConnected);
                Assert(ndx < Count);
                Assert(0 <= ndx);

                IDataRow row;

                lock (m_lock)
                    row = m_table.Get(ndx);

                return row;
            }

            public IDataRow[] Get(int[] indices)
            {
                Assert(IsConnected);
                Assert(indices != null);
                Assert(!indices.Any(x => x < 0 || Count <= x));

                IDataRow[] rows;
                lock (m_lock)
                    rows = m_table.Get(indices).Cast<IDataRow>().ToArray();

                return rows;
            }

            public void Insert(IDataRow[] data)
            {
                Assert(IsConnected);
                Assert(data != null);
                Assert(data.Count(d => d == null) == 0);
                Assert(data.OfType<T>().Count() == data.Count());

                lock (m_lock)
                {
                    m_table.Insert(data.Cast<T>().ToArray());

                    if (AutoFlush)
                        m_table.Flush();
                }

            }

            public void Insert(IDataRow datum)
            {
                Assert(IsConnected);
                Assert(datum != null);
                Assert(datum is T);

                lock (m_lock)
                {
                    m_table.Insert((T)datum);

                    if (AutoFlush)
                        m_table.Flush();
                }
            }

            public void Replace(int ndx , IDataRow datum)
            {
                Assert(IsConnected);
                Assert(ndx < Count);
                Assert(0 <= ndx);
                Assert(datum != null);
                Assert(datum is T);

                lock (m_lock)
                {
                    DatumReplacing?.Invoke(ndx , datum);
                    m_table.Replace(ndx , (T)datum);

                    if (AutoFlush)
                        m_table.Flush();
                }
            }

            public IEnumerable<IDataRow> Enumerate()
            {
                Assert(IsConnected);

                if (m_table.RowCount == 0)
                    return Enumerable.Empty<IDataRow>();

                return Enumerate(0);
            }

            public IEnumerable<IDataRow> Enumerate(int ndxFirst = 0)
            {
                Assert(IsConnected);
                Assert(ndxFirst < Count);
                Assert(0 <= ndxFirst);

                return Iterate(ndxFirst);
            }

            public void Dispose() => Disconnect();

            public IDisposable Lock()
            {
                Monitor.Enter(m_lock);

                return new AutoReleaser(() => Monitor.Exit(m_lock));
            }



            //private:
            void RegisterHandlers()
            {
                m_table.DatumDeleted += Table_DatumDeleted;
                m_table.DatumInserted += Table_DatumInserted;
                m_table.DatumReplaced += Table_DatumReplaced;
                m_table.DataDeleted += Table_DataDeleted;
                m_table.DataInserted += Table_DataInserted;
                m_table.SourceCleared += Table_SourceCleared;
            }

            void UnregisterHandlers()
            {
                m_table.DatumDeleted -= Table_DatumDeleted;
                m_table.DatumInserted -= Table_DatumInserted;
                m_table.DatumReplaced -= Table_DatumReplaced;
                m_table.DataDeleted -= Table_DataDeleted;
                m_table.DataInserted -= Table_DataInserted;
                m_table.SourceCleared -= Table_SourceCleared;
            }

            IEnumerable<IDataRow> Iterate(int ndx)
            {
                while (ndx < Count)
                    yield return Get(ndx++);
            }

            void Table_DataInserted(int[] indices , T[] data) => DataInserted?.Invoke(indices , 
                Array.ConvertAll(data , d => (IDataRow)d));

            void Table_DataDeleted(int[] indices) => DataDeleted?.Invoke(indices);
            void Table_DatumReplaced(int ndx , T datum) => DatumReplaced?.Invoke(ndx , datum);
            void Table_DatumInserted(int ndx , T datum) => DatumInserted?.Invoke(ndx , datum);
            void Table_DatumDeleted(int ndx) => DatumDeleted?.Invoke(ndx);
            void Table_SourceCleared() => SourceCleared?.Invoke();
        }
    }
}
