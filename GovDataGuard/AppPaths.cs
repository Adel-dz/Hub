using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDataGuard
{
    static class AppPaths
    {
        const string APP_BASE_DIR = "DGD.Governor";
        const string DLG_DIR = "Dlg\\";
        const string LOG_DIR = "Log\\";
        const string USER_SETTING_FILE = "user.param";
        const string APP_SETTING_FILE = "app.param";
        const string DATA_UPDATE_DIR = "Dep\\";
        const string APP_UPDATE_DIR = "AppDep\\";
        const string TABLES_DIR = "DB\\";
        const string LOG_FILE = "bkuplog.txt";
        const string PROFILES_FILE = "profiles.gov";


        public static string UserDataFolder => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , APP_BASE_DIR);

        public static string AppDataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) , APP_BASE_DIR);

        public static string UserSettingsFilePath => Path.Combine(UserDataFolder , USER_SETTING_FILE);
        public static string AppSettingsFilePath => Path.Combine(AppDataFolder , APP_SETTING_FILE);
        public static string TablesFolder => Path.Combine(AppDataFolder , TABLES_DIR);
        public static string DataUpdateFolder => Path.Combine(AppDataFolder , DATA_UPDATE_DIR);
        public static string AppUpdateFolder => Path.Combine(AppDataFolder , APP_UPDATE_DIR);
        public static string LogFilePath => Path.Combine(AppDataFolder , LOG_FILE);
        public static string LogFolder => Path.Combine(AppDataFolder , LOG_DIR);
        public static string LocalDialogFolderPath => Path.Combine(AppDataFolder , DLG_DIR);
        public static string LocalProfilesPath => Path.Combine(LocalDialogFolderPath , PROFILES_FILE);
    }
}
