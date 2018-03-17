using System;
using System.Windows.Forms;

namespace DGD.Hub
{
    sealed partial class BusyDialog: Form
    {
        public BusyDialog()
        {
            InitializeComponent();
        }

        public string Message
        {
            get
            {
                if (InvokeRequired)
                    return Invoke(new Func<string>(() => m_lblMessage.Text)) as string;

                return m_lblMessage.Text;
            }
            set
            {
                if (InvokeRequired)
                    Invoke(new Action(() => m_lblMessage.Text = value));
                else
                    m_lblMessage.Text = value;
            }
        }
    }
}
