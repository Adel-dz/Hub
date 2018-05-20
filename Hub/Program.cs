using DGD.Hub.DB;
using DGD.HubCore;
using DGD.HubCore.Net;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace DGD.Hub
{
    static class Program
    {
        static TablesManager m_tblManager;
        static SettingsManager m_settings;
        static DLG.DialogManager m_dlgManager;
        static RunOnce.RunOnceManager m_runOnceManager;
        static MainWindow m_mainWindow;
        static Mutex m_mtx;
        static bool m_restarting;

        public static TablesManager TablesManager => m_tblManager;
        public static SettingsManager Settings => m_settings;
        public static DLG.DialogManager DialogManager => m_dlgManager;
        public static RunOnce.RunOnceManager RunOnceManager => m_runOnceManager;
        public static AppArchitecture_t AppArchitecture => GetAppArchitecture();
        public static IConnectionParam NetworkSettings
        {
            get
            {
                var credential = new Credential(Settings.UserName , Settings.Password);
                IProxy proxy = Settings.EnableProxy ? new Proxy(Settings.ProxyHost , Settings.ProxyPort) : null;

                return new ConnectionParam(SettingsManager.ServerURL , credential , proxy);
            }
        }

        public static DialogResult ShowMessage(string msg , string caption = null ,
            MessageBoxButtons btn = MessageBoxButtons.OK , MessageBoxIcon icon = MessageBoxIcon.None)
        {
            if (m_mainWindow == null)
                return DialogResult.None;

            Func<DialogResult> showMsg = () =>
            {
                if (caption == null)
                    caption = m_mainWindow.Text;

                return MessageBox.Show(m_mainWindow , msg , caption , btn , icon);
            };

            if (m_mainWindow.InvokeRequired)
                return (DialogResult)m_mainWindow.Invoke(showMsg);

            return showMsg();
        }

        public static void Restart()
        {
            if (m_mainWindow != null)
            {
                m_restarting = true;

                if (m_mainWindow.InvokeRequired)
                    m_mainWindow.Invoke(new Action(m_mainWindow.Close));
                else
                    m_mainWindow.Close();
            }
            else
                Application.Restart();
        }

        //private:
        static AppArchitecture_t GetAppArchitecture()
        {
#if WINXP
            return AppArchitecture_t.WinXP;
#else
            if (Environment.Is64BitOperatingSystem)
                return AppArchitecture_t.Win7SP1X64;

            return AppArchitecture_t.Win7SP1;
#endif
        }


        //Entry:
        [STAThread]
        static void Main()
        {
            bool created;

            m_mtx = new Mutex(true , @"Global\HUB_BoumekouezKhaled" , out created);

            if (!created)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using (m_settings = new SettingsManager())
                using (m_runOnceManager = new RunOnce.RunOnceManager())
                {
                    var ssEngin = new Jobs.SplashScreenEngin();
                    ssEngin.Start();                    

                    if (!Directory.Exists(SettingsManager.AppDataFolder))
                        Directory.CreateDirectory(SettingsManager.AppDataFolder);

                    if (!Directory.Exists(SettingsManager.UserDataFolder))
                        Directory.CreateDirectory(SettingsManager.UserDataFolder);

                    if (!Directory.Exists(SettingsManager.TablesFolder))
                        Directory.CreateDirectory(SettingsManager.TablesFolder);

                    if (!Directory.Exists(SettingsManager.DialogFolder))
                        Directory.CreateDirectory(SettingsManager.DialogFolder);

                    if (Settings.AutoDetectProxy)
                    {
                        ssEngin.FeedbackText = "Détection des paramètres du serveur proxy...";

                        IProxy proxy = Proxy.DetectProxy();

                        if ((Settings.EnableProxy = proxy != null))
                        {
                            Settings.ProxyHost = proxy.Host;
                            Settings.ProxyPort = proxy.Port;
                        }

                        ssEngin.FeedbackText = "Détection des paramètres du serveur proxy terminée.";
                    }

                    ssEngin.FeedbackText = "Exécution de tâches RunOnce...";
                    m_runOnceManager.Run();
                    ssEngin.FeedbackText = "Exécution de tâches RunOnce terminée.";
                    
                    ssEngin.FeedbackText = "Démarrage en cours...";
                    using (m_tblManager = new TablesManager())
                    using (m_dlgManager = new DLG.DialogManager())
                    {

                        m_mainWindow = new MainWindow();
                        ssEngin.FeedbackText = "Merci d'utiliser le HUB de la valeur en douanes.";
                        Thread.Sleep(2000);

                        m_mainWindow.Shown += delegate { ssEngin.Dispose(); };

                        Application.Run(m_mainWindow);
                        m_mainWindow = null;
                    }

                    Log.LogEngin.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur fatale c’est produite:\n{ex.Message}\nCliquez sur OK pour fermer l'apliaction." ,
                    null ,
                    MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            finally
            {
                if (m_mtx != null)
                {
                    m_mtx.ReleaseMutex();
                    m_mtx.Dispose();
                }
            }

            if (m_restarting)
                Application.Restart();
        }

        private static void Initialize(Jobs.SplashScreen ss)
        {

        }

    }
}
