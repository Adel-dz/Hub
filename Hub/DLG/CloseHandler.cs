using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.Hub.DLG
{
    sealed class CloseHandler
    {
        const int MAX_ATTEMPTS = 5;
        const int TIMER_INTERVAL = 5 * 1000;

        readonly Timer m_timer;
        readonly System.Threading.AutoResetEvent m_autoEvent;
        readonly uint m_clID;
        byte[] m_msgData;
        uint m_reqID;
        int m_attempts;
        

        public CloseHandler(uint clID)
        {
            m_clID = clID;

            m_timer = new Timer(TIMER_INTERVAL);
            m_timer.TimeElapsed += ProcessTimer;

            m_autoEvent = new System.Threading.AutoResetEvent(false);
        }

        ~CloseHandler()
        {
            Dbg.Log("CloseHandler finalized.");
        }

        public void Start()
        {
            var ms = new MemoryStream();
            var writer = new RawDataWriter(ms , Encoding.UTF8);
            writer.Write(m_clID);
            writer.Write(DateTime.Now);

            m_msgData = ms.ToArray();

            var thread = new System.Threading.Thread(DoWork);
            thread.Start();
        }


        //private:
        void DoWork()
        {
            PostReq();
            m_autoEvent.WaitOne();
            m_autoEvent.Dispose();
        }

        void PostReq()
        {
            string tmpFile = Path.GetTempFileName();
            var netEngin = new NetEngin(Program.Settings);

            try
            {
                Dbg.Log("Posting closing notification...");

                netEngin.Download(tmpFile , SettingsManager.ConnectionReqURI);
                IEnumerable<Message> reqs = DialogEngin.ReadConnectionsReq(tmpFile);

                if (reqs.Any())
                    m_reqID = reqs.Max(m => m.ID);

                var req = new Message(++m_reqID , 0 , Message_t.Close , m_msgData);
                DialogEngin.AppendConnectionsReq(tmpFile , reqs.Add(req));
                netEngin.Upload(SettingsManager.ConnectionReqURI , tmpFile);
                m_timer.Start();
            }
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);
                m_autoEvent.Set();
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        void ProcessTimer()
        {
            m_timer.Stop();
            string tmpFile = Path.GetTempFileName();

            try
            {
                var netEngin = new NetEngin(Program.Settings);
                netEngin.Download(tmpFile , SettingsManager.ConnectionRespURI);

                IEnumerable<Message> resps = from msg in DialogEngin.ReadConnectionsResp(tmpFile)
                                             where msg.MessageCode == Message_t.Ok && msg.ReqID >= m_reqID
                                             select msg;

                if (resps.Any())
                {
                    Message resp = resps.SingleOrDefault(m => m.ReqID == m_reqID);

                    if (resp != null)
                    {
                        if (BitConverter.ToUInt32(resp.Data , 0) == m_clID)
                        {
                            Dbg.Log("Closing notification done.");
                            m_autoEvent.Set();
                            return;
                        }
                    }

                    if (++m_attempts < MAX_ATTEMPTS)
                        PostReq();
                    else
                    {
                        m_autoEvent.Set();
                        return;
                    }
                }
                else if (++m_attempts < MAX_ATTEMPTS)
                    m_timer.Start();
                else
                {
                    m_autoEvent.Set();
                    return;
                }
            }
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);

                if (++m_attempts < MAX_ATTEMPTS)
                    m_timer.Restart();
                else
                {
                    m_autoEvent.Set();
                    return;
                }
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }
    }
}
