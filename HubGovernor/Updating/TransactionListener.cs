using DGD.HubCore.DB;
using DGD.HubCore.Updating;
using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Updating
{
    sealed class TransactionListener: IDisposable
    {
        public TransactionListener()
        {
            m_dataProvider = AppContext.TableManager.Transactions.DataProvider;
            m_dataProvider.Connect();
        }


        public bool IsDisposed { get; private set; }
        public bool IsListening { get; private set; }
        public bool Disabled { get; set; }


        public void Start()
        {
            Assert(IsListening == false);
            Assert(IsDisposed == false);


            IEnumerable<IDataTable> tables = AppContext.TableManager.DeployableTables.Add(
                AppContext.TableManager.TRLabels , AppContext.TableManager.TRSpotValues);

            foreach (IDataTable tbl in tables)
            {
                uint idTable = tbl.ID;

                Action<IDataRow> rowReplacing = delegate (IDataRow row)
                {
                    if (!Disabled)
                        m_replacingCache.Add(Tuple.Create(idTable , row));
                };

                Action<IDataRow> rowInserted = delegate (IDataRow row)
                {
                    if (!Disabled)
                    {
                        for (int i = 0; i < m_replacingCache.Count; ++i)
                            if (m_replacingCache[i].Item1 == idTable && m_replacingCache[i].Item2.ID == row.ID)
                            {
                                var obj = row as ITaggedRow;

                                if (obj == null || !obj.Disabled)
                                    LogAction(row , idTable , ActionCode_t.ReplaceRow);
                                else
                                    LogAction(row , idTable , ActionCode_t.DeleteRow);

                                m_replacingCache.RemoveAt(i);
                                return;
                            }

                        LogAction(row , idTable , ActionCode_t.AddRow);
                    }
                };

                Action<IDataRow> rowReplaced = delegate (IDataRow row)
                {
                    if (!Disabled)
                    {
                        for (int i = 0; i < m_replacingCache.Count; ++i)
                            if (m_replacingCache[i].Item1 == idTable && m_replacingCache[i].Item2.ID == row.ID)
                            {
                                var obj = row as ITaggedRow;

                                if (obj == null || !obj.Disabled)
                                    LogAction(row , idTable , ActionCode_t.ReplaceRow);
                                else
                                    LogAction(row , idTable , ActionCode_t.DeleteRow);

                                m_replacingCache.RemoveAt(i);
                                return;
                            }

                        LogAction(row , idTable , ActionCode_t.ReplaceRow);
                    }
                };

                Action<IDataRow> rowDeleted = delegate (IDataRow row)
                {
                    if (!Disabled)
                    {
                        for (int i = 0; i < m_replacingCache.Count; ++i)
                            if (m_replacingCache[i].Item1 == idTable && m_replacingCache[i].Item2.ID == row.ID)
                                return;

                        LogAction(row , idTable , ActionCode_t.DeleteRow);
                    }
                };


                var indexer = new KeyIndexer(tbl.DataProvider);
                indexer.Connect();
                m_dataIndexers.Add(indexer);

                indexer.DatumReplacing += rowReplacing;
                indexer.DatumDeleted += rowDeleted;
                indexer.DatumInserted += rowInserted;
                indexer.DatumReplaced += rowReplaced;
            }


            IsListening = true;
        }


        public void Dispose()
        {
            if (!IsDisposed)
            {

                m_dataIndexers.ForEach(ndxer => ndxer.Dispose());
                m_dataIndexers.Clear();
                m_dataProvider.Close();

                IsListening = false;
                IsDisposed = true;
            }
        }

        //private:
        readonly List<IKeyIndexer> m_dataIndexers = new List<IKeyIndexer>();
        readonly List<Tuple<uint , IDataRow>> m_replacingCache = new List<Tuple<uint , IDataRow>>();
        readonly IDatumProvider m_dataProvider;


        void LogAction(IDataRow row , uint idTable , ActionCode_t actionCode)
        {
            uint id = AppContext.TableManager.Transactions.CreateUniqID();
            var datum = new Transaction(id , idTable , row.ID , actionCode);

            m_dataProvider.Insert(datum);
        }
    }
}
