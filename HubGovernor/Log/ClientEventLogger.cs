using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Log
{
    interface IClientEventLogger: IEventLogger
    {
        uint ClientID { get; }
        IEnumerable<IEventLog> Logs { get; }
    }



    sealed class ClientEventLogger: IClientEventLogger
    {
        readonly EventLogTable m_tbl;
        IDatumProvider m_logProvider;

        public ClientEventLogger(uint clID)
        {
            Dbg.Assert(clID != 0);
            ClientID = clID;

            m_tbl = new EventLogTable(AppPaths.GetClientLogPath(clID));
            m_logProvider = m_tbl.DataProvider;
            m_logProvider.Connect();
        }

        public bool IsDisposed { get; private set; }
        public IEnumerable<IEventLog> Logs => m_logProvider.Enumerate().Cast<IEventLog>();
        public uint ClientID { get; }


        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_logProvider.Dispose();
                m_tbl.Dispose();
                IsDisposed = true;
            }
        }

        public IEventLog LogEvent(string txt , DateTime tm , EventType_t evType)
        {
            var log = new EventLog(m_tbl.CreateUniqID() , txt , evType , tm);
            m_logProvider.Insert(log);

            return log;
        }
    }
}
