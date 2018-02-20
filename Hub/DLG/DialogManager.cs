using DGD.Hub.Log;
using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace DGD.Hub.DLG
{
    sealed class DialogManager: IDisposable
    {
        readonly Timer m_dialogTimer;
        Timer m_updateTimer;
        ClientInfo m_clInfo;
        int m_dlgInterval;
        bool m_dlgTimerEnabled;
        bool m_resumeMode;
        bool m_updateTimerEnabled;


        public DialogManager()
        {
            m_dialogTimer = new Timer(obj => ProcessDialogTimer() , null , Timeout.Infinite , Timeout.Infinite);
            m_updateTimer = new Timer(obj => ProcessUpdateTimer() , null , Timeout.Infinite , Timeout.Infinite);
            m_dlgInterval = SettingsManager.DialogTimerInterval;
        }

        public bool IsDisposed { get; private set; }

        public IEnumerable<ProfileInfo> Profiles
        {
            get
            {
                string tmpFile = Path.GetTempFileName();

                using (new AutoReleaser(() => File.Delete(tmpFile)))
                using (LogEngin.PushMessage("Récupération de la liste des profils à partir du serveur des douanes…"))
                {
                    new NetEngin(Program.Settings).Download(tmpFile , SettingsManager.ProfilesURI);
                    return DialogEngin.ReadProfiles(tmpFile);
                }
            }
        }

        public void Start()
        {
            //client enregistre?
            m_clInfo = Program.Settings.ClientInfo;

            if (m_clInfo == null && Program.DialogManager.RegisterClient())
            {
                StartDialogTimer();
                StartUpdateTimer(true);

                return;
            }

            string tmpFile = Path.GetTempFileName();

            Action download = () =>
            {
                //maj du fichier dialog
                string dlgFilePath = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);
                
                var netEngin = new NetEngin(Program.Settings);

                netEngin.Download(dlgFilePath , SettingsManager.GetClientDialogURI(m_clInfo.ClientID) , true);
                netEngin.Download(tmpFile , SettingsManager.GetServerDialogURI(m_clInfo.ClientID) , true);

            };

            Action onSuccess = () =>
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                if (clDlg.ClientStatus == ClientStatus_t.Enabled)
                {
                    StartDialogTimer();
                    StartUpdateTimer(true);
                }
                else
                    if (clDlg.ClientStatus == ClientStatus_t.Banned)
                    foreach (IDBTable tbl in Program.TablesManager.CriticalTables)
                        tbl.Clear();
                else
                {
                    m_resumeMode = true;
                    PostResumeReqAsync();
                }

                File.Delete(tmpFile);
            };

            Action<Task> onErr = t =>
            {
                Dbg.Log(t.Exception.InnerException.Message);

                System.Windows.Forms.MessageBox.Show(
                    "Impossible de se connecter au serveur distant. Veuillez réessayer ultérieurement." ,
                    null ,
                    System.Windows.Forms.MessageBoxButtons.OK ,
                    System.Windows.Forms.MessageBoxIcon.Error);

                File.Delete(tmpFile);
                System.Windows.Forms.Application.Exit();
            };

            var task = new Task(download , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            task.Start();
        }



        void PostResumeReqAsync()
        {
                                                            
        }

        void ProcessStatus(ClientDialog clDlg)
        {

        }

        public void Stop()
        {
            StopDialogTimer();
            StopUpdateTimer();
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                StopDialogTimer();
                m_dialogTimer.Dispose();
            }
        }


        //private:
        bool RegisterClient()
        {
            var busyDlg = new BusyDialog();
            busyDlg.Message = "Initialisation...";


            Action<Task<IEnumerable<ProfileInfo>>> onSuccess = t => busyDlg.Dispose();

            Action<Task> onErr = t =>
            {
                busyDlg.Dispose();
                System.Windows.Forms.MessageBox.Show(t.Exception.InnerException.Message , null);
            };

            IEnumerable<ProfileInfo> profiles = null;
            var task = new Task<IEnumerable<ProfileInfo>>(() => profiles = Program.DialogManager.Profiles ,
                TaskCreationOptions.LongRunning);

            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            task.Start();
            busyDlg.ShowDialog();

            if (profiles == null)
            {
                System.Windows.Forms.Application.Exit();
                return false;
            }

            ClientInfo clInfo;

            using (var dlg = new ProfileDialog(profiles))
            {
                if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    System.Windows.Forms.Application.Exit();

                clInfo = ClientInfo.CreateClient(dlg.SelectedProfile.ProfileID);
                clInfo.ContaclEMail = dlg.ContactEMail;
                clInfo.ContactName = dlg.Contact;
                clInfo.ContactPhone = dlg.ContactPhone;
                clInfo.MachineName = Environment.MachineName;
            }


            using (var dlg = new ConnectionDialog(clInfo))
            {
                dlg.ShowDialog();

                if (!dlg.IsRegistered)
                {
                    System.Windows.Forms.Application.Exit();
                    return false;
                }

                m_clInfo = Program.Settings.ClientInfo;
            }

            return true;
        }

        void ProcessDialogTimer()
        {
            StopDialogTimer();

            Dbg.Log("Processing timer...");

            Uri srvDlgURI = SettingsManager.GetServerDialogURI(m_clInfo.ClientID);
            string tmpFile = Path.GetTempFileName();

            LogEngin.PushFlash("Interrogation du serveur...");

            try
            {
                new NetEngin(Program.Settings).Download(tmpFile , srvDlgURI);
                m_dlgInterval = SettingsManager.DialogTimerInterval;
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);

                LogEngin.PushFlash(ex.Message);
                m_dlgInterval += 60 * 1000;
                StartDialogTimer();

                return;
            }



            try
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                switch (clDlg.ClientStatus)
                {
                    case ClientStatus_t.Enabled:
                    if (!m_updateTimerEnabled)
                        StartUpdateTimer(true);

                    break;

                    case ClientStatus_t.Disabled:
                    if (m_updateTimerEnabled)
                        StopUpdateTimer();

                    break;

                    case ClientStatus_t.Banned:
                    if (m_updateTimerEnabled)
                    {
                        StopUpdateTimer();

                        foreach (HubCore.DB.IDBTable tbl in Program.TablesManager.Tables)
                            tbl.Clear();
                    }


                    break;

                    default:

                    break;
                }
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
            }
            finally
            {
                StartDialogTimer();
            }
        }

        void ProcessUpdateTimer()
        {
            StopUpdateTimer();

            try
            {
                LogEngin.PushFlash("Recherche des mises à jour...");

                try
                {
                    AutoUpdater.UpdateData();
                    LogEngin.PushFlash("Votre application est à jour.");
                }
                catch (Exception ex)
                {
                    LogEngin.PushFlash(ex.Message);
                }

                EventLogger.Debug("Update done!");
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
            }
            finally
            {
                StartUpdateTimer();
            }
        }

        void StartDialogTimer(bool startNow = false)
        {
            int interval = SettingsManager.DialogTimerInterval;

            lock (m_dialogTimer)
                if (!m_dlgTimerEnabled)
                {
                    m_dialogTimer.Change(startNow ? 0 : interval , interval);
                    m_dlgTimerEnabled = true;
                }
        }

        void StopDialogTimer()
        {
            lock (m_dialogTimer)
                if (m_dlgTimerEnabled)
                {
                    m_dialogTimer.Change(Timeout.Infinite , Timeout.Infinite);
                    m_dlgTimerEnabled = false;
                }
        }

        void StartUpdateTimer(bool startNow = false)
        {
            int interval = SettingsManager.UpdateTimerInterval;

            lock (m_updateTimer)
                if (!m_updateTimerEnabled)
                {
                    m_updateTimer.Change(startNow ? 0 : interval , interval);
                    m_updateTimerEnabled = true;
                }
        }

        void StopUpdateTimer()
        {
            lock (m_updateTimer)
                if (m_updateTimerEnabled)
                {
                    m_updateTimer.Change(Timeout.Infinite , Timeout.Infinite);
                    m_updateTimerEnabled = false;
                }
        }
    }
}
