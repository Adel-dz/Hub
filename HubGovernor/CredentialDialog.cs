using System.Windows.Forms;

namespace DGD.HubGovernor
{
    sealed partial class CredentialDialog: Form
    {
        public CredentialDialog()
        {
            InitializeComponent();
        }


        public string ServerURI
        {
            get { return m_bServerURL.Text; }
            set { m_bServerURL.Text = value; }
        }

        public string UserName
        {
            get { return m_tbUserName.Text; }
            set { m_tbUserName.Text = value; }
        }

        public string Password
        {
            get { return m_tbPassword.Text; }
            set { m_tbPassword.Text = value; }
        }

        public bool SavePassword
        {
            get { return m_chkSavePassword.Checked; }
            set { m_chkSavePassword.Checked = value; }
        }
    }
}
