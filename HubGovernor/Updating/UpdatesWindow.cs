using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGD.HubGovernor.Extensions;
using easyLib.Extensions;
using static System.Diagnostics.Debug;
using DGD.HubCore.DB;
using DGD.HubCore.Net;
using easyLib.DB;
using DGD.HubCore.Updating;
using System.IO;
using DGD.HubCore;
using DGD.HubGovernor.Log;

namespace DGD.HubGovernor.Updating
{
    public partial class UpdatesWindow: Form
    {
        const string WIN7SP1_UPDATE_FILENAME = "hubw7";
        const string WIN7SP1X64_UPADTE_FILENAME = "hubw7x64";
        const string WINXP_UPADTE_FILENAME = "hubwxp";

        public UpdatesWindow()
        {
            InitializeComponent();
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            LoadDataUpdates();
            LoadAppUpdates();
            RegisterHandlers();

            m_tsbBuildUpdate.Enabled = AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).Count > 0;
            m_tsbUploadDataUpdates.Enabled = AppContext.AccessPath.GetDataProvider(
                InternalTablesID.INCREMENT).Enumerate().Cast<UpdateIncrement>().Where(inc =>
                inc.IsDeployed == false).Count() > 0;
            m_tsbUploadAppUpdates.Enabled = AppContext.AccessPath.GetDataProvider(InternalTablesID.APP_UPDATE).Enumerate().
                Cast<AppUpdate>().Count(up => up.DeployTime == AppUpdate.NOT_YET) > 0;

            m_sslUpdateKey.Text = $"Clé de mise à jour: {AppContext.Settings.AppSettings.UpdateKey}";

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            UnregisterHandlers();
        }

        //private:
        void RegisterHandlers()
        {
            AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).DatumInserted += Transactions_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumInserted += AppUpdates_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumDeleted += AppUpdates_DatumDeleted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.INCREMENT).DatumInserted += DataUpdates_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumReplaced += AppUpdates_DatumReplaced;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.INCREMENT).DatumReplaced += DataUpdates_DatumReplaced;
        }

        void UnregisterHandlers()
        {
            AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).DatumInserted -= Transactions_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumInserted -= AppUpdates_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.INCREMENT).DatumInserted -= DataUpdates_DatumInserted;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumReplaced -= AppUpdates_DatumReplaced;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.INCREMENT).DatumReplaced -= DataUpdates_DatumReplaced;
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE).DatumDeleted -= AppUpdates_DatumDeleted;
        }

        void LoadAppUpdates()
        {
            Assert(!InvokeRequired);

            m_lvAppUpdates.Items.Clear();

            IDatumProvider dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.APP_UPDATE);

            if (dp.Count > 0)
                foreach (AppUpdate update in dp.Enumerate())
                {
                    var lvi = new ListViewItem(update.Content) { Tag = update };
                    m_lvAppUpdates.Items.Add(lvi);
                }
        }

        void LoadDataUpdates()
        {
            Assert(!InvokeRequired);

            IDatumProvider dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT);

            m_lvDataUpdates.Items.Clear();

            if (dp.Count > 0)
            {
                foreach (UpdateIncrement inc in dp.Enumerate())
                {
                    var lvi = new ListViewItem(inc.Content) { Tag = inc };
                    m_lvDataUpdates.Items.Add(lvi);
                }
            }
        }

        void NormalizeAppUpdates(AppUpdate update)
        {
            KeyIndexer ndxer = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE);

            var seq = from AppUpdate up in ndxer.Source.Enumerate()
                      where up.AppArchitecture == update.AppArchitecture && up.Version.CompareTo(update.Version) > 0
                      select up;

            if (seq.Any())
                update.DeployTime = AppUpdate.NEVER;
            else
            {
                seq = from AppUpdate up in ndxer.Source.Enumerate()
                      where up.AppArchitecture == update.AppArchitecture &&
                        up.Version.CompareTo(update.Version) <= 0 &&
                        up.DeployTime == AppUpdate.NOT_YET
                      select up;

                foreach (AppUpdate up in seq.ToArray())
                {
                    var datum = new AppUpdate(up.ID , up.Version , up.AppArchitecture , up.CreationTime);
                    datum.DeployTime = AppUpdate.NEVER;
                    int ndx = ndxer.IndexOf(up.ID);
                    ndxer.Source.Replace(ndx , datum);
                }
            }
        }

     
        //handlers
        private void BuildDataUpdate_Click(object sender , EventArgs e)
        {
            var dlg = new Jobs.ProcessingDialog();
            bool firstUpdate = AppContext.Settings.AppSettings.UpdateKey == 0;

            Action<Task> onErr = (t) =>
            {
                dlg.Dispose();
                this.ShowError(t.Exception.InnerException.Message);
            };

            Action onSuccess = () =>
            {
                dlg.Dispose();
                m_tsbBuildUpdate.Enabled = false;
                m_tsbUploadDataUpdates.Enabled = true;

                if(firstUpdate)
                    m_sslUpdateKey.Text = 
                        $"Clé de mise à jour: {AppContext.Settings.AppSettings.UpdateKey}";

            };

            var task = new Task(new UpdateBuilder().Run , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);

            task.Start();
            dlg.ShowDialog(Parent);
        }

        private void Updates_DataItemActivate(object sender , EventArgs e)
        {
            var inc = m_lvDataUpdates.SelectedItems[0].Tag as UpdateIncrement;

            try
            {
                var updateView = new TableUpdateViewer(inc.ID);
                updateView.Show(Parent);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void Transactions_DatumInserted(int ndx , IDatum datum)
        {
            if (InvokeRequired)
                Invoke(new Action(() => m_tsbBuildUpdate.Enabled = true));
            else
                m_tsbBuildUpdate.Enabled = true;
        }

        private void UploadDataUpdates_Click(object sender , EventArgs e)
        {
            var dlg = new Jobs.ProcessingDialog();

            Action upload = () =>
            {
                KeyIndexer ndxerInc = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.INCREMENT);
                IEnumerable<uint> ids = from UpdateIncrement inc in ndxerInc.Source.Enumerate()
                                        where inc.IsDeployed == false
                                        select inc.ID;

                var netEngin = new NetEngin(AppContext.Settings.AppSettings);

                foreach (var id in ids)
                {
                    string fileName = id.ToString("X");
                    string src = Path.Combine(AppPaths.DataUpdateFolder , fileName);
                    Uri dst = new Uri(AppPaths.RemoteDataUpdateDirUri , fileName);
                    netEngin.Upload(dst , src);
                }

                netEngin.Upload(AppPaths.RemoteDataMainfestURI , AppPaths.LocalDataManifestPath);
                netEngin.Upload(AppPaths.RemoteManifestURI , AppPaths.LocalManifestPath);

                foreach (uint id in ids)
                {
                    var inc = ndxerInc.Get(id) as UpdateIncrement;
                    inc.DeployTime = DateTime.Now;
                    ndxerInc.Source.Replace(ndxerInc.IndexOf(id) , inc);
                }
            };

            Action<Task> onErr = t =>
            {
                dlg.Dispose();
                this.ShowError(t.Exception.InnerException.Message);
            };

            Action onSuccess = () =>
            {
                dlg.Dispose();
                m_tsbUploadDataUpdates.Enabled = false;

            };

            var task = new Task(upload , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            task.Start();
            dlg.ShowDialog(Parent);
        }

        private void AddPackage_Click(object sender , EventArgs e)
        {
            using (var dlg = new AppUpdateDialog())
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    IDatumProvider dp = null;
                    var waitDlg = new Jobs.ProcessingDialog();

                    Action buildUpdate = () =>
                    {
                        var bag = new FilesBag();

                        foreach (string file in dlg.Files)
                        {
                            waitDlg.Message = $"Préparation de {file}";
                            string relDir = Path.GetDirectoryName(file.Remove(0 , dlg.RootFolder.Length + 1));

                            if (relDir.Length > 0)
                                bag.Add(file , relDir);
                            else
                                bag.Add(file);
                        }

                        waitDlg.Message = "Compression en cours...";
                        dp = AppContext.TableManager.AppUpdates.DataProvider;
                        dp.Connect();

                        var update = new AppUpdate(AppContext.TableManager.AppUpdates.CreateUniqID() , dlg.Version);
                        bag.Compress(Path.Combine(AppPaths.AppUpdateFolder , update.ID.ToString("X")));

                        NormalizeAppUpdates(update);
                        dp.Insert(update);
                    };


                    Action<Task> onErr = t =>
                    {
                        TextLogger.Error(t.Exception.InnerException.Message);
                        this.ShowError(t.Exception.InnerException.Message);

                        dp?.Dispose();
                        waitDlg.Dispose();
                    };

                    Action onSuccess = () =>
                    {
                        dp?.Dispose();
                        waitDlg.Dispose();
                    };


                    var task = new Task(buildUpdate , TaskCreationOptions.LongRunning);
                    task.OnSuccess(onSuccess);
                    task.OnError(onErr);
                    task.Start();

                    waitDlg.ShowDialog(this);
                }
        }

        private void UploadAppUpdates_Click(object sender , EventArgs e)
        {
            var filesNames = new Dictionary<AppArchitecture_t , string>
                {
                    { AppArchitecture_t.Win7SP1, WIN7SP1_UPDATE_FILENAME },                    
                    { AppArchitecture_t.WinXP, WINXP_UPADTE_FILENAME }
                };


            var waitDlg = new Jobs.ProcessingDialog();


            Action run = () =>
            {
                KeyIndexer ndxer = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE);

                var seq = from AppUpdate up in ndxer.Source.Enumerate()
                          where up.DeployTime == AppUpdate.NOT_YET
                          select up;

                //maj app manifest + manifest global
                Dictionary<AppArchitecture_t , string> appManifest;

                try
                {
                    appManifest = UpdateEngin.ReadAppManifest(AppPaths.LocalAppManifestPath);

                }
                catch (Exception ex)
                {
                    TextLogger.Warning(ex.Message);
                    appManifest = new Dictionary<AppArchitecture_t , string>();
                }
                                


                IUpdateManifest gManifest;

                try
                {
                    gManifest = UpdateEngin.ReadUpdateManifest(AppPaths.LocalManifestPath);
                }
                catch (Exception ex)
                {
                    TextLogger.Warning(ex.Message);
                    gManifest = new UpdateManifest(0 , 0);
                }



                var netEngin = new NetEngin(AppContext.Settings.AppSettings);
                
                foreach (AppUpdate up in seq)
                {
                    gManifest.Versions[up.AppArchitecture] = up.Version;
                    appManifest[up.AppArchitecture] = filesNames[up.AppArchitecture];

                    string srcFileName = up.ID.ToString("X");
                    string destFileName = filesNames[up.AppArchitecture];
                    Uri dst = new Uri(AppPaths.RemoteAppUpdateDirUri , destFileName);

                    waitDlg.Message = $"Transfert du fichier {destFileName}. Cette opération peut durer plusieurs minutes.";
                    netEngin.Upload(dst , Path.Combine(AppPaths.AppUpdateFolder , srcFileName));
                    up.DeployTime = DateTime.Now;

                    ndxer.Source.Replace(ndxer.IndexOf(up.ID) , up);
                }

                waitDlg.Message = "Transfert du manifest des applications...";
                UpdateEngin.WriteAppManifest(AppPaths.LocalAppManifestPath , appManifest);
                netEngin.Upload(AppPaths.RemoteAppMainfestURI , AppPaths.LocalAppManifestPath);

                waitDlg.Message = "Transfert du manifest global...";
                UpdateEngin.WriteUpdateManifest(gManifest , AppPaths.LocalManifestPath);
                netEngin.Upload(AppPaths.RemoteManifestURI , AppPaths.LocalManifestPath);
            };


            Action onSucces = () =>
            {
                m_tsbUploadAppUpdates.Enabled = false;
                waitDlg.Dispose();
            };

            Action<Task> onErr = t =>
            {
                waitDlg.Dispose();
                this.ShowError(t.Exception.InnerException.Message);
                TextLogger.Error(t.Exception.InnerException.Message);
            };


            var task = new Task(run , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSucces);
            task.OnError(onErr);
            task.Start();

            waitDlg.ShowDialog(this);
        }

        private void DataUpdates_DatumInserted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(DataUpdates_DatumInserted) , row);
            else
            {
                var lvi = new ListViewItem(row.Content)
                {
                    Tag = row
                };

                m_lvDataUpdates.Items.Add(lvi);               
            }
        }

        private void AppUpdates_DatumInserted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(AppUpdates_DatumInserted) , row);
            else
            {
                var lvi = new ListViewItem(row.Content)
                {
                    Tag = row
                };

                m_lvAppUpdates.Items.Add(lvi);

                if ((row as AppUpdate).DeployTime == AppUpdate.NOT_YET)
                    m_tsbUploadAppUpdates.Enabled = true;
            }
        }

        private void AppUpdates_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(AppUpdates_DatumReplaced) , row);
            else
            {
                for (int i = 0; i < m_lvAppUpdates.Items.Count; ++i)
                {
                    if ((m_lvAppUpdates.Items[i].Tag as IDataRow).ID == row.ID)
                    {
                        var lvi = new ListViewItem(row.Content)
                        {
                            Tag = row
                        };

                        m_lvAppUpdates.Items[i] = lvi;
                        break;
                    }
                }
            }
        }

        private void DataUpdates_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(DataUpdates_DatumReplaced) , row);
            else
            {
                for (int i = 0; i < m_lvDataUpdates.Items.Count; ++i)
                {
                    if ((m_lvDataUpdates.Items[i].Tag as IDataRow).ID == row.ID)
                    {
                        var lvi = new ListViewItem(row.Content)
                        {
                            Tag = row
                        };

                        m_lvDataUpdates.Items[i] = lvi;
                        break;
                    }
                }
            }
        }

        private void AppUpdates_DatumDeleted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(AppUpdates_DatumDeleted) , row);
            else
            {
                for (int i = 0; i < m_lvAppUpdates.Items.Count; ++i)
                    if ((m_lvAppUpdates.Items[i].Tag as IDataRow).ID == row.ID)
                    {
                        m_lvAppUpdates.Items.RemoveAt(i);
                        break;
                    }
            }
        }

        private void AppUpdates_ItemActivate(object sender , EventArgs e)
        {
            var item = m_lvAppUpdates.SelectedItems[0].Tag as AppUpdate;

            var viewer = new AppUpdateViewer(item);
            viewer.Show(Owner);
        }
    }
}