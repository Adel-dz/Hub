using System;
using static System.Diagnostics.Debug;


namespace easyLib
{
    public sealed class Timer: IDisposable
    {
        readonly System.Threading.Timer m_timer;
        int m_interval;


        public event Action TimeElapsed;

        public Timer(int msInterval)
        {
            Assert(msInterval >= 0);

            m_timer = new System.Threading.Timer(ProcessTimer ,
                null ,
                System.Threading.Timeout.Infinite ,
                System.Threading.Timeout.Infinite);

            m_interval = msInterval;
        }


        public bool IsDiposed { get; private set; }
        public bool IsRunning { get; private set; }

        public int Interval
        {
            get { return m_interval; }

            set
            {
                lock (m_timer)
                    if (m_timer.Change(IsRunning ? value : System.Threading.Timeout.Infinite , value))
                        m_interval = value;
            }
        }

        public void Start(bool startNow = false)
        {
            Assert(!IsRunning);

            lock (m_timer)
                IsRunning = m_timer.Change(startNow ? 0 : System.Threading.Timeout.Infinite , m_interval);
        }

        public void Stop()
        {
            Assert(IsRunning);

            lock (m_timer)
                IsRunning = !m_timer.Change(System.Threading.Timeout.Infinite , System.Threading.Timeout.Infinite);
        }

        public void Dispose()
        {
            if (!IsDiposed)
                lock (m_timer)
                {
                    if (!IsDiposed)
                    {
                        m_timer.Change(System.Threading.Timeout.Infinite , System.Threading.Timeout.Infinite);
                        m_timer.Dispose();
                        TimeElapsed = null;
                        IsDiposed = true;
                    }
                }
        }


        //private:
        void ProcessTimer(object unused) => TimeElapsed?.Invoke();
    }
}
