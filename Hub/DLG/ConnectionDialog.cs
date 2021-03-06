﻿using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DGD.Hub.DLG
{
    sealed partial class ConnectionDialog: Form
    {
        const string SRV_ERROR = "Aucune réponse du serveur. Veuillez réessayer ultérieurement.";
        const string MAX_ATTEMPTS_ERROR = "Le serveur tarde à répondre. Voulez-vous réessayer ?";
        const string REJECT_CONNCTION_ERROR = "Votre inscription a été refusée. " +
            "Veuillez contacter la sous-direction de la valeur en douane pour résoudre ce problème.";

        ClientInfo m_clInfo;
        Action<Exception> m_exHandler;
        uint m_msgID;
        int m_attemptsCount;


        public ConnectionDialog(ClientInfo clInfo)
        {
            InitializeComponent();

            m_timer.Interval = SettingsManager.ConnectionTimerInterval;
            m_clInfo = clInfo;
        }


        public bool IsRegistered { get; private set; }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            RunAction(PostReq);

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            m_timer.Enabled = false;
        }


        //private:
        void SetProgressMessage(string msg)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => m_lbl_ProgressInfo.Text = msg));
                else
                    m_lbl_ProgressInfo.Text = msg;
            }
            catch
            { }
        }

        void StartTimer()
        {
            if (InvokeRequired)
                Invoke(new Action(m_timer.Start));
            else
                m_timer.Start();
        }

        void StopTimer()
        {
            if (InvokeRequired)
                Invoke(new Action(m_timer.Stop));
            else
                m_timer.Stop();
        }

        void RunAction(Action action , bool useThreadPool = false)
        {
            Action<Task> onErr = t => m_exHandler(t.Exception.InnerException);

            var task = new Task(action , useThreadPool ? TaskCreationOptions.None : TaskCreationOptions.LongRunning);
            task.OnError(onErr);

            task.Start();
        }

        void FatalExceptionHandler(Exception ex)
        {
            Dbg.Log("Fatal exception.");

            Log.LogEngin.PushFlash(ex.Message);

            ShowMessage(SRV_ERROR);
            CloseDialog();
        }

        void RespExceptionHandler(Exception ex)
        {
            Dbg.Log("Response exception.");

            if (++m_attemptsCount >= SettingsManager.MaxConnectAttemps)
                FatalExceptionHandler(ex);
            else
                StartTimer();

        }

        void ReqExceptionHandler(Exception ex)
        {
            Dbg.Log("Request exception.");
            if (++m_attemptsCount >= SettingsManager.MaxConnectAttemps)
                FatalExceptionHandler(ex);
            else
            {
                Thread.Sleep(500);
                PostReq();
            }
        }

        void CloseDialog()
        {
            if (InvokeRequired)
                Invoke(new Action(Close));
            else
                Close();
        }

        DialogResult ShowMessage(string txt , MessageBoxButtons btns = MessageBoxButtons.OK)
        {
            const string caption = "Enregistement de l'application";

            if (InvokeRequired)
                return (DialogResult)Invoke(new Func<string , string , MessageBoxButtons , DialogResult>(MessageBox.Show) ,
                    txt , caption , btns);

            return MessageBox.Show(txt , caption , btns);
        }

        void ProcessResp()
        {
            StopTimer();

            string tmpFile = Path.GetTempFileName();
            Dbg.Log($"Processing Response, attempts = {m_attemptsCount + 1}.");

            m_exHandler = RespExceptionHandler;

            var netEngin = new NetEngin(Program.NetworkSettings);

            using (new AutoReleaser(() => File.Delete(tmpFile)))
            {
                SetProgressMessage("Réception des données à partir du serveur...");

                try
                {
                    netEngin.Download(tmpFile , Urls.ConnectionRespURL, true);
                }
                catch(Exception ex)
                {
                    Dbg.Log(ex.Message);
                    DialogEngin.WriteConnectionsResp(tmpFile , Enumerable.Empty<HubCore.DLG.Message>());
                }

                IEnumerable<HubCore.DLG.Message> messages = DialogEngin.ReadConnectionsResp(tmpFile);
                HubCore.DLG.Message[] msgs = (from resp in messages
                                              where resp.ReqID >= m_msgID
                                              select resp).ToArray();

                HubCore.DLG.Message msg = msgs.Where(m => m.ReqID == m_msgID).SingleOrDefault();

                uint clID = msg == null ? 0 : BitConverter.ToUInt32(msg.Data , 0);

                if (msg != null && clID == m_clInfo.ClientID)
                {
                    switch (msg.MessageCode)
                    {
                        case Message_t.InvalidID:
                        Dbg.Log($"Got invalid ID! (ClientID = {m_clInfo.ClientID}).");

                        ClientInfo clInfo = ClientInfo.CreateClient(m_clInfo.ProfileID);
                        clInfo.ContaclEMail = m_clInfo.ContaclEMail;
                        clInfo.ContactName = m_clInfo.ContactName;
                        clInfo.ContactPhone = m_clInfo.ContactPhone;                        
                        m_clInfo = clInfo;

                        if (++m_attemptsCount >= SettingsManager.MaxConnectAttemps)
                            if (ShowMessage(MAX_ATTEMPTS_ERROR , MessageBoxButtons.YesNo) != DialogResult.Yes)
                            {
                                CloseDialog();
                                return;
                            }
                            else
                                m_attemptsCount = 0;

                        PostReq();
                        break;

                        case Message_t.Ok:
                        Dbg.Log("Client registered :-)!");


                        Program.Settings.ClientInfo = m_clInfo;
                        SetProgressMessage("Enregistrement terminé.");

                        //creation des fichier dlg
                        string dlgFile = SettingsManager.GetClientDialogFilePath(clID);
                        DialogEngin.WriteHubDialog(dlgFile , clID , Enumerable.Empty<HubCore.DLG.Message>());
         
                        try
                        {
                            netEngin.Upload(SettingsManager.GetClientDialogURL(clID) , dlgFile , true);
                        }
                        catch(Exception ex)
                        {
                            Dbg.Log(ex.Message);
                        }

                        ShowMessage("Votre enregistrement est maintenant terminé. " +
                            "Vous pouvez commencer à utiliser l’application.");

                        IsRegistered = true;
                        CloseDialog();
                        break;

                        case Message_t.InvalidProfile:
                        Dbg.Log($"Got invalid Profile! (ProfileID: = {m_clInfo.ProfileID}).");

                        ShowMessage(SRV_ERROR);
                        CloseDialog();
                        return;

                        case Message_t.Rejected:
                        Dbg.Log("Got reject connection!");
                        ShowMessage(REJECT_CONNCTION_ERROR);
                        CloseDialog();
                        return;

                        default:
                        Dbg.Log("Got invalid response!!!!");
                        Dbg.Assert(false);
                        break;
                    }
                }
                else if (msgs.Length > 0)
                {
                    Dbg.Log("Request message lost.");

                    if (++m_attemptsCount >= SettingsManager.MaxConnectAttemps)
                        if (ShowMessage(MAX_ATTEMPTS_ERROR , MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            CloseDialog();
                            return;
                        }
                        else
                            m_attemptsCount = 0;

                    PostReq();
                }
                else if (++m_attemptsCount >= SettingsManager.MaxConnectAttemps)
                {
                    Dbg.Log("Timeout.");

                    if (ShowMessage(MAX_ATTEMPTS_ERROR , MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        StartTimer();
                        m_attemptsCount = 0;
                        PostReq();
                    }
                    else
                        CloseDialog();
                }
                else
                {
                    StartTimer();
                    SetProgressMessage("Attente de la réponse du serveur...");
                }
            }
        }

        void PostReq()
        {
            Dbg.Log("Sending connection request.");

            string tmpFile = Path.GetTempFileName();

            m_exHandler = ReqExceptionHandler;

            using (new AutoReleaser(() => File.Delete(tmpFile)))
            {
                var netEngin = new NetEngin(Program.NetworkSettings);

                SetProgressMessage("Envoi des données au serveur...");

                netEngin.Download(tmpFile , Urls.ConnectionReqURL, true);
                List<HubCore.DLG.Message> msgs = DialogEngin.ReadConnectionsReq(tmpFile).ToList();
                m_msgID = msgs.Count == 0 ? 1 : msgs.Max(m => m.ID) + 1;

                var ms = new MemoryStream();
                byte[] ciBytes = m_clInfo.GetBytes();
                byte[] ceBytes = GetEnvironment().GetBytes();
                ms.Write(ciBytes , 0 , ciBytes.Length);
                ms.Write(ceBytes , 0 , ceBytes.Length);

                var msg = new HubCore.DLG.Message(m_msgID , 0 , Message_t.NewConnection , ms.ToArray());
                msgs.Add(msg);

                DialogEngin.WriteConnectionsReq(tmpFile , msgs);
                netEngin.Upload(Urls.ConnectionReqURL , tmpFile, true);
                StartTimer();

                SetProgressMessage("Attente de la réponse du serveur...");
            }
        }

        ClientEnvironment GetEnvironment()
        {
            var clEnv = new ClientEnvironment();
            clEnv.HubArchitecture = Program.AppArchitecture;
            clEnv.HubVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            clEnv.Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
            clEnv.MachineName = Environment.MachineName;
            clEnv.OSVersion = Environment.OSVersion.VersionString;
            clEnv.UserName = Environment.UserName;

            return clEnv;
        }

        //handlers:
        private void Timer_Tick(object sender , EventArgs e)
        {
            RunAction(ProcessResp);
        }
    }
}
