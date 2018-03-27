using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Log
{
    enum EventSource_t
    {
        System,
        User,
        Client
    }



    sealed class LogManager: IDisposable
    {
        readonly List<IEventLogger> m_loggers = new List<IEventLogger>();


        public bool IsDisposed { get; private set; }

        public void LogClientActivity(uint clID , string txt , DateTime tm) =>
            LogEvent(txt , tm , EventType_t.Activity , EventSource_t.Client , clID);


        public void LogClientError(uint clID , string txt , DateTime tm) =>
            LogEvent(txt , tm , EventType_t.Error , EventSource_t.Client , clID);


        public void LogSysActivity(string txt , DateTime tm) =>
            LogEvent(txt , tm , EventType_t.Activity , EventSource_t.System , 0);


        public void LogSysError(string txt , DateTime tm) =>
            LogEvent(txt , tm , EventType_t.Error , EventSource_t.System , 0);

        public void LogUserActivity(string txt , DateTime tm) =>
            LogEvent(txt , tm , EventType_t.Activity , EventSource_t.User , 0);


        public void RegisterLogger(IEventLogger logger)
        {
            Dbg.Assert(logger != null);

            lock (m_loggers)
                if (!IsLoggerRegistered(logger))
                    m_loggers.Add(logger);
        }

        public void UnregisterLogger(IEventLogger logger)
        {
            if (logger == null)
                return;

            lock (m_loggers)
            {
                for (int i = 0; i < m_loggers.Count; ++i)
                    if (m_loggers[i].Equals(logger))
                    {
                        m_loggers.RemoveAt(i);                        
                        break;
                    }
            }
        }

        public bool IsLoggerRegistered(IEventLogger logger)
        {
            lock (m_loggers)
                foreach (IEventLogger log in m_loggers)
                    if (log.Equals(logger))
                        return true;

            return false;
        }

        public IEnumerable<IEventLog> EnumerateClientLog(uint clID)
        {
            IEnumerable<IEventLog> seq = Enumerable.Empty<IEventLog>();

            lock (m_loggers)
                foreach (IEventLogger logger in m_loggers)
                    seq.Concat(logger.EnumerateLog(clID));

            return seq;
        }

        public IEnumerable<IEventLog> EnumerateSrvLog()
        {
            IEnumerable<IEventLog> seq = Enumerable.Empty<IEventLog>();

            lock (m_loggers)
                foreach (IEventLogger logger in m_loggers)
                    seq.Concat(logger.EnumerateLog(0));

            return seq;
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                lock (m_loggers)
                {
                    foreach (IEventLogger logger in m_loggers)
                        logger.Dispose();

                    m_loggers.Clear();

                    IsDisposed = true;
                }
            }
        }


        //private:
        void LogEvent(string txt , DateTime tm , EventType_t evType , EventSource_t src , uint clID)
        {
            if (!string.IsNullOrWhiteSpace(txt))
                lock (m_loggers)
                    foreach (IEventLogger logger in m_loggers)
                        logger.LogEvent(txt , tm , evType , src , clID);
        }
    }
}
