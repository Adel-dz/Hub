using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;



namespace easyLib.DB
{
    partial class DataTable<T>
    {
        class RowProvider: IDatumProvider
        {
            readonly DataTable<T> m_table;
            readonly object m_lock = new object();
            int m_refCount;
           

            public event Action<int , IDatum> DatumInserted;
            public event Action<int> DatumDeleted;
            public event Action<int> DatumDeleting;
            public event Action<int , IDatum> DatumReplaced;
            public event Action<int , IDatum> DatumReplacing;
            public event Action SourceResetted;


            public RowProvider(DataTable<T> table)
            {
                Assert(table != null);

                m_table = table;
            }


            public IDataSource DataSource => m_table;
            public bool IsConnected { get; private set; }
            public bool IsDisposed { get; private set; }
            public bool AutoFlush { get; set; }

            public int Count
            {
                get
                {
                    Assert(IsConnected);

                    return m_table.RowCount;
                }
            }


            public void Connect()
            {
                Assert(!IsDisposed);

                lock (m_lock)
                {
                    if (m_refCount == 0)
                    {
                        if (!m_table.IsConnected)
                            m_table.Connect();

                        RegisterHandlers();

                        DebugHelper.RegisterProvider(this , m_table.Name);
                    }

                    ++m_refCount;
                    IsConnected = true;
                }
            }

            public void Close()
            {
                lock (m_lock)
                {
                    Disconnect();

                    if (m_refCount == 0)
                        Dispose();
                }
            }

            public void Insert(IDatum item)
            {
                Assert(IsConnected);
                Assert(item is T);

                m_table.InsertRow((T)item);

                if (AutoFlush)
                    m_table.Flush();
            }

            public void Delete(int ndxItem)
            {
                Assert(IsConnected);
                Assert(ndxItem < Count);               

                m_table.DeleteRow(ndxItem);

                if (AutoFlush)
                    m_table.Flush();
            }

            public IEnumerable<IDatum> Enumerate() => Count == 0 ? Enumerable.Empty<IDatum>() : Enumerate(0);

            public IEnumerable<IDatum> Enumerate(int ndxFirst)
            {
                Assert(IsConnected);
                Assert(ndxFirst < Count);                

                return Iterate(ndxFirst);
            }

            public IDatum Get(int ndxItem)
            {
                Assert(IsConnected);
                Assert(ndxItem < Count);
                
                return m_table.GetRow(ndxItem);
            }

            public void Replace(int ndxItem , IDatum item)
            {
                Assert(IsConnected);
                Assert(ndxItem < Count);
                Assert(item is T);                

                m_table.ReplaceRow(ndxItem , (T)item);

                if (AutoFlush)
                    m_table.Flush();
            }

            public void Dispose()
            {
                lock (m_lock)
                {
                    if (!IsDisposed)
                    {
                        if (m_refCount > 0)
                            Disconnect(true);

                        DatumInserted = DatumReplaced = DatumReplacing = null;
                        DatumDeleting = DatumDeleted = null;

                        DebugHelper.UnregisterProvider(this);

                        IsDisposed = true;
                    }

                }
            }


            //private:
            void OnSourceResetted()
            {
                SourceResetted?.Invoke();
            }

            void OnRowInserted(int ndx , IDatum row) 
            {
                DatumInserted?.Invoke(ndx , row);
            }

            void OnRowDeleted(int ndx)
            {
                DatumDeleted?.Invoke(ndx);
            }

            void OnRowDeleting(int ndx)
            {
                DatumDeleting?.Invoke(ndx);
            }

            void OnRowReplaced(int ndx , IDatum row)
            {
                DatumReplaced?.Invoke(ndx , row);
            }

            void OnRowReplacing(int ndx , IDatum row) 
            {
                DatumReplacing?.Invoke(ndx , row);
            }

            IEnumerable<IDatum> Iterate(int ndx)
            {
                while(ndx < Count)
                    yield return Get(ndx++);
            }

            private void RegisterHandlers()
            {
                m_table.RowInserted += OnRowInserted;
                m_table.RowDeleted += OnRowDeleted;
                m_table.RowDeleting += OnRowDeleting;
                m_table.RowReplaced += OnRowReplaced;
                m_table.RowReplacing += OnRowReplacing;
            }

            private void UnregisterHandlers()
            {
                m_table.RowInserted -= OnRowInserted;
                m_table.RowDeleted -= OnRowDeleted;
                m_table.RowDeleting -= OnRowDeleting;
                m_table.RowReplaced -= OnRowReplaced;
                m_table.RowReplacing -= OnRowReplacing;
            }

            void Disconnect(bool closeAll = false)
            {
                if (m_refCount > 0)
                {
                    m_table.Flush();

                    if (closeAll)
                        m_refCount = 1;

                    if (--m_refCount == 0)
                    {
                        UnregisterHandlers();
                        IsConnected = false;
                    }
                }
            }
        }
    }
}
