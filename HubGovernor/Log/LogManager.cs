using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Log
{

    sealed class LogManager: IDisposable
    {
        readonly List<ClientEventLogger> m_clientsloggers;
        readonly SysEventLogger m_sysLogger;


        public event Action<IEventLog> SysLogAdded;
        public event Action<uint , IEventLog> ClientLogAdded;



        public LogManager()
        {
            m_clientsloggers = new List<ClientEventLogger>();
            m_sysLogger = new SysEventLogger();
        }


        public bool IsDisposed { get; private set; }

        public void LogClientActivity(uint clID , string txt, DateTime tm)
        {
            Dbg.Assert(IsLoggerStarted(clID) == true);

            lock (m_clientsloggers)
            {
                ClientEventLogger logger = m_clientsloggers.Find(l => l.ClientID == clID);

                IEventLog log = logger?.LogEvent(txt , tm , EventType_t.Activity);

                if (log != null)
                    ClientLogAdded?.Invoke(clID , log);
            }
        }

        public void LogClientError(uint clID , string txt, DateTime tm)
        {
            Dbg.Assert(IsLoggerStarted(clID) == true);

            lock (m_clientsloggers)
            {
                ClientEventLogger logger = m_clientsloggers.Find(l => l.ClientID == clID);

                IEventLog log = logger?.LogEvent(txt , tm , EventType_t.Error);

                if (log != null)
                    ClientLogAdded?.Invoke(clID , log);
            }
        }

        public void LogClientActivity(uint clID , string txt) => LogClientActivity(clID , txt , DateTime.Now);

        public void LogClientError(uint clID , string txt) => LogClientError(clID , txt , DateTime.Now);
        
        public void LogSysActivity(string txt, bool showToUser = false)
        {
            lock (m_sysLogger)
            {
                IEventLog log = m_sysLogger.LogEvent(txt , DateTime.Now , EventType_t.Activity);
                SysLogAdded?.Invoke(log);
            }

            if(showToUser)
                TextLogger.Info(txt);
        }

        public void LogSysError(string txt, bool showToUser = false)
        {
            lock (m_sysLogger)
            {
                IEventLog log = m_sysLogger.LogEvent(txt , DateTime.Now , EventType_t.Error);
                SysLogAdded?.Invoke(log);
            }

            if(showToUser)
                TextLogger.Error(txt);
        }

        public void LogUserActivity(string txt)
        {
            lock (m_sysLogger)
            {
                IEventLog log = m_sysLogger.LogEvent(txt , DateTime.Now , EventType_t.Activity);
                SysLogAdded?.Invoke(log);
            }
        }

        public void StartLogger(uint clID)
        {
            lock (m_clientsloggers)
            {
                if (m_clientsloggers.Find(l => l.ClientID == clID) == null)
                {
                    var logger = new ClientEventLogger(clID);
                    m_clientsloggers.Add(logger);
                }

            }
        }

        public void CloseLogger(uint clID)
        {
            lock (m_clientsloggers)
            {
                ClientEventLogger logger = m_clientsloggers.Find(l => l.ClientID == clID);

                if (logger != null)
                {
                    m_clientsloggers.Remove(logger);
                    logger.Dispose();
                }
            }
        }

        public bool IsLoggerStarted(uint clID)
        {
            lock (m_clientsloggers)
                return m_clientsloggers.Find(l => l.ClientID == clID) != null;
        }

        public IEnumerable<IEventLog> EnumerateClientLog(uint clID)
        {
            Dbg.Assert(IsLoggerStarted(clID));

            lock (m_clientsloggers)
            {
                ClientEventLogger logger = m_clientsloggers.Find(l => l.ClientID == clID);
                return logger?.Logs;
            }
        }

        public IEnumerable<IEventLog> EnumerateSysLog() => m_sysLogger.Logs;

        public void Dispose()
        {
            if (!IsDisposed)
            {
                lock (m_clientsloggers)
                    if (!IsDisposed)
                    {
                        SysLogAdded = null;
                        ClientLogAdded = null;


                        foreach (ClientEventLogger logger in m_clientsloggers)
                            logger.Dispose();

                        m_clientsloggers.Clear();

                        m_sysLogger.Dispose();
                        IsDisposed = true;
                    }
            }
        }
    }
}
