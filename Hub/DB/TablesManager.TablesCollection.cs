using DGD.HubCore.DB;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DGD.Hub.DB
{
    partial class TablesManager
    {
        class TablesCollection: ITablesCollection
        {
            readonly TablesManager m_owner;


            public TablesCollection(TablesManager owner)
            {
                m_owner = owner;
            }


            public IDBTable this[uint tableID]
            {
                get
                {
                    Monitor.Enter(m_owner.m_lock);
                    IDBTable tbl = m_owner.GetTable(tableID , true);
                    Monitor.Exit(m_owner.m_lock);

                    return tbl;
                }
            }

            public IEnumerator<IDBTable> GetEnumerator()
            {
                foreach (IDBTable tbl in m_owner.AllTables)
                {
                    lock (m_owner.m_lock)
                        if (!tbl.IsConnected)
                            tbl.Connect();

                    yield return tbl;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
