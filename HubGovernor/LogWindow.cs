using easyLib.Log;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Diagnostics.Debug;

namespace DGD.HubGovernor
{
    sealed partial class LogWindow: Form, ILogReceiver
    {
        const string OPT_KEY = "LOG";


        public LogWindow()
        {
            InitializeComponent();

            TextLogger.RegisterReceiver(this);
        }

        public void Write(string message , LogSeverity severity)
        {
            Dbg.Log(message);

            if (InvokeRequired)
                BeginInvoke(new Action<string , LogSeverity>(LogText) , message , severity);
            else
                LogText(message , severity);
        }



        //protected:
        protected override void OnLoad(EventArgs e)
        {
            Opts.WindowPlacement wp = AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY];

            if (wp != null)
                Bounds = wp;

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                base.OnFormClosing(e);

                AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY] = new Opts.WindowPlacement(Bounds);
            }
        }



        //private:
        void LogText(string text , LogSeverity severity)
        {
            Assert(!InvokeRequired);

            if (m_rtbLogBox.IsDisposed)
                return;

            bool urgent = severity >= LogSeverity.Warning;
            Color clr = severity > LogSeverity.Warning ? Color.Red : 
                severity == LogSeverity.Warning? Color.DarkOrange : m_rtbLogBox.ForeColor;

            int start = m_rtbLogBox.TextLength;
            int len = text.Length;

            m_rtbLogBox.AppendText(text);
            m_rtbLogBox.AppendText(Environment.NewLine);

            m_rtbLogBox.Select(start , len);
            m_rtbLogBox.SelectionColor = clr;
            m_rtbLogBox.Select(m_rtbLogBox.TextLength , 0);

            m_rtbLogBox.ScrollToCaret();

            if (urgent && !Visible)
                Visible = true;
        }


        //handlers
        private void Clear_Click(object sender , EventArgs e)
        {
            m_rtbLogBox.Clear();
        }
    }
}
