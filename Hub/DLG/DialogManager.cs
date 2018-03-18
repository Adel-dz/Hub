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
using System.Threading.Tasks;



namespace DGD.Hub.DLG
{
    sealed partial class DialogManager: IDisposable
    {
        const int TTL_MAX = 10;
        readonly object m_lock = new object();
        readonly Timer m_dialogTimer;
        readonly Timer m_updateTimer;
        readonly Dictionary<Message_t , Action<Message>> m_msgHandlersTable;
        ClientInfo m_clInfo;
        ClientStatus_t m_clStatus = ClientStatus_t.Unknown;
        uint m_srvLastMsgID;
        uint m_clientLastMsgID;
        int m_timeToLive = TTL_MAX;
        bool m_needUpload;
        bool m_dialogRunning;


        public DialogManager()
        {
            m_dialogTimer = new Timer(SettingsManager.DialogTimerInterval);
            m_dialogTimer.TimeElapsed += ProcessDialogTimer;

            m_updateTimer = new Timer(SettingsManager.UpdateTimerInterval);
            m_updateTimer.TimeElapsed += ProcessUpdateTimer;

            m_msgHandlersTable = new Dictionary<Message_t , Action<Message>>
            {
                {Message_t.Sync, SyncHandler },
                {Message_t.Null, NullHandler }
            };
        }

        public bool IsDisposed { get; private set; }
        public bool IsRunning { get; private set; }

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
            Dbg.Assert(IsRunning == false);

            IsRunning = true;

            Opts.SettingsView.ClientInfoChanged += SettingsView_ClientInfoChaned;

            //client enregistre?
            m_clInfo = Program.Settings.ClientInfo;

            if (m_clInfo == null)
            {
                if (RegisterClient())
                {
                    m_clStatus = ClientStatus_t.Enabled;
                    m_dialogTimer.Start();
                    m_updateTimer.Start(true);
                    m_dialogRunning = true;

                    var updateTask = new Task(AutoUpdater.UpdateApp , TaskCreationOptions.LongRunning);
                    updateTask.Start();
                }

                return;
            }


            DialogEngin.WriteHubDialog(SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID) , 
                m_clInfo.ClientID , Enumerable.Empty<Message>());
            

            //process only status part of the g file
            string tmpFile = Path.GetTempFileName();

            Action start = () =>
            {
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(tmpFile , SettingsManager.GetServerDialogURI(m_clInfo.ClientID) , true);
            };

            Action onSuccess = () =>
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                m_clStatus = clDlg.ClientStatus;

                if (m_clStatus == ClientStatus_t.Enabled)
                    new StartHandler(m_clInfo.ClientID , StartResp).Start();
                else if (m_clStatus == ClientStatus_t.Banned)
                {
                    foreach (IDBTable tbl in Program.TablesManager.CriticalTables)
                    {
                        tbl.Clear();
                        Program.Settings.DataGeneration = 0;
                    }

                    System.Windows.Forms.MessageBox.Show(AppText.ERR_BANNED , AppText.APP_NAME ,
                           System.Windows.Forms.MessageBoxButtons.OK , System.Windows.Forms.MessageBoxIcon.Error);
                    Exit();
                    return;
                }
                else
                    new ResumeHandler(ResumeResp , m_clInfo.ClientID).Start();

                File.Delete(tmpFile);
            };

            Action<Task> onErr = t =>
            {
                Dbg.Log(t.Exception.InnerException.Message);

                //assume client enabled
                m_clStatus = ClientStatus_t.Enabled;
                new StartHandler(m_clInfo.ClientID , StartResp).Start();

                //System.Windows.Forms.MessageBox.Show(
                //    "Impossible de se connecter au serveur distant. Veuillez réessayer ultérieurement." ,
                //    null ,
                //    System.Windows.Forms.MessageBoxButtons.OK ,
                //    System.Windows.Forms.MessageBoxIcon.Error);

                //File.Delete(tmpFile);
                //Exit();
            };

            var task = new Task(start , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            task.Start();

            var appUpdateTask = new Task(AutoUpdater.UpdateApp , TaskCreationOptions.LongRunning);
            appUpdateTask.Start();
        }


        public void PostMessage(Message_t msgCode , byte[] data = null , uint reqID = 0)
        {
            lock (m_lock)
            {
                Message msg = new Message(++m_clientLastMsgID , reqID , msgCode , data);

                DialogEngin.AppendHubDialog(SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID) ,
                    m_clInfo.ClientID , msg);

                m_needUpload = true;
            }
        }

        public void Stop(bool ignoreCloseNotification = false)
        {
            if (IsRunning)
            {
                m_updateTimer.Stop();
                m_dialogTimer.Stop();                

                Opts.SettingsView.ClientInfoChanged -= SettingsView_ClientInfoChaned;

                if (m_clInfo != null && !ignoreCloseNotification && m_dialogRunning)
                {
                    var thread = new System.Threading.Thread(PostCloseMessage);
                    thread.Start();
                }

                m_dialogRunning = false;
                IsRunning = false;
            }
        }

        public void Exit(bool ignoreCloseNotification = false)
        {
            Stop(ignoreCloseNotification);
            System.Windows.Forms.Application.Exit();
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_updateTimer.Dispose();
                m_dialogTimer.Dispose();
            }
        }

        public uint SendMessage(Message_t msgCode, byte[] data = null, uint reqID = 0)
        {
            Message msg;

            lock(m_lock)            
                msg = new Message(++m_clientLastMsgID , reqID , msgCode , data);

            
            try
            {
                Uri dest = SettingsManager.GetClientDialogURI(m_clInfo.ClientID);
                string src = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);

                DialogEngin.AppendHubDialog(src , m_clInfo.ClientID , msg);
                new NetEngin(Program.Settings).Upload(dest , src);

                return msg.ID;
            }            
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);                
            }

            return 0;
        }

        public Message ReceiveMessage(uint reqID)
        {
            string tmpFile = Path.GetTempFileName();
            Uri src = SettingsManager.GetServerDialogURI(m_clInfo.ClientID);

            try
            {
                new NetEngin(Program.Settings).Download(tmpFile , src);
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(tmpFile);

                Message msg = clDlg.Messages.SingleOrDefault(m => m.ReqID == reqID);
                return msg;
            }
            catch (Exception ex)
            {
                Dbg.Log(ex.Message);
            }
            finally
            {
                File.Delete(tmpFile);
            }

            return null;
        }

        //private:
        void StartResp(bool ok)
        {
            if (ok)
            {
                m_dialogTimer.Start();
                m_updateTimer.Start(true);
                m_dialogRunning = true;
            }
            else
            {
                System.Windows.Forms.Application.OpenForms[0].ShowError(
                    "Le serveur a rejeté la demande de connexion. Veuillez réessayer ultérieurement.");
                Exit();
            }
        }

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

                Exit();
                break;

                case ResumeHandler.Result_t.Ok:            
                m_dialogTimer.Start();
                m_updateTimer.Start(true);
                m_dialogRunning = true;
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

            if (profiles == null || !profiles.Any())
            {
                System.Windows.Forms.MessageBox.Show("Aucune réponse du serveur. Veuillez réessayer ultérieurement." ,
                    AppText.APP_NAME , System.Windows.Forms.MessageBoxButtons.OK , System.Windows.Forms.MessageBoxIcon.Error);

                Exit();
                return false;
            }

            ClientInfo clInfo;

            using (var dlg = new ProfileDialog(profiles))
            {
                if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    Exit();

                clInfo = ClientInfo.CreateClient(dlg.SelectedProfile.ProfileID);
                clInfo.ContaclEMail = dlg.ContactEMail;
                clInfo.ContactName = dlg.Contact;
                clInfo.ContactPhone = dlg.ContactPhone;
                //clInfo.MachineName = Environment.MachineName;
            }


            using (var dlg = new ConnectionDialog(clInfo))
            {
                dlg.ShowDialog();

                if (!dlg.IsRegistered)
                {
                    Exit();
                    return false;
                }

                m_clInfo = Program.Settings.ClientInfo;
                DialogEngin.WriteHubDialog(SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID) ,
                    m_clInfo.ClientID , Enumerable.Empty<Message>());
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
                        m_updateTimer.Stop();

                        foreach (IDBTable tbl in Program.TablesManager.Tables)
                            tbl.Clear();

                        System.Windows.Forms.MessageBox.Show(AppText.ERR_BANNED , AppText.APP_NAME ,
                            System.Windows.Forms.MessageBoxButtons.OK , System.Windows.Forms.MessageBoxIcon.Error);
                        Exit();
                        return;

                        default:
                        Dbg.Assert(false);
                        break;
                    }

                    m_clStatus = clDlg.ClientStatus;
                }



                Dbg.Assert(m_clStatus == ClientStatus_t.Enabled);

                uint id = m_srvLastMsgID;

                var msgs = from msg in clDlg.Messages
                           where msg.ID > id
                           select msg;

                if (msgs.Any())
                {
                    m_srvLastMsgID = msgs.Max(m => m.ID);

                    Action<Message> msgHandler;

                    foreach (Message msg in msgs)
                        if (m_msgHandlersTable.TryGetValue(msg.MessageCode , out msgHandler))
                            msgHandler.Invoke(msg);

                    if (m_needUpload)
                    {
                        string clFilePath = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);
                        new NetEngin(Program.Settings).Upload(SettingsManager.GetClientDialogURI(m_clInfo.ClientID) , clFilePath , true);
                        m_needUpload = false;
                    }

                    m_timeToLive = TTL_MAX;
                }

                if (--m_timeToLive <= 0)
                    PostSyncMessage();

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
                LogEngin.PushFlash("Recherche des mises à jour de données...");

                try
                {
                    if (AutoUpdater.UpdateData())
                        LogEngin.PushFlash("Vos données sont à jour.");

                    EventLogger.Debug("Update done!");
                }
                catch (Exception ex)
                {
                    LogEngin.PushFlash(ex.Message);
                }

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

        void PostCloseMessage()
        {
            Dbg.Log("Posting closing notification...");

            var req = new Message(++m_clientLastMsgID , 0 , Message_t.Close);
            string dlgFile = SettingsManager.GetClientDialogFilePath(m_clInfo.ClientID);
            DialogEngin.AppendHubDialog(dlgFile , m_clInfo.ClientID , req);

            try
            {
                new NetEngin(Program.Settings).Upload(SettingsManager.GetClientDialogURI(m_clInfo.ClientID) , dlgFile);
            }
            catch { }
        }

        void PostSyncMessage()
        {
            Action post = () =>
            {
                var netEngin = new NetEngin(Program.Settings);
                string tmpFile = Path.GetTempFileName();
                var ms = new MemoryStream();
                var writer = new RawDataWriter(ms , Encoding.UTF8);
                writer.Write(m_clInfo.ClientID);
                writer.Write(m_srvLastMsgID);
                writer.Write(m_clientLastMsgID);
                byte[] msgData = ms.ToArray();

                try
                {
                    netEngin.Download(tmpFile , SettingsManager.ConnectionReqURI);
                    var seq = DialogEngin.ReadConnectionsReq(tmpFile);
                    uint msgID = 0;

                    if (seq.Any())
                        msgID = seq.Max(m => m.ID);

                    var msg = new Message(msgID + 1 , 0 , Message_t.Sync , msgData);
                    DialogEngin.AppendConnectionsReq(tmpFile , new Message[] { msg });
                    netEngin.Upload(SettingsManager.ConnectionReqURI , tmpFile);
                }
                catch { }
                finally
                {
                    File.Delete(tmpFile);
                }
            };


            var task = new Task(post , TaskCreationOptions.LongRunning);
            task.Start();
        }


        //handlers:
        private void SettingsView_ClientInfoChaned()
        {
            m_clInfo = Program.Settings.ClientInfo;            
        }
    }
}
