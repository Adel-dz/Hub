using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DGD.Hub.DLG
{
    sealed class ResumeHandler
    {
        public enum Result_t
        {
            Error,
            Ok,
            Rejected
        }

        const int TIMER_INTERVALL = 10 * 1000;

        Action<Result_t> m_callback;
        string m_cxnReqFile;
        uint m_lastMsgID;
        readonly Timer m_timer;
        readonly uint m_clientID;


        public ResumeHandler(Action<Result_t> callback, uint clID)
        {
            Dbg.Assert(callback != null);

            m_callback = callback;
            m_clientID = clID;
            m_timer = new Timer(ProcessTimer , null, Timeout.Infinite , Timeout.Infinite);
        }
             
        public void Start()
        {
            m_cxnReqFile = Path.GetTempFileName();

            //maj du ID req
            Action init = () =>
            {
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(m_cxnReqFile , SettingsManager.ConnectionReqURI , true);

                IEnumerable<Message> msgs = DialogEngin.ReadConnectionsReq(m_cxnReqFile);

                if (msgs.Count() > 0)
                    m_lastMsgID = msgs.Max(msg => msg.ID);
            };

            Action<Task> onErr = t =>
            {
                File.Delete(m_cxnReqFile);

                Dbg.Log(t.Exception.InnerException.Message);
                m_callback(Result_t.Error);
                m_callback = null;
            };


            var task = new Task(init , TaskCreationOptions.LongRunning);
            task.OnSuccess(PostReqAsync);
            task.OnError(onErr);
            task.Start();
        }

        //private:
        void PostReqAsync()
        {
            Action upload = () =>
            {
                var msg = new Message(++m_lastMsgID , 0 , Message_t.Resume , BitConverter.GetBytes(m_clientID));
                IEnumerable<Message> msgs = DialogEngin.ReadConnectionsReq(m_cxnReqFile);
                DialogEngin.WriteConnectionsReq(m_cxnReqFile , msgs.Add(msg));

                var netEngin = new NetEngin(Program.Settings);

                try
                {
                    netEngin.Upload(SettingsManager.ConnectionReqURI , m_cxnReqFile , true);
                    m_timer.Change(TIMER_INTERVALL , TIMER_INTERVALL);
                }
                catch(Exception ex)
                {
                    Dbg.Log(ex.Message);
                    m_callback(Result_t.Error);
                    m_callback = null;
                }
            };

            new Task(upload , TaskCreationOptions.LongRunning).Start();
        }

        void ProcessTimer(object unused)
        {
            m_timer.Change(Timeout.Infinite , Timeout.Infinite);

            var netEngin = new NetEngin(Program.Settings);
            Uri respFileURI = SettingsManager.ConnectionRespURI;
            string tmpFile = Path.GetTempFileName();

            netEngin.Download(tmpFile, respFileURI, true);

            var seq = from msg in DialogEngin.ReadConnectionsResp(tmpFile)
                      where msg.ReqID >= m_lastMsgID
                      select msg;

            if(!seq.Any())
                m_timer.Change(Timeout.Infinite , Timeout.Infinite);
            else
            {
                Message resp = (from msg in seq
                                where msg.ReqID == m_lastMsgID
                                select msg).SingleOrDefault();

                if (resp == null)
                    PostReqAsync();
                else
                {
                    switch (resp.MessageCode)
                    {
                        case Message_t.Ok:
                        //reset dlg file
                        try
                        {
                            string dlgFile = SettingsManager.GetClientDialogFilePath(m_clientID);
                            DialogEngin.WriteHubDialog(dlgFile , m_clientID , Enumerable.Empty<Message>());
                            netEngin.Upload(SettingsManager.GetClientDialogURI(m_clientID) , dlgFile , true);
                            m_callback(Result_t.Ok);
                        }
                        catch(Exception ex)
                        {
                            Dbg.Log(ex.Message);
                            PostReqAsync();
                        }                        
                        break;

                        case Message_t.InvalidID:
                        case Message_t.Rejected:
                        m_callback(Result_t.Rejected);
                        break;
                        
                        default:
                        Dbg.Assert(false);
                        break;
                    }
                }
                
            }
        }
    }
}
