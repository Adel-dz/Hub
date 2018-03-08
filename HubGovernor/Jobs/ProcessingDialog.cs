using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DGD.HubGovernor.Jobs
{
    sealed partial class ProcessingDialog: Form
    {
        string m_msg;
        bool m_isDirty;

        public ProcessingDialog()
        {
            InitializeComponent();
        }


        public string Message
        {
            get { return m_lblMessage.Text; }

            set
            {
                m_msg = value;            
                m_isDirty = true;
            }
        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_timer.Start();

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            m_timer.Stop();
        }

        //private:

        //handlers:
        private void Timer_Tick(object sender , EventArgs e)
        {
            if (m_isDirty)
            {
                m_lblMessage.Text = m_msg;
                m_isDirty = false;
            }
        }
    }
}
