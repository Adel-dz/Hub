using easyLib;
using System;

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
                if (!IsDisposed)
                {
                    m_timer.Restart();
                    m_msgVisible = true;

                    MessageReady?.Invoke(msg);
                }
            }
        }

        public static IDisposable PushMessage(string msg)
        {
            lock (m_timer)
            {
                if (!IsDisposed)
                {
                    if (m_msgVisible)
                        CloseMessage();

                    m_msgVisible = true;
                    MessageReady?.Invoke(msg);
                }

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
            if (!IsDisposed)
            {
                m_timer.Stop();
                MessageTimeout?.Invoke();
            }
        }

        static void ProcessTimer()
        {
            lock (m_timer)
            {
                if (!IsDisposed)
                {
                    CloseMessage();
                    m_msgVisible = false;
                }
            }
        }

    }
}
