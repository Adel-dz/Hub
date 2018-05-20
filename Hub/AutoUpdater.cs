using System;
using System.Collections.Generic;
using System.IO;
using static System.Diagnostics.Debug;
using easyLib;
using DGD.HubCore.Updating;
using DGD.HubCore.DB;
using System.Linq;
using DGD.HubCore.Net;
using System.Reflection;
using DGD.HubCore;

namespace DGD.Hub
{
    static class AutoUpdater
    {
        public static event Action DataUpdated;
        public static event Action<uint> BeginTableUpdate;
        public static event Action<uint> EndTableUpdate;
        public static event Func<bool> CanDownlaodAppUpdate;
        public static event Func<bool> CanRunAppUpdate;

        public static bool UpdateData()
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
                var netEngin = new NetEngin(Program.NetworkSettings);
                netEngin.Download(manifest , Urls.ManifestURL);
                IUpdateManifest updateManifest = UpdateEngin.ReadUpdateManifest(manifest);

                string log = "Recherche de mise à jour des données. Version actulle des données: " + 
                    $"{Program.Settings.DataGeneration}. ";


                if (updateManifest.DataGeneration == Program.Settings.DataGeneration)
                {
                    Program.DialogManager.PostLog(log + " Les données sont à jour", false);
                    return true;
                }

                if (Program.Settings.UpdateKey != updateManifest.UpdateKey)
                {
                    if (Program.Settings.UpdateKey == 0)
                        Program.Settings.UpdateKey = updateManifest.UpdateKey;
                    else
                    {
                        Log.LogEngin.PushFlash(AppText.ERR_UPDATEKEY);
                        Dbg.Log("Update key mismatch!");

                        Program.DialogManager.PostLog(log + AppText.ERR_UPDATEKEY, true);

                        return false;
                    }
                }

                //TODO: signaler l'erreur si DataGeneration < manifest.DataGeneration            

                string dataManifest = Path.GetTempFileName();

                using (Log.LogEngin.PushMessage("Installation des mises à jour..."))
                using (new AutoReleaser(() => File.Delete(dataManifest)))
                {
                    netEngin.Download(dataManifest , Urls.DataManifestURL);

                    var uris = new List<UpdateURI>(UpdateEngin.ReadDataManifest(dataManifest , Program.Settings.DataGeneration));

                    foreach (UpdateURI uu in uris.OrderBy(u => u.DataPreGeneration))
                    {
                        if (uu.DataPreGeneration == Program.Settings.DataGeneration)
                        {
                            string updateFile = Path.GetTempFileName();
                            using (new AutoReleaser(() => File.Delete(updateFile)))
                            {
                                netEngin.Download(updateFile , Urls.DataUpdateDirURL + uu.FileURI);
                                ApplyUpdate(updateFile);
                                Program.Settings.DataGeneration = uu.DataPostGeneration;
                            }
                        }
                    }

                    DataUpdated?.Invoke();
                }

                Program.DialogManager.PostLog(log + "Mises à jour installées. "+
                    $"Nouvelle version des données: {Program.Settings.DataGeneration}" , false);

                Assert(Program.Settings.DataGeneration == updateManifest.DataGeneration);
            }

            return true;
        }

        public static void UpdateApp()
        {
            //dl global manifest
            Log.LogEngin.PushFlash("Rechercher d'une mise à jour de l'application...");
            const string logTxt = "Rechercher d'une mise à jour de l'application. ";

            string tmpFile = Path.GetTempFileName();
            using (new AutoReleaser(() => File.Delete(tmpFile)))
            {
                var netEngin = new NetEngin(Program.NetworkSettings);
                netEngin.Download(tmpFile , Urls.ManifestURL);
                IUpdateManifest updateManifest = UpdateEngin.ReadUpdateManifest(tmpFile);

                Version curVer = Assembly.GetExecutingAssembly().GetName().Version;
                Version ver = updateManifest.GetAppVersion(Program.AppArchitecture);

                if (ver == null || curVer.CompareTo(ver) >= 0)
                {
                    Log.LogEngin.PushFlash("Vous disposez déjà de la dernière version de l’application.");
                    Program.DialogManager.PostLog(logTxt + "Pas de nouvelle mise à jour" , false);
                    return;
                }

                Log.LogEngin.PushFlash($"Une nouvelle version de l'application est disponible ({ver}).");
                Program.DialogManager.PostLog(logTxt + 
                    $"Une nouvelle version de l'application est disponible, version: ({ver})", false);

                const string setupTxtLog = "L'utilsateur a refuser d'installer la nouvelle version du HUB";

                if (CanDownlaodAppUpdate?.Invoke() != true)
                {
                    Program.DialogManager.PostLog(setupTxtLog , false);
                    return;
                }

                Log.LogEngin.PushFlash($"Téléchargement de la mise à jour...");

                //dl app manifest
                netEngin.Download(tmpFile , Urls.AppManifestURL);
                Dictionary<AppArchitecture_t , string> upFiles = UpdateEngin.ReadAppManifest(tmpFile);
                string fileName = upFiles[Program.AppArchitecture];

                //dl update file
                var url = Urls.AppUpdateDirURL + fileName;
                netEngin.Download(tmpFile , url);

                if (CanRunAppUpdate?.Invoke() != true)
                {
                    Program.DialogManager.PostLog(setupTxtLog , false); 
                    return;
                }

                string tmpDir = Path.GetTempPath();
                new FilesBag().Decompress(tmpFile , tmpDir);
                System.Diagnostics.Process.Start(Path.Combine(tmpDir , "setup.exe"));
                Program.DialogManager.PostLog("Lancement du programme d'installation de la mise à jour du HUB" , false);
                System.Windows.Forms.Application.Exit();
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
