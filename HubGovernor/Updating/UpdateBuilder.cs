using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubCore.Updating;
using DGD.HubGovernor.DB;
using DGD.HubGovernor.Strings;
using easyLib;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Updating
{
    sealed class UpdateBuilder
    {
        class RowAction
        {
            public RowAction(uint rowID , ActionCode_t code)
            {
                RowID = rowID;
                ActionCode = code;
            }

            public uint RowID { get; }
            public ActionCode_t ActionCode { get; set; }
        }

        struct DatumInfo
        {
            public DatumInfo(IDataRow datum, int size)
            {
                Datum = datum;
                Size = size;
            }


            public int Size { get; }
            public IDataRow Datum { get; }
        }

        struct ActionInfo
        {
            public ActionInfo(IUpdateAction action, int szDatum)
            {
                Action = action;
                DatumSize = szDatum;
            }


            public IUpdateAction Action { get; }
            public int DatumSize { get; }
        }


        readonly System.IO.MemoryStream m_memStream = new System.IO.MemoryStream();
        readonly RawDataReader m_memReader;
        readonly RawDataWriter m_memWriter;
        static readonly Dictionary<ActionCode_t , int> m_actionPriority;

        static UpdateBuilder()
        {
            m_actionPriority = new Dictionary<ActionCode_t , int>()
            {
                { ActionCode_t.DeleteRow, 0 },
                { ActionCode_t.AddRow, 1 },
                { ActionCode_t.ReplaceRow, 2 }
            };
        }

        public UpdateBuilder()
        {
            m_memReader = new RawDataReader(m_memStream , System.Text.Encoding.UTF8);
            m_memWriter = new RawDataWriter(m_memStream , System.Text.Encoding.UTF8);
        }

        public void Run()
        {
            List<TableUpdate> tablesUpdate = BuildTablesUpdate();

            Opts.AppSettings opt = AppContext.Settings.AppSettings;

            var inc = new UpdateIncrement(AppContext.TableManager.DataUpdates.CreateUniqID() ,
                opt.DataGeneration);

            string incFileName = inc.ID.ToString("X");
            string incFilePath = System.IO.Path.Combine(AppPaths.DataUpdateFolder , incFileName);
            UpdateEngin.SaveTablesUpdate(tablesUpdate, incFilePath);
            AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT).Insert(inc);

            foreach (TableUpdate tu in tablesUpdate)
                AppContext.TableManager.SetTableGeneration(tu.TableID , tu.PostGeneration);

            AppContext.TableManager.Transactions.Reset();

            string dataMainfest = AppPaths.LocalDataManifestPath;
            UpdateEngin.UpdateDataManifest(dataMainfest , new UpdateURI(incFileName , opt.DataGeneration));

            if (opt.DataGeneration++ == 0)
                opt.UpdateKey = (uint)DateTime.Now.Ticks;

            AppContext.Settings.Save();

            string manifest = AppPaths.LocalManifestPath;
            UpdateEngin.WriteUpdateManifest(new UpdateManifest(opt.UpdateKey, opt.DataGeneration , 0) , manifest);                        
        }


        //private:
        List<TableUpdate> BuildTablesUpdate()
        {
            PrepareTables();
            
            Dictionary<uint , List<RowAction>> actions = new Dictionary<uint , List<RowAction>>();

            foreach (Transaction trans in AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).Enumerate())
            {
                List<RowAction> lst;

                if (!actions.TryGetValue(trans.TableID , out lst))
                {
                    lst = new List<RowAction>();
                    actions[trans.TableID] = lst;
                }

                RowAction ra = lst.Find(item => item.RowID == trans.RowID);

                if (ra == null)
                    lst.Add(new RowAction(trans.RowID , trans.Action));
                else
                    if (ra.ActionCode == ActionCode_t.AddRow && trans.Action == ActionCode_t.DeleteRow)
                    lst.Remove(ra);
                else
                    ra.ActionCode = SelectAction(ra.ActionCode , trans.Action);
            }


            return BuildTablesUpdate(actions);
        }
        
        static ActionCode_t SelectAction(ActionCode_t code1 , ActionCode_t code2)
        {
            return m_actionPriority[code1] < m_actionPriority[code2] ? code1 : code2;
        }

        ActionInfo BuildAction(uint idTable , uint rowID , ActionCode_t code)
        {
            switch (code)
            {
                case ActionCode_t.DeleteRow:
                return new ActionInfo(new DeleteRow(rowID), 0);

                case ActionCode_t.ReplaceRow:
                {
                    DatumInfo di = ReadDatum(idTable , rowID);
                    return new ActionInfo(new ReplaceRow(di.Datum) , di.Size); 
                }

                case ActionCode_t.AddRow:
                {
                    DatumInfo di = ReadDatum(idTable , rowID);
                    return new ActionInfo(new AddRow(di.Datum) , di.Size);
                }

                default:
                Assert(false);
                break;
            }

            return new ActionInfo();
        }

        List<TableUpdate> BuildTablesUpdate(Dictionary<uint , List<RowAction>> tablesTrans)
        {
            var keys = tablesTrans.Keys;
            var updates = new List<TableUpdate>(keys.Count);

            foreach (uint idTable in keys)
            {
                IEnumerable<ActionInfo> infos = from RowAction ra in tablesTrans[idTable]
                              select BuildAction(idTable , ra.RowID , ra.ActionCode);

                int maxSize = 0;
                var lst = new List<IUpdateAction>();

                foreach(ActionInfo ai in infos)
                {
                    if (ai.DatumSize > maxSize)
                        maxSize = ai.DatumSize;

                    lst.Add(ai.Action);
                }


                var tableUpdate =
                    new TableUpdate(idTable , lst , maxSize, AppContext.TableManager.GetTableGeneration(idTable));

                updates.Add(tableUpdate);
            }

            return updates;
        }

        static void PrepareTables()
        {
            const int NDX_LABEL = 0;
            const int NDX_SPOT_VALUE = 1;

            IDatumProvider dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION);
            List<RowAction>[] actions = { new List<RowAction>() , new List<RowAction>() };

            using (var pump = new DataPump(dp))
            {
                pump.Connect();

                DataPump.Item item = pump.NextItem;

                while (item != null)
                {
                    var trans = item.Row as Transaction;
                    int ndx = trans.TableID == InternalTablesID.TR_LABEL ? NDX_LABEL :
                        trans.TableID == InternalTablesID.TR_SPOT_VALUE ? NDX_SPOT_VALUE : -1;

                    if (ndx != -1)
                    {
                        RowAction ra = actions[ndx].Find(a => a.RowID == trans.RowID);

                        if (ra == null)
                            actions[ndx].Add(new RowAction(trans.RowID , trans.Action));
                        else if (ra.ActionCode == ActionCode_t.AddRow && trans.Action == ActionCode_t.DeleteRow)
                            actions[ndx].Remove(ra);
                        else
                            ra.ActionCode = SelectAction(ra.ActionCode , trans.Action);

                        dp.Delete(item.Index);
                    }

                    item = pump.NextItem;
                }
            }

            foreach (RowAction ra in actions[NDX_LABEL])
                UpdateTextTable(ra.RowID , ra.ActionCode);

            foreach (RowAction ra in actions[NDX_SPOT_VALUE])
                UpdateSpotTable(ra.RowID , ra.ActionCode);
        }

        static void UpdateSpotTable(uint rowID , ActionCode_t code)
        {
            switch (code)
            {
                case ActionCode_t.DeleteRow:
                {
                    var trSpot =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).Get(rowID) as TR.SpotValue;

                    int ndx = AppContext.AccessPath.GetKeyIndexer(TablesID.SPOT_VALUE).IndexOf(trSpot.PublishedValueID);
                    AppContext.AccessPath.GetDataProvider(TablesID.SPOT_VALUE).Delete(ndx);
                }
                break;

                case ActionCode_t.ReplaceRow:
                {
                    var trSpot =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).Get(rowID) as TR.SpotValue;

                    var trProdMapping = AppContext.AccessPath.GetKeyIndexer(
                        InternalTablesID.TR_PRODUCT_MAPPING).Get(trSpot.ProductMappingID) as TR.ProductMapping;

                    var trLabel =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL).Get(trSpot.LabelID) as TR.TRLabel;

                    //LabelMapping.ID == TRLabel.ID
                    var trLabelMapping =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL_MAPPING).Get(trLabel.ID) as TR.LabelMapping;

                    if(trLabelMapping == null)
                    {
                        var sharedText = new SharedText(AppContext.TableManager.SharedTexts.CreateUniqID() , trLabel.Label);
                        AppContext.AccessPath.GetDataProvider(TablesID.SHARED_TEXT).Insert(sharedText);

                        trLabelMapping = new TR.LabelMapping(trLabel.ID , sharedText.ID);
                        AppContext.AccessPath.GetDataProvider(InternalTablesID.TR_LABEL_MAPPING).Insert(trLabelMapping);
                    }


                    var pubValue = new Spots.SpotValue(
                        trSpot.PublishedValueID ,
                        trSpot.Price ,
                        trSpot.Time ,
                        trProdMapping.ProductID ,
                        trProdMapping.ContextID ,
                        SuppliersID.TR ,
                        trLabelMapping.SharedTextID);

                    int ndx = AppContext.AccessPath.GetKeyIndexer(TablesID.SPOT_VALUE).IndexOf(trSpot.PublishedValueID);
                    AppContext.AccessPath.GetDataProvider(TablesID.SPOT_VALUE).Replace(ndx , pubValue);
                }

                break;

                case ActionCode_t.AddRow:
                {
                    var trSpot =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).Get(rowID) as TR.SpotValue;

                    var trProdMapping =
                        AppContext.AccessPath.GetKeyIndexer(
                            InternalTablesID.TR_PRODUCT_MAPPING).Get(trSpot.ProductMappingID) as TR.ProductMapping;

                    var trLabel =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL).Get(trSpot.LabelID) as TR.TRLabel;

                    //LabelMapping.ID == TRLabel.ID
                    var trLabelMapping =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL_MAPPING).Get(trLabel.ID) as TR.LabelMapping;

                    if (trLabelMapping == null)
                    {
                        var sharedText = new SharedText(AppContext.TableManager.SharedTexts.CreateUniqID() , trLabel.Label);
                        AppContext.AccessPath.GetDataProvider(TablesID.SHARED_TEXT).Insert(sharedText);

                        trLabelMapping = new TR.LabelMapping(trLabel.ID , sharedText.ID);
                        AppContext.AccessPath.GetDataProvider(InternalTablesID.TR_LABEL_MAPPING).Insert(trLabelMapping);
                    }

                    var pubValue = new Spots.SpotValue(
                        AppContext.TableManager.SpotValues.CreateUniqID() ,
                        trSpot.Price ,
                        trSpot.Time ,
                        trProdMapping.ProductID ,
                        trProdMapping.ContextID ,
                        SuppliersID.TR ,
                        trLabelMapping.SharedTextID);

                    AppContext.AccessPath.GetDataProvider(TablesID.SPOT_VALUE).Insert(pubValue);

                    trSpot.PublishedValueID = pubValue.ID;
                    int ndx = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).IndexOf(trSpot.ID);

                    AppContext.TransactionListener.Disabled = true;
                    AppContext.AccessPath.GetDataProvider(InternalTablesID.TR_SPOT_VALUE).Replace(ndx , trSpot);
                    AppContext.TransactionListener.Disabled = false;
                }
                break;

                default:
                break;
            }
        }

        static private void UpdateTextTable(uint rowID , ActionCode_t code)
        {
            var lbl = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL).Get(rowID) as TR.TRLabel;

            switch (code)
            {
                case ActionCode_t.ReplaceRow:
                {
                    // TRLabel.ID == TR.LabelMapping.ID
                    var trLabelMapping =
                        AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_LABEL_MAPPING).Get(lbl.ID) as TR.LabelMapping;

                    if (trLabelMapping != null)
                    {

                        int ndxTxt = AppContext.AccessPath.GetKeyIndexer(TablesID.SHARED_TEXT).IndexOf(trLabelMapping.SharedTextID);

                        var txtNew = new Strings.SharedText(trLabelMapping.SharedTextID , lbl.Label);
                        AppContext.AccessPath.GetDataProvider(TablesID.SHARED_TEXT).Replace(ndxTxt , txtNew);
                    }
                }
                break;

                default:
                Assert(code == ActionCode_t.AddRow);    //ce cas est traité dans UpdateSpotTable()
                break;
            }
        }

        DatumInfo ReadDatum(uint tableID, uint rowID)
        {
            IDataRow d = AppContext.AccessPath.GetKeyIndexer(tableID).Get(rowID);
            m_memStream.Position = 0;
            d.Write(m_memWriter);
            int sz = (int)m_memStream.Position;

            m_memStream.Position = 0;
            IDataRow datum = AppContext.DatumFactory.CreateDatum(tableID);
            datum.Read(m_memReader);

            return new DatumInfo(datum, sz);
        }
    }
}
