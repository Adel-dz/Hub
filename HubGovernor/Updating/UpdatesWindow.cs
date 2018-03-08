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
using easyLib.Log;

namespace DGD.HubGovernor.Updating
{
    public partial class UpdatesWindow: Form
    {
        readonly IDatumProvider m_dpDataUpdates;
        readonly IDatumProvider m_dpAppUpdates;

        public UpdatesWindow()
        {
            InitializeComponent();

            m_dpDataUpdates = AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT);
            m_dpAppUpdates = AppContext.AccessPath.GetDataProvider(InternalTablesID.APP_UPDATE);
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
        }

        void UnregisterHandlers()
        {
            AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).DatumInserted -= Transactions_DatumInserted;
        }

        void LoadAppUpdates()
        {
            Assert(!InvokeRequired);

            m_lvAppUpdates.Items.Clear();

            if(m_dpAppUpdates.Count > 0)
                foreach (AppUpdate update in m_dpAppUpdates.Enumerate())
                {
                    var lvi = new ListViewItem(update.Content) { Tag = update };
                    m_lvAppUpdates.Items.Add(lvi);
                }
        }

        void LoadDataUpdates()
        {
            Assert(!InvokeRequired);


            m_lvDataUpdates.Items.Clear();

            if (m_dpDataUpdates.Count > 0)
            {
                foreach (UpdateIncrement inc in m_dpDataUpdates.Enumerate())
                {
                    var lvi = new ListViewItem(inc.Content) { Tag = inc };
                    m_lvDataUpdates.Items.Add(lvi);
                }                
            }
        }

        //handlers
        private void BuildUpdate_Click(object sender , EventArgs e)
        {
            var dlg = new Jobs.ProcessingDialog();

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
            };

            var task = new Task(new UpdateBuilder().Run , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);

            task.Start();
            dlg.ShowDialog(Parent);

            LoadDataUpdates();
        }

        private void Updates_ItemActivate(object sender , EventArgs e)
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

        private void Transactions_DatumInserted(int ndx , easyLib.DB.IDatum datum)
        {
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
                    string src = System.IO.Path.Combine(AppPaths.DataUpdateFolder , fileName);
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

                LoadDataUpdates();
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

                        var update = new AppUpdate(AppContext.TableManager.AppUpdates.CreateUniqID() , dlg.Version.ToString());
                        bag.Compress(Path.Combine(AppPaths.AppUpdateFolder , update.ID.ToString("X")));

                        string manifestPath = AppPaths.LocalManifestPath;

                        try
                        {
                            IUpdateManifest oldManifest = UpdateEngin.ReadUpdateManifest(manifestPath);
                            var dict = new Dictionary<AppArchitecture_t , Version>(oldManifest.Versions.Count() + 1);
                            dict[dlg.AppArchitecture] = dlg.Version;

                            var newManifest = new UpdateManifest(oldManifest.UpdateKey , oldManifest.DataGeneration , dict);
                            UpdateEngin.WriteUpdateManifest(newManifest , manifestPath);
                        }
                        catch (Exception ex)
                        {
                            EventLogger.Warning(ex.Message);

                            var dict = new Dictionary<AppArchitecture_t , Version>
                            {
                                { dlg.AppArchitecture, dlg.Version }
                            };

                            var newManifest = new UpdateManifest(AppContext.Settings.AppSettings.UpdateKey ,
                                AppContext.Settings.AppSettings.DataGeneration , dict);

                            UpdateEngin.WriteUpdateManifest(newManifest , manifestPath);
                        }

                        dp.Insert(update);
                    };


                    Action<Task> onErr = t =>
                    {
                        EventLogger.Error(t.Exception.InnerException.Message);
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
            var dlg = new Jobs.ProcessingDialog();

            Action upload = () =>
            {
                KeyIndexer ndxerInc = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.APP_UPDATE);
                IEnumerable<uint> ids = from AppUpdate update in ndxerInc.Source.Enumerate()
                                        where update.IsDeployed == false
                                        select update.ID;

                var netEngin = new NetEngin(AppContext.Settings.AppSettings);

                foreach (var id in ids)
                {
                    string fileName = id.ToString("X");
                    string src = Path.Combine(AppPaths.AppUpdateFolder , fileName);
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

                LoadDataUpdates();
                m_tsbUploadDataUpdates.Enabled = false;
            };

            var task = new Task(upload , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            task.Start();
            dlg.ShowDialog(Parent);
        }
    }
}
