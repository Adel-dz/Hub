using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DGD.Hub.Log
{
    static class LogEngin
    {
        const int MESSAGE_DELAY = 5000;

        static Timer m_timer;
        static bool m_msgVisible;

        public static event Action<string> MessageReady;
        public static event Action MessageTimeout;


        static LogEngin()
        {
            m_timer = new Timer(ProcessTimer);
        }


        public static void PushFlash(string msg)
        {
            lock (m_timer)
            {
                m_timer.Change(MESSAGE_DELAY , Timeout.Infinite);
                m_msgVisible = true;
            }

            MessageReady?.Invoke(msg);
        }

        public static IDisposable PushMessage(string msg)
        {
            lock (m_timer)
                if (m_msgVisible)
                    CloseMessage();

            MessageReady?.Invoke(msg);
            return new AutoReleaser(CloseMessage);
        }

        public static void Dispose()
        {
            if(m_timer != null)
            {
                lock (m_timer)
                    if (m_timer != null)
                    {
                        m_timer.Dispose();
                        m_timer = null;
                    }
            }
        }

        //private:
        static void CloseMessage()
        {
            m_timer.Change(Timeout.Infinite , Timeout.Infinite);
            MessageTimeout?.Invoke();
        }

        static void ProcessTimer(object notUsed)
        {
            lock (m_timer)
            {
                CloseMessage();
                m_msgVisible = false;
            }
        }

    }
}
