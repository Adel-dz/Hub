﻿#define WIN7SP1

using DGD.Hub.DB;
using DGD.HubCore.Updating;
using System;
using System.IO;
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
#if WIN7SP1
            AppArchitecture_t.Win7SP1;
#elif WIN7SP1X64
           TargetSystem_t.Win7SP1X64;
#else
            TargetSystem_t.WinXP;
#endif

        //private:

        //Entry:
        [STAThread]
        static void Main()
        {
            using(m_settings = new SettingsManager())
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

    }
}
