using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.Hub.Jobs
{
    sealed class SplashScreenEngin: IJobFeedback, IDisposable
    {
        readonly SplashScreen m_splashScreen;

        public SplashScreenEngin()
        {
            m_splashScreen = new SplashScreen();
        }


        public string FeedbackText
        {
            get
            {
                Assert(!IsDisposed);

                return m_splashScreen.FeedbackText;
            }
            set
            {
                Assert(!IsDisposed);

                m_splashScreen.FeedbackText = value;
            }
        }

        public bool IsDisposed { get; private set; }
        public bool IsRunning { get; private set; }


        public void Close()
        {
            if (!IsDisposed)
            {
                m_splashScreen.BeginInvoke(new Action(m_splashScreen.Dispose));
                IsDisposed = true;
            }
        }

        public void Start()
        {
            Assert(!IsRunning);

            var thread = new Thread(() => m_splashScreen.ShowDialog());
            thread.Start();

            IsRunning = true;
        }

        public void Dispose() => Close();
    }
}
