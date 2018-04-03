using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Log
{
    interface ISysEventLogger: IEventLogger
    {
        IEnumerable<IEventLog> Logs { get; }
    }


    
    sealed class SysEventLogger: ISysEventLogger
    {
        readonly EventLogTable m_tbl;
        readonly IDatumProvider m_dpLogs;


        public SysEventLogger()
        {
            m_tbl = new EventLogTable(AppPaths.SysLogPath);
            m_dpLogs = m_tbl.DataProvider;
            m_dpLogs.Connect();
        }

        
        public bool IsDisposed { get; private set; }

        public IEnumerable<IEventLog> Logs => m_dpLogs.Enumerate().Cast<IEventLog>();

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_dpLogs.Dispose();
                m_tbl.Dispose();

                IsDisposed = true;
            }
        }

        public IEventLog LogEvent(string txt , DateTime tm , EventType_t evType)
        {
            var ev = new EventLog(m_tbl.CreateUniqID() , txt , evType);
            m_dpLogs.Insert(ev);

            return ev;
        }
    }


}
