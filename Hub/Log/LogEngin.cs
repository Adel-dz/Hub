using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.Hub.Log
{
    static class LogEngin
    {
        const int MESSAGE_DELAY = 5000;

        static readonly Timer m_timer;
        static bool m_msgVisible;

        public static event Action<string> MessageReady;
        public static event Action MessageTimeout;


        static LogEngin()
        {
            m_timer = new Timer(MESSAGE_DELAY);
            m_timer.TimeElapsed += ProcessTimer;
        }

        public static bool IsDisposed { get; private set; }

        public static void PushFlash(string msg)
        {
            lock (m_timer)
            {
                m_timer.Start();
                m_msgVisible = true;

                MessageReady?.Invoke(msg);
            }
        }

        public static IDisposable PushMessage(string msg)
        {
            lock (m_timer)
            {
                if (m_msgVisible)
                    CloseMessage();

                MessageReady?.Invoke(msg);
                return new AutoReleaser(CloseMessage);
            }
        }

        public static void Dispose()
        {
            if(!IsDisposed)
            {
                lock (m_timer)
                    if (!IsDisposed)
                    {
                        m_timer.Dispose();
                        IsDisposed = true;
                    }
            }
        }

        //private:
        static void CloseMessage()
        {
            m_timer.Stop();
            MessageTimeout?.Invoke();
        }

        static void ProcessTimer()
        {
            lock (m_timer)
            {
                CloseMessage();
                m_msgVisible = false;
            }
        }

    }
}
