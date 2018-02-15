using System;
using System.Collections.Generic;
using System.IO;
using static System.Diagnostics.Debug;
using easyLib;
using DGD.HubCore.Updating;
using DGD.HubCore.DB;
using System.Linq;
using DGD.HubCore.Net;

namespace DGD.Hub
{
    static class AutoUpdater
    {
        public static event Action<uint> BeginTableUpdate;
        public static event Action<uint> EndTableUpdate;

        public static void UpdateData()
        {
            /*
             * telecharger le fichier manifest
             * si DataGeneration > manifest.DataGeneration 
             *  signaler l'erreur a HubGovernor
             *  sortir
             * si DataGeneration < manifest.DataGeneration 
             *  telecharger manifest.data
             *  pour chaque entrées E dans manifest.data et tant que DataGeneration < manifest.DataGeneration
             *      si DataGeneration == E.PreDataGeneration
             *          Appliquer les maj des tables
             *          mettre a jours DataGeneration
             */

            string manifest = Path.GetTempFileName();
            using (new AutoReleaser(() => File.Delete(manifest)))
            {
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(manifest , Program.Settings.ManifestURI);
                uint updateDataGen = UpdateEngin.ReadUpdateManifest(manifest).DataGeneration;

                if (updateDataGen == Program.Settings.DataGeneration)
                    return;

                //TODO: signaler l'erreur si DataGeneration < manifest.DataGeneration            

                string dataManifest = Path.GetTempFileName();

                using(Log.LogEngin.PushMessage("Installation des mises à jour..."))
                using (new AutoReleaser(() => File.Delete(dataManifest)))
                {
                    netEngin.Download(dataManifest , Program.Settings.DataManifestURI);

                    var uris = new List<UpdateURI>(UpdateEngin.ReadDataManifest(dataManifest , Program.Settings.DataGeneration));

                    foreach (UpdateURI uu in uris.OrderBy(u => u.DataPreGeneration))
                    {
                        if (uu.DataPreGeneration == Program.Settings.DataGeneration)
                        {
                            string updateFile = Path.GetTempFileName();
                            using (new AutoReleaser(() => File.Delete(updateFile)))
                            {                                
                                netEngin.Download(updateFile , new Uri(Program.Settings.DataUpdateDirURI , uu.FileURI));                                
                                ApplyUpdate(updateFile);
                                Program.Settings.DataGeneration = uu.DataPostGeneration;
                            }                            
                        }
                    }                   
                }

                Assert(Program.Settings.DataGeneration == updateDataGen);
            }
        }


        //private;
        static void ApplyAction(IUpdateAction action , DBKeyIndexer ndxer)
        {
            switch (action.Code)
            {
                case ActionCode_t.DeleteRow:
                var delAct = action as DeleteRow;
                ndxer.Source.Delete(ndxer.IndexOf(delAct.RowID));
                break;

                case ActionCode_t.ReplaceRow:
                var replaceAct = action as ReplaceRow;
                ndxer.Source.Replace(ndxer.IndexOf(replaceAct.RowID) , replaceAct.Datum);
                break;

                case ActionCode_t.AddRow:
                var addAct = action as AddRow;
                ndxer.Source.Insert(addAct.Datum);
                break;

                default:
                Assert(false);
                break;
            }
        }

        static void ApplyUpdate(string updateFile)
        {
            IEnumerable<TableUpdate> updates = UpdateEngin.LoadTablesUpdate(updateFile , Program.TablesManager.DataFactory);

            foreach (TableUpdate update in updates)
            {
                IDBTable table = Program.TablesManager.Tables[update.TableID];
                //TODO: traiter le cas ou une version de table est manquante ex: 1,2 ,4

                if (table.Version != update.PreGeneration)
                    continue;

                using (new AutoReleaser(() => EndTableUpdate?.Invoke(table.ID)))
                {
                    BeginTableUpdate?.Invoke(table.ID);

                    if (update.DatumMaxSize > table.DatumSize)
                        Program.TablesManager.ResizeTable(table.ID , update.DatumMaxSize);

                    using (var ndxer = new DBKeyIndexer(Program.TablesManager , update.TableID))
                    {
                        ndxer.Connect();

                        foreach (IUpdateAction action in update.Actions)
                            ApplyAction(action , ndxer);
                    }

                    table.Version = update.PostGeneration;
                }
            }
        }
    }
}
