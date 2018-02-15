using DGD.HubCore;
using DGD.HubCore.DB;
using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;


namespace DGD.Hub.DB
{
    partial class TablesManager
    {
        class DataAccessPath: IDisposable
        {
            class ColumnIndexerEntry<T>
            {
                public ColumnIndexerEntry(DBColumnIndexer<T> ndxer, ColumnID_t colID)
                {
                    Indexer = ndxer;
                    ColumnID = colID;
                }
                
                public DBColumnIndexer<T> Indexer { get; }
                public uint TableID => Indexer.Source.DataSource.ID;
                public ColumnID_t ColumnID { get; }
            }


            readonly TablesManager m_tblManager;
            readonly List<IDBTableProvider> m_providers = new List<IDBTableProvider>();
            readonly List<DBKeyIndexer> m_keysIndexers = new List<DBKeyIndexer>();
            List<ColumnIndexerEntry<SubHeading>> m_ndxersSubHeading;


            public DataAccessPath(TablesManager tableManager)
            {
                Assert(tableManager != null);

                m_tblManager = tableManager;
            }


            public DBKeyIndexer GetKeyIndexer(uint idTable)
            {
                Assert(m_tblManager.Tables[idTable] != null);

                DBKeyIndexer ndxer = m_keysIndexers.Find(x => x.Source.DataSource.ID == idTable);


                if (ndxer == null)
                {
                    ndxer = new DBKeyIndexer(m_tblManager , idTable);
                    m_keysIndexers.Add(ndxer);
                }

                if(!ndxer.IsConnected)
                    ndxer.Connect();

                return ndxer;
            }

            public bool IsIndexerRead(uint idTable) =>
                m_keysIndexers.Find(x => x.Source.DataSource.ID == idTable) != null;

            public IDBTableProvider GetDataProvider(uint idTable)
            {
                Assert(m_tblManager.Tables[idTable] != null);

                IDBTableProvider dp = m_providers.Find(item => item.DataSource.ID == idTable);

                if (dp == null)
                {
                    dp = m_tblManager.Tables[idTable].DataProvider;
                    m_providers.Add(dp);
                }

                if(!dp.IsConnected)
                    dp.Connect();

                return dp;
            }

            public bool IsProviderReady(uint idTable) =>
                m_providers.Find(item => item.DataSource.ID == idTable) != null;

            public DBColumnIndexer<SubHeading> GetSubHeadingIndxer(uint tableID, ColumnID_t colID)
            {
                if (m_ndxersSubHeading == null)
                    m_ndxersSubHeading = new List<ColumnIndexerEntry<SubHeading>>();

                ColumnIndexerEntry<SubHeading> entry = m_ndxersSubHeading.Find(e => e.TableID == tableID && e.ColumnID == colID);

                if(entry == null)
                {
                    entry = new ColumnIndexerEntry<SubHeading>(CreateSubHeadingIndexer(tableID , colID) , colID);
                    m_ndxersSubHeading.Add(entry);
                }

                if (!entry.Indexer.IsConnected)
                    entry.Indexer.Connect();

                return entry.Indexer;
            }

            public void Dispose()
            {
                if(m_ndxersSubHeading != null)
                    DisposeColumnIndexer(m_ndxersSubHeading);

                foreach (DBKeyIndexer ndxer in m_keysIndexers)
                    ndxer.Dispose();

                m_keysIndexers.Clear();
                
                foreach (IDBProvider dp in m_providers)
                    dp.Dispose();

                m_providers.Clear();
            }

            //private:
            DBColumnIndexer<SubHeading> CreateSubHeadingIndexer(uint tableID, ColumnID_t colID)
            {
                DBColumnIndexer<SubHeading> ndxer;

                switch(tableID)
                {

                    case TablesID.PRODUCT:
                    Assert(colID == ColumnID_t.SubHeading);

                    ndxer = new DBColumnIndexer<SubHeading>(m_tblManager , tableID , d => (d as Product).SubHeading);
                    break;

                    case TablesID.SPOT_VALUE:
                    Assert(colID == ColumnID_t.SubHeading);
                    ndxer = new DBColumnIndexer<SubHeading>(m_tblManager , tableID , d => (d as SpotValue).Product.SubHeading);
                    break;

                    default:
                    ndxer = null;
                    Assert(false);
                    break;
                }

                return ndxer;
            }

            void DisposeColumnIndexer<T>(List<ColumnIndexerEntry<T>> list)
            {
                foreach (ColumnIndexerEntry<T> entry in list)
                    entry.Indexer.Dispose();

                list.Clear();
            }
        }
    }
}
