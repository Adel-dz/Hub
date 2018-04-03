using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Log
{
    sealed partial class SysLogWindow: Form
    {
        Font m_tmFont;


        public SysLogWindow()
        {
            InitializeComponent();
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_tmFont = new Font(m_rtbData.Font , FontStyle.Bold | FontStyle.Italic);

            foreach (IEventLog evLog in AppContext.LogManager.EnumerateSysLog())
                AddLog(evLog);

            AppContext.LogManager.SysLogAdded += LogManager_SysLogAdded;

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {            
            base.OnFormClosed(e);

            AppContext.LogManager.SysLogAdded -= LogManager_SysLogAdded;
            m_tmFont?.Dispose();
        }


        //private:
        void AddLog(IEventLog evLog)
        {
            m_rtbData.SelectionBullet = true;

            m_rtbData.SelectionIndent = 10;
            m_rtbData.SelectionFont = m_tmFont;
            m_rtbData.SelectedText = evLog.Time.ToString() + ": ";
            m_rtbData.SelectionFont = m_rtbData.Font;

            if (evLog.EventType == EventType_t.Error)
                m_rtbData.SelectionColor = Color.Red;

            m_rtbData.SelectedText = evLog.Text + Environment.NewLine;

            m_rtbData.SelectionBullet = false;
            m_rtbData.AppendText(Environment.NewLine);
        }


        //handlers:
        private void LogManager_SysLogAdded(IEventLog log) 
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IEventLog>(AddLog) , log);
            else
                AddLog(log);
        }

        private void SortAscending_Click(object sender , EventArgs e)
        {
            AppContext.LogManager.SysLogAdded -= LogManager_SysLogAdded;

            IEnumerable<IEventLog> seq = from IEventLog log in AppContext.LogManager.EnumerateSysLog()
                                         orderby log.Time
                                         select log;

            m_rtbData.Clear();

            foreach (IEventLog log in seq)
                AddLog(log);

            AppContext.LogManager.SysLogAdded += LogManager_SysLogAdded;
        }

        private void SortDescending_Click(object sender , EventArgs e)
        {
            AppContext.LogManager.SysLogAdded -= LogManager_SysLogAdded;

            IEnumerable<IEventLog> seq = from IEventLog log in AppContext.LogManager.EnumerateSysLog()
                                         orderby log.Time descending
                                         select log;

            m_rtbData.Clear();

            foreach (IEventLog log in seq)
                AddLog(log);

            AppContext.LogManager.SysLogAdded += LogManager_SysLogAdded;
        }
    }
}
