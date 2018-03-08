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
        public UpdatesWindow()
        {
            InitializeComponent();
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            LoadData();
            RegisterHandlers();

            m_tsbBuildUpdate.Enabled = AppContext.AccessPath.GetDataProvider(InternalTablesID.TRANSACTION).Count > 0;
            m_tsbUpload.Enabled = AppContext.AccessPath.GetDataProvider(
                InternalTablesID.INCREMENT).Enumerate().Cast<UpdateIncrement>().Where(inc =>
                inc.IsDeployed == false).Count() > 0;

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

        void LoadData()
        {
            Assert(!InvokeRequired);


            var dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT);

            m_lvUpdates.Items.Clear();

            if (dp.Count > 0)
            {
                foreach (UpdateIncrement inc in dp.Enumerate())
                {
                    var lvi = new ListViewItem(inc.Content) { Tag = inc };
                    m_lvUpdates.Items.Add(lvi);
                }

                m_sslUpdateKey.Text = $"Clé de mise à jour: {AppContext.Settings.AppSettings.UpdateKey}";
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
                m_tsbUpload.Enabled = true;
            };

            var task = new Task(new UpdateBuilder().Run , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);

            task.Start();
            dlg.ShowDialog(Parent);

            LoadData();
        }

        private void Updates_ItemActivate(object sender , EventArgs e)
        {
            var inc = m_lvUpdates.SelectedItems[0].Tag as UpdateIncrement;

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

        private void Upload_Click(object sender , EventArgs e)
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
                    Uri dst = new Uri(AppPaths.RemoteDataDirUri , fileName);
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

                LoadData();
                m_tsbUpload.Enabled = false;
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


                        bag.Compress(Path.Combine(AppPaths.AppUpdateFolder , update.ID.ToString()));

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

        private void Timer_Tick(object sender , EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
