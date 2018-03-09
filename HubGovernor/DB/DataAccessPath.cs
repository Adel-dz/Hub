using DGD.HubCore.DB;
using easyLib.DB;
using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.DB
{
    sealed class DataAccessPath : IDisposable
    {
        readonly object m_lock = new object();
        readonly List<IDatumProvider> m_providers = new List<IDatumProvider>();
        readonly List<KeyIndexer> m_keysIndexers = new List<KeyIndexer>();


        public KeyIndexer GetKeyIndexer(uint idTable)
        {
            Assert(AppContext.TableManager.GetTable(idTable) != null);

            lock(m_lock)
            {
                KeyIndexer ndxer = m_keysIndexers.Find(x => x.Source.DataSource.ID == idTable);


                if(ndxer == null)
                {
                    ndxer = new KeyIndexer(GetDataProvider(idTable));
                    ndxer.Connect();

                    m_keysIndexers.Add(ndxer);
                }

                return ndxer;
            }
        }

        public IDatumProvider GetDataProvider(uint idTable)
        {
            Assert(AppContext.TableManager.GetTable(idTable) != null);

            lock (m_lock)
            {
                IDatumProvider dp = m_providers.Find(item => item.DataSource.ID == idTable);

                if (dp == null)
                {
                    dp = AppContext.TableManager.GetTable(idTable).DataProvider;
                    dp.Connect();

                    m_providers.Add(dp);
                }

                return dp;
            }
        }

        public void Dispose()
        {
            lock (m_lock)
            {
                foreach (KeyIndexer ndxer in m_keysIndexers)
                    ndxer.Close();

                m_keysIndexers.Clear();

                foreach (IDatumProvider dp in m_providers)
                    dp.Close();

                m_providers.Clear();
            }
        }
    }
}
