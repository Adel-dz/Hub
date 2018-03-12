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

        public static TablesManager TablesManager => m_tblManager;
        public static SettingsManager Settings => m_settings;
        public static DLG.DialogManager DialogManager => m_dlgManager;

        public static AppArchitecture_t AppArchitecture =>
#if WINXP
            AppArchitecture_t.WinXP;
#elif WIN7SP1X64
           AppArchitecture_t.Win7SP1X64;
#else
            AppArchitecture_t.Win7SP1;
#endif

        //private:

        //Entry:
        [STAThread]
        static void Main()
        {
            bool created;
            Mutex mtx = null;

            try
            {
                mtx = new Mutex(true , @"Global\HUB_BoumekouezKhaled" , out created);

                if (created)
                {
                    using (m_settings = new SettingsManager())
                    using (m_tblManager = new TablesManager())
                    using (m_dlgManager = new DLG.DialogManager())
                    {
                        if (!Directory.Exists(SettingsManager.AppDataFolder))
                            Directory.CreateDirectory(SettingsManager.AppDataFolder);

                        if (!Directory.Exists(SettingsManager.UserDataFolder))
                            Directory.CreateDirectory(SettingsManager.UserDataFolder);

                        if (!Directory.Exists(SettingsManager.TablesFolder))
                            Directory.CreateDirectory(SettingsManager.TablesFolder);

                        if (!Directory.Exists(SettingsManager.DialogFolder))
                            Directory.CreateDirectory(SettingsManager.DialogFolder);


                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        Application.Run(new MainWindow());
                    }

                    Log.LogEngin.Dispose();
                }
                else
                {
                    Dbg.Log("App already running!");
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
                if (mtx != null)
                {
                    mtx.ReleaseMutex();
                    mtx.Dispose();
                }
            }
        }
    }
}
