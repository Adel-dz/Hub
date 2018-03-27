using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Log
{
    class ClientEventLogger: IEventLogger
    {
        DataTable<EventLog> m_logTable;
        IDatumProvider m_logProvider;
        readonly object m_lock = new object();
        readonly uint m_clID;

        public ClientEventLogger(uint clID)
        {
            Dbg.Assert(clID != 0);
            m_clID = clID;
        }

        protected ClientEventLogger()
        { }


        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (!IsDisposed)
                lock (m_lock)
                    if (!IsDisposed)
                    {
                        m_logProvider?.Close();
                        IsDisposed = true;
                    }
        }

        public IEnumerable<IEventLog> EnumerateLog(uint clID = 0)
        {
            lock (m_lock)
                return DataProvider.Enumerate().Cast<IEventLog>();            
        }

        public void LogEvent(string txt , DateTime tm , EventType_t evType , EventSource_t src , uint clientID = 0)
        {
            if(clientID == m_clID)
            {
                Dbg.Assert(src == EventSource_t.Client);

                lock(m_lock)
                {
                    IDatumProvider dp = DataProvider;
                    var log = new EventLog(m_logTable.CreateUniqID() , txt , evType , tm);
                    dp.Insert(log);
                }
            }
        }


        //private:
        IDatumProvider DataProvider
        {
            get
            {
                if (m_logProvider == null)
                {
                    string tblPath = m_clID == 0? AppPaths.SrvLogPath : AppPaths.GetClientLogPath(m_clID);
                    m_logTable = new EventLogTable("Log" , tblPath);
                    m_logProvider = m_logTable.DataProvider;

                    m_logProvider.Connect();
                }

                return m_logProvider;
            }
        }
    }
}
