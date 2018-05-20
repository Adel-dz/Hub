using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DGD.Hub.DLG
{
    sealed class StartHandler
    {
        const int TIMER_INTERVAL = 10 * 1000;
        const int MAX_ATTEMPTS = 10;
                
        readonly Timer m_timer;
        readonly Action<bool> m_callBack;
        readonly uint m_clID;        
        byte[] m_msgData;
        uint m_reqID;
        int m_cnxAttempts;


        public StartHandler(uint clID , Action<bool> callBack)
        {
            Dbg.Assert(callBack != null);

            m_clID = clID;
            m_callBack = callBack;
            m_timer = new Timer(TIMER_INTERVAL);
            m_timer.TimeElapsed += ProcessResp;
        }


        public void Start()
        {
            var ms = new MemoryStream();
            var writer = new RawDataWriter(ms , Encoding.UTF8);
            ClientEnvironment clEnv = GetEnvironment();
            writer.Write(m_clID);
            clEnv.Write(writer);
            writer.Write(DateTime.Now);
            m_msgData = ms.ToArray();

            var task = new Task(PostReq , TaskCreationOptions.LongRunning);
            task.Start();
        }


        //private:
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

        void PostReq()
        {
            Dbg.Log("Posting start msg...");

            //posting to cnx file
            string tmpFile = Path.GetTempFileName();
            var netEngin = new NetEngin(Program.NetworkSettings);

            try
            {
                netEngin.Download(tmpFile , Urls.ConnectionReqURL , true);

                IEnumerable<Message> msgsCnx = DialogEngin.ReadConnectionsReq(tmpFile);

                if (msgsCnx.Any())
                    m_reqID = msgsCnx.Max(m => m.ID);
                else
                    m_reqID = 0;

                Message req = new Message(++m_reqID , 0 , Message_t.Start , m_msgData);
                DialogEngin.WriteConnectionsReq(tmpFile , msgsCnx.Add(req));
                netEngin.Upload(Urls.ConnectionReqURL , tmpFile , true);
                m_cnxAttempts = 0;
                Dbg.Log("Posting start msg done.");
            }
            catch (Exception ex)
            {
                Dbg.Log(ex.Message);
            }
            finally
            {
                m_timer.Start();
                File.Delete(tmpFile);
            }
        }

        void ProcessResp()
        {
            m_timer.Stop();
            Dbg.Log("Processing start notification resp...");

            string tmpFile = Path.GetTempFileName();
            var netEngin = new NetEngin(Program.NetworkSettings);

            try
            {
                netEngin.Download(tmpFile , Urls.ConnectionRespURL , true);

                IEnumerable<Message> resps = from msg in DialogEngin.ReadConnectionsResp(tmpFile)
                                             where msg.ReqID >= m_reqID
                                             select msg;

                if (resps.Any())
                {
                    Message resp = resps.SingleOrDefault(m => m.ReqID == m_reqID);

                    if (resp != null)
                    {
                        var ms = new MemoryStream(resp.Data);
                        var reader = new RawDataReader(ms , Encoding.UTF8);
                        uint clID = reader.ReadUInt();

                        if (clID == m_clID)
                        {
                            switch (resp.MessageCode)
                            {
                                case Message_t.Ok:
                                m_callBack.Invoke(true);
                                Dbg.Log("Starting notification done. :-)");
                                return;

                                case Message_t.Rejected:
                                m_callBack.Invoke(false);
                                Dbg.Log("Starting rejected. :-(");
                                return;
                            }
                        }
                    }

                    Dbg.Log("Starting msg lost. Reposting...");
                    PostReq();
                }
                else if (++m_cnxAttempts >= MAX_ATTEMPTS)
                {
                    Dbg.Log("Starting msg lost. Reposting...");
                    PostReq();
                }
                else
                    m_timer.Start();
            }
            catch (Exception ex)
            {
                Dbg.Log(ex.Message);
                m_timer.Start();
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }
    }
}
