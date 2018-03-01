﻿using DGD.HubCore.DLG;
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
    sealed class StartHandler
    {
        const int TIMER_INTERVAL = 30 * 1000;
        const int MAX_ATTEMPTS = 10;

        readonly easyLib.Timer m_timer;
        readonly uint m_clID;
        byte[] m_msgData;
        uint m_reqID;
        int m_cnxAttempts;


        public StartHandler(uint clID)
        {
            m_clID = clID;
            m_timer = new Timer(TIMER_INTERVAL);
            m_timer.TimeElapsed += ProcessResp;
        }
            

        public void Start()
        {
            var ms = new MemoryStream();
            var writer = new RawDataWriter(ms , Encoding.UTF8);
            writer.Write(m_clID);
            writer.Write(DateTime.Now);
            m_msgData = ms.ToArray();

            var task = new Task(PostReq , TaskCreationOptions.LongRunning);
            task.Start();
        }

        //private:
        void PostReq()
        {
            Dbg.Log("Posting start msg...");

            //posting to cnx file
            string tmpFile = Path.GetTempFileName();
            var netEngin = new NetEngin(Program.Settings);

            try
            {
                netEngin.Download(tmpFile , SettingsManager.ConnectionReqURI , true);

                IEnumerable<Message> msgsCnx = DialogEngin.ReadConnectionsReq(tmpFile);

                if (msgsCnx.Any())
                    m_reqID = msgsCnx.Max(m => m.ID);
                else
                    m_reqID = 0;

                Message req = new Message(++m_reqID , 0 , Message_t.Start , m_msgData);
                DialogEngin.WriteConnectionsReq(tmpFile , msgsCnx.Add(req));
                netEngin.Upload(SettingsManager.ConnectionReqURI, tmpFile , true);
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

            try
            {
                new NetEngin(Program.Settings).Download(tmpFile , SettingsManager.ConnectionRespURI , true);

                IEnumerable<Message> resps = from msg in DialogEngin.ReadConnectionsResp(tmpFile)
                                             where msg.MessageCode == Message_t.Ok && msg.ReqID >= m_reqID
                                             select msg;

                if (resps.Any())
                {
                    Message resp = resps.SingleOrDefault(m => m.ReqID == m_reqID);

                    if (resp != null)
                    {
                        var ms = new MemoryStream(resp.Data);
                        var reader = new RawDataReader(ms , Encoding.UTF8);
                        uint clID = reader.ReadUInt();

                        Dbg.Assert(resp.MessageCode == Message_t.Ok);

                        if (clID == m_clID)
                        {
                            Dbg.Log("Starting notification done. :-)");
                            return;
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
