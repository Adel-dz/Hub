//using DGD.HubCore.DLG;
//using DGD.HubCore.Net;
//using easyLib;
//using easyLib.Extensions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DGD.Hub.DLG
//{
//    sealed class StartHandler
//    {
//        const int TIMER_INTERVAL = 30 * 1000;

//        readonly easyLib.Timer m_timer;
//        readonly uint m_clID;
//        byte[] m_msgData;
//        uint m_reqID;        


//        public StartHandler(uint clID)
//        {
//            m_clID = clID;
//            m_timer = new easyLib.Timer(TIMER_INTERVAL);
//            m_timer.TimeElapsed += ProcessResp;
//        }

//        ~StartHandler()
//        {
//            Dbg.Log("StartHandler finalized.");
//        }

//        public void Start()
//        {
//            var ms = new MemoryStream();
//            var writer = new RawDataWriter(ms , Encoding.UTF8);
//            writer.Write(m_clID);
//            writer.Write(DateTime.Now);
//            m_msgData = ms.ToArray();

//            var task = new Task(PostReq , TaskCreationOptions.LongRunning);
//            task.Start();
//        }

//        //private:
//        void PostReq()
//        {
//            Dbg.Log("Posting start msg...");

//            //posting to cnx file

//            string tmpFile = Path.GetTempFileName();
//            var netEngin = new NetEngin(Program.Settings);

//            try
//            {
//                netEngin.Download(tmpFile , SettingsManager.ConnectionReqURI , true);                

//                IEnumerable<Message> msgsCnx = DialogEngin.ReadConnectionsReq(tmpFile);

//                if (msgsCnx.Any())
//                    m_reqID = msgsCnx.Max(m => m.ID);
//                else
//                    m_reqID = 0;

//                Message req = new Message(++m_reqID , 0 , Message_t.Start , m_msgData);
//                DialogEngin.WriteConnectionsReq(tmpFile , msgsCnx.Add(req));
//                netEngin.Upload(SettingsManager.DialogDirUri , tmpFile , true);
//                Dbg.Log("Posting start msg done.");
//            }
//            catch(Exception ex)
//            {
//                Dbg.Log(ex.Message);
//            }
//            finally
//            {
//                m_timer.Start();
//                File.Delete(tmpFile);
//            }                                    
//        }

//        void ProcessResp()
//        {
//            m_timer.Stop();

//            string tmpFile = Path.GetTempFileName();

//            try
//            {
//                new NetEngin(Program.Settings).Download(tmpFile , SettingsManager.ConnectionRespURI , true);

//                IEnumerable<Message> resps = from msg in DialogEngin.ReadConnectionsResp(tmpFile)
//                                             where msg.PrevMessageID >= m_reqID
//                                             select msg;

//                if(m_msgData.Any())
//                {

//                }

//            }
//        }

//    }
//}
