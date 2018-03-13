using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DGD.Hub.DLG
{
    sealed class CloseHandler
    {
        readonly uint m_clID;

        public CloseHandler(uint clID)
        {
            m_clID = clID;        
        }


        public void Start()
        {
            var thread = new System.Threading.Thread(PostReq);
            thread.Start();
        }


        //private:
        void PostReq()
        {
            var ms = new MemoryStream();
            var writer = new RawDataWriter(ms , Encoding.UTF8);
            writer.Write(m_clID);
            writer.Write(DateTime.Now);

            byte[] msgData = ms.ToArray();


            string tmpFile = Path.GetTempFileName();
            var netEngin = new NetEngin(Program.Settings);

            try
            {
                Dbg.Log("Posting closing notification...");

                netEngin.Download(tmpFile , SettingsManager.ConnectionReqURI);
                IEnumerable<Message> reqs = DialogEngin.ReadConnectionsReq(tmpFile);
                uint reqID = 0;

                if (reqs.Any())
                    reqID = reqs.Max(m => m.ID);

                var req = new Message(++reqID , 0 , Message_t.Close , msgData);
                DialogEngin.AppendConnectionsReq(tmpFile , reqs.Add(req));
                netEngin.Upload(SettingsManager.ConnectionReqURI , tmpFile);
            }
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }
    }
}
