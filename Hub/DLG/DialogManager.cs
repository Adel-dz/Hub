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
using System.Threading.Tasks;



namespace DGD.Hub.DLG
{
    sealed partial class DialogManager: IDisposable
    {
        readonly Timer m_dialogTimer;
        readonly Timer m_updateTimer;
        readonly Dictionary<Message_t , Func<Message , Message>> m_msgHandlersTable;
        ClientInfo m_clInfo;
        ClientStatus_t m_clStatus = ClientStatus_t.Unknown;
        uint m_lastSrvMsgID;
        bool m_needUpload;


        public DialogManager()
        {
            m_dialogTimer = new Timer(SettingsManager.DialogTimerInterval);
            m_dialogTimer.TimeElapsed += ProcessDialogTimer;

            m_updateTimer = new Timer(SettingsManager.UpdateTimerInterval);
            m_updateTimer.TimeElapsed += ProcessUpdateTimer;

            m_msgHandlersTable = new Dictionary<Message_t , Func<Message , Message>>
            {
                {Message_t.UnknonwnMsg, DefaultProcessing }
            };
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

            if (m_clInfo == null)
            {
                if (RegisterClient())
                {
                    m_clStatus = ClientStatus_t.Enabled;
                    m_dialogTimer.Start();
                    m_updateTimer.Start(true);
                }

                return;
            }

            //process only status part of the g file
            string tmpFile = Path.GetTempFileName();

            Action download = () =>
            {
                //download g file
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(tmpFile , SettingsManager.GetServerDialogURI(m_clInfo.ClientID) , true);

            };

            Action onSuccess = () =>
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                m_clStatus = clDlg.ClientStatus;

                if (m_clStatus == ClientStatus_t.Enabled)
                {
                    m_dialogTimer.Start();
                    m_updateTimer.Start(true);
                }
                else
                    if (m_clStatus == ClientStatus_t.Banned)
                    foreach (IDBTable tbl in Program.TablesManager.CriticalTables)
                    {
                        tbl.Clear();
                        Program.Settings.DataGeneration = 0;
                    }
                else
                    new ResumeHandler(ResumeResp , m_clInfo.ClientID).Start();

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

        public void Stop()
        {
            m_updateTimer.Stop();
            m_dialogTimer.Stop();
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_updateTimer.Dispose();
                m_dialogTimer.Dispose();
            }
        }


        //private:
        void ResumeResp(ResumeHandler.Result_t resp)
        {
            switch (resp)
            {
                case ResumeHandler.Result_t.Error:
                Dbg.Log("Error on resume req.");

                System.Windows.Forms.MessageBox.Show(
                    "Impossible de se connecter au serveur distant. Veuillez réessayer ultérieurement." ,
                    null ,
                    System.Windows.Forms.MessageBoxButtons.OK ,
                    System.Windows.Forms.MessageBoxIcon.Error);

                System.Windows.Forms.Application.Exit();
                break;

                case ResumeHandler.Result_t.Ok:
                m_dialogTimer.Start();
                m_updateTimer.Start(true);
                break;

                case ResumeHandler.Result_t.Rejected:
                Dbg.Log("Resume req. rejected!");
                break;

                default:
                Dbg.Assert(false);
                break;
            }
        }

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
            m_dialogTimer.Stop();

            Dbg.Log("Processing dialog timer...");

            Uri srvDlgURI = SettingsManager.GetServerDialogURI(m_clInfo.ClientID);
            string tmpFile = Path.GetTempFileName();

            LogEngin.PushFlash("Interrogation du serveur...");

            try
            {
                new NetEngin(Program.Settings).Download(tmpFile , srvDlgURI , true);
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);

                LogEngin.PushFlash(ex.Message);
                m_dialogTimer.Start();

                return;
            }


            try
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                if (m_clStatus != clDlg.ClientStatus)
                {
                    switch (clDlg.ClientStatus)
                    {
                        case ClientStatus_t.Enabled:
                        m_updateTimer.Start(true);
                        break;

                        case ClientStatus_t.Disabled:
                        if (m_clStatus == ClientStatus_t.Enabled)
                            m_updateTimer.Stop();
                        return;

                        case ClientStatus_t.Banned:
                        if (m_updateTimer.IsRunning)
                        {
                            m_updateTimer.Stop();

                            foreach (IDBTable tbl in Program.TablesManager.Tables)
                                tbl.Clear();
                        }
                        return;

                        default:
                        Dbg.Assert(false);
                        break;
                    }

                    m_clStatus = clDlg.ClientStatus;
                }



                Dbg.Assert(m_clStatus == ClientStatus_t.Enabled);

                var msgs = from msg in clDlg.Messages
                           where msg.ID >= m_lastSrvMsgID
                           select msg;

                var respList = new List<Message>();

                foreach (Message msg in msgs)
                {
                    Func<Message , Message> msgHandler;

                    if (!m_msgHandlersTable.TryGetValue(msg.MessageCode , out msgHandler))
                        msgHandler = DefaultProcessing;

                    Message resp = msgHandler(msg);

                    if (resp != null)
                        respList.Add(resp);

                    m_lastSrvMsgID = Math.Max(m_lastSrvMsgID , msg.ID);
                }

                string clFilePath = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);

                if (respList.Count > 0)
                {
                    DialogEngin.AppendHubDialog(clFilePath , m_clInfo.ClientID , respList);
                    m_needUpload = true;
                }

                if (m_needUpload)
                {
                    new NetEngin(Program.Settings).Upload(SettingsManager.GetClientDialogURI(m_clInfo.ClientID) , clFilePath , true);
                    m_needUpload = false;
                }

                //update g file
                string srvFilePath = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);
                File.Delete(srvFilePath);
                File.Move(tmpFile , srvFilePath);
                m_dialogTimer.Start();
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
                m_dialogTimer.Start();
            }
        }

        void ProcessUpdateTimer()
        {
            m_updateTimer.Stop();
            EventLogger.Debug("Processing update timer...");

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
                m_updateTimer.Start();
            }
        }

    }
}
