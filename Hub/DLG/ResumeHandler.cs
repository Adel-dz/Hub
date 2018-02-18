﻿using DGD.HubCore.DLG;
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
            Enabled,
            Disbaled,
            Banned,
        }

        const int TIMER_INTERVALL = 10 * 1000;

        Action<Result_t> m_callback;
        string m_cxnReqFile;
        uint m_lastMsgID;
        readonly Timer m_timer;
        readonly uint m_clientID;


        public ResumeHandler(Action<Result_t> calback, uint clID)
        {
            Dbg.Assert(m_callback != null);

            m_callback = calback;
            m_clientID = clID;
            m_timer = new Timer(ProcessTimer , null, Timeout.Infinite , Timeout.Infinite);
        }

        

        public void Start()
        {
            string m_cnxReqFile = Path.GetTempFileName();

            Action init = () =>
            {
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(m_cnxReqFile , SettingsManager.ConnectionReqURI , true);

                IEnumerable<Message> msgs = DialogEngin.ReadConnectionsReq(m_cnxReqFile);

                if (msgs.Count() > 0)
                    m_lastMsgID = msgs.Max(msg => msg.ID);
            };

            Action<Task> onErr = t =>
            {
                File.Delete(m_cnxReqFile);

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

        private void ProcessTimer(object state)
        {
            throw new NotImplementedException();
        }
    }
}