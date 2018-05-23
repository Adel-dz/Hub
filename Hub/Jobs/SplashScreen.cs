using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace DGD.Hub.Jobs
{
    sealed partial class SplashScreen: Form
    {
        const int TIMER_INTREVAL = 500;
        string m_msg;
        readonly easyLib.Timer m_timer;


        public SplashScreen()
        {
            InitializeComponent();

            m_timer = new easyLib.Timer(TIMER_INTREVAL);
            m_timer.TimeElapsed += Timer_TimeElapsed;

            m_lblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
        }


        public string FeedbackText
        {
            get { return m_msg ?? ""; }
            set { m_msg = value ?? ""; }
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_timer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            m_timer.Dispose();
            m_timer.TimeElapsed -= Timer_TimeElapsed;

            base.OnClosing(e);
        }


        //private:
        void ShowFeedback()
        {
            if (InvokeRequired)
                Invoke(new Action(ShowFeedback));
            else
            {
                if (m_lblMessage.Text != m_msg)
                    m_lblMessage.Text = m_msg;

                if (!m_timer.IsDisposed)
                    m_timer.Start();
            }
        }


        //handlers:
        private void Timer_TimeElapsed()
        {
            if (m_timer.IsDisposed)
                return;

            m_timer.Stop();
            ShowFeedback();
        }

    }
}
