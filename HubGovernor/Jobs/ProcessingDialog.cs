using System;
using System.Windows.Forms;

namespace DGD.HubGovernor.Jobs
{
    sealed partial class ProcessingDialog: Form
    {
        public ProcessingDialog()
        {
            InitializeComponent();
        }


        public string Message
        {
            get { return m_lblMessage.Text; }

            set { SetMessage(value); }
        }


        //private:
        void SetMessage(string txt)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string>(SetMessage) , txt);
            else
                m_lblMessage.Text = txt;
        }
    }
}
