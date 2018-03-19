using DGD.Hub.DB;
using DGD.HubCore;
using DGD.HubCore.Updating;
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

        public static AppArchitecture_t AppArchitecture =>
#if WINXP
            AppArchitecture_t.WinXP;
#elif WIN7SP1X64
           AppArchitecture_t.Win7SP1X64;
#else
            AppArchitecture_t.Win7SP1;
#endif

        public static DialogResult ShowMessage(string msg, string caption = null, 
            MessageBoxButtons btn = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            if (m_mainWindow == null)
                return DialogResult.None;

            Func<DialogResult> showMsg = () =>
            {
                if (caption == null)
                    caption = m_mainWindow.Text;

                return MessageBox.Show(m_mainWindow, msg, caption,btn, icon);
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

        //Entry:
        [STAThread]
        static void Main()
        {
            bool created;            

            m_mtx = new Mutex(true , @"Global\HUB_BoumekouezKhaled" , out created);

            if (!created)                
                return;
            
            try
            {
                using (m_settings = new SettingsManager())
                using (m_runOnceManager = new RunOnce.RunOnceManager())
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    if (!Directory.Exists(SettingsManager.AppDataFolder))
                        Directory.CreateDirectory(SettingsManager.AppDataFolder);

                    if (!Directory.Exists(SettingsManager.UserDataFolder))
                        Directory.CreateDirectory(SettingsManager.UserDataFolder);

                    if (!Directory.Exists(SettingsManager.TablesFolder))
                        Directory.CreateDirectory(SettingsManager.TablesFolder);

                    if (!Directory.Exists(SettingsManager.DialogFolder))
                        Directory.CreateDirectory(SettingsManager.DialogFolder);


                    m_runOnceManager.Run();

                    using (m_tblManager = new TablesManager())
                    using (m_dlgManager = new DLG.DialogManager())
                    {
                        m_mainWindow = new MainWindow();
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
    }
}
