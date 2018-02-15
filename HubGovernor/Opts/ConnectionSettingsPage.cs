using System;
using System.Windows.Forms;

namespace DGD.HubGovernor.Opts
{
    sealed partial class ConnectionSettingsPage: UserControl, ISettingsPage
    {
        public ConnectionSettingsPage()
        {
            InitializeComponent();
        }


        public void Apply()
        {
            AppSettings opt = AppContext.Settings.AppSettings;
            opt.Password = m_tbPassword.Text;
            opt.UserName = m_tbUserName.Text;
            opt.ServerURL = m_tbServerURL.Text;            
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            AppSettings opt = AppContext.Settings.AppSettings;

            m_tbServerURL.Text = opt.ServerURL;
            m_tbPassword.Text = opt.Password;
            m_tbUserName.Text = opt.UserName;

            base.OnLoad(e);
        }
    }
}
