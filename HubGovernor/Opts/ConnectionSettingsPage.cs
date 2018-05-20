using DGD.HubCore.Net;
using DGD.HubGovernor.Log;
using System;
using System.Net;
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

            if (Uri.IsWellFormedUriString(m_tbServerURL.Text , UriKind.Absolute))
                opt.ServerURL = m_tbServerURL.Text;
            else
                TextLogger.Error("Le format de d'adresse du serveur est incorrect. Adresse du serveur ignorée.");
            
            opt.Password = m_tbPassword.Text;
            opt.UserName = m_tbUserName.Text;            
            opt.EnableProxy = m_chkEnableProxy.Checked;
            opt.ProxyAddress = m_tbProxyHost.Text;
            opt.ProxyPort = (ushort)m_nudProxyPort.Value;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            AppSettings opt = AppContext.Settings.AppSettings;

            m_tbServerURL.Text = opt.ServerURL;
            m_tbPassword.Text = opt.Password;
            m_tbUserName.Text = opt.UserName;
            m_chkEnableProxy.Checked = opt.EnableProxy;
            m_nudProxyPort.Value = opt.ProxyPort;
            m_tbProxyHost.Text = opt.ProxyAddress;

            base.OnLoad(e);
        }

        //handlers:
        private void DetectProxy_Click(object sender , EventArgs e)
        {
            try
            {
                AppContext.LogManager.LogSysActivity("Détection des paramètres du proxy..." , true);
                IProxy proxy = Proxy.DetectProxy();

                m_chkEnableProxy.Checked = proxy != null;

                if (proxy != null)
                {
                    AppContext.LogManager.LogSysActivity("Paramètres du proxy détectés." , true);

                    m_tbProxyHost.Text = proxy.Host;
                    m_nudProxyPort.Value = proxy.Port;
                }
                else
                    AppContext.LogManager.LogSysActivity("Aucun proxy détecté." , true);
            }
            catch (Exception ex)
            {
                AppContext.LogManager.LogSysError("Erreur lors de la détection des paramètres du proxy: " +
                    ex.Message , true);
            }
        }

        private void EnableProxy_CheckedChanged(object sender , EventArgs e)
        {
            m_tbProxyHost.Enabled = m_nudProxyPort.Enabled = m_chkEnableProxy.Checked;
        }
    }
}
