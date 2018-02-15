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
            get { return m_lblMessage.Text; }
            set { m_lblMessage.Text = value; }
        }       
    }
}
