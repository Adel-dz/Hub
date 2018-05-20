using DGD.HubCore;
using DGD.HubCore.Net;
using System;
using System.IO;



namespace DGD.HubGovernor
{
    sealed class AppPaths
    {
        const string APP_BASE_DIR = "DGD.Governor";
        const string DATA_UPDATE_DIR = "Dep\\";
        const string APP_UPDATE_DIR = "AppDep\\";
        const string TABLES_DIR = "DB\\";
        const string CACHE_DIR = "Cache\\";
        const string DLG_DIR = "Dlg\\";
        const string LOG_DIR = "Log\\";
        const string USER_SETTING_FILE = "user.param";
        const string APP_SETTING_FILE = "app.param";


        public static string UserDataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , APP_BASE_DIR);

        public static string AppDataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) , APP_BASE_DIR);

        public static string UserSettingsFilePath => Path.Combine(UserDataFolder , USER_SETTING_FILE);
        public static string AppSettingsFilePath => Path.Combine(AppDataFolder , APP_SETTING_FILE);
        public static string TablesFolder => Path.Combine(AppDataFolder , TABLES_DIR);
        public static string DataUpdateFolder => Path.Combine(AppDataFolder , DATA_UPDATE_DIR);
        public static string AppUpdateFolder => Path.Combine(AppDataFolder , APP_UPDATE_DIR);
        public static string ManifestPath => Path.Combine(DataUpdateFolder , Names.ManifestFile);
        public static string DataManifestPath => Path.Combine(DataUpdateFolder , Names.DataManifestFile);
        public static string AppManifestPath => Path.Combine(AppUpdateFolder , Names.AppManifestFile);
        public static string DialogFolderPath => Path.Combine(AppDataFolder , DLG_DIR);
        public static string ConnectionRespPath => Path.Combine(DialogFolderPath , Names.ConnectionRespFile);
        public static string ConnectionReqPath => Path.Combine(DialogFolderPath , Names.ConnectionReqFile);
        public static string ProfilesPath => Path.Combine(DialogFolderPath , Names.ProfilesFile);
        public static string LogFolder => Path.Combine(AppDataFolder , LOG_DIR);
        public static string GetClientLogPath(uint clID) => Path.Combine(LogFolder , clID.ToString("X"));
        public static string SysLogPath => Path.Combine(LogFolder , "Sys");

        public static string GetClientDilogFilePath(uint idClient) => Path.Combine(DialogFolderPath ,
            Names.GetClientDialogFile(idClient));

        public static string GetClientDialogURL(uint clientID) => Urls.DialogDirURL + Names.GetClientDialogFile(clientID);

        public static string GetSrvDialogFilePath(uint idClient) => Path.Combine(DialogFolderPath , 
            Names.GetSrvDialogFile(idClient));

        public static string GetSrvDialogURL(uint clientID) => Urls.DialogDirURL + Names.GetSrvDialogFile(clientID);

        public static void CheckFolders()
        {
            if (!Directory.Exists(UserDataFolder))
                Directory.CreateDirectory(UserDataFolder);

            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);


            if (!Directory.Exists(TablesFolder))
                Directory.CreateDirectory(TablesFolder);

            if (!Directory.Exists(DataUpdateFolder))
                Directory.CreateDirectory(DataUpdateFolder);

            if (!Directory.Exists(AppUpdateFolder))
                Directory.CreateDirectory(AppUpdateFolder);

            if (!Directory.Exists(DialogFolderPath))
                Directory.CreateDirectory(DialogFolderPath);
        }

        //private:
        //static Uri RemoteBaseURI => new Uri(AppContext.Settings.AppSettings.ServerURL);
    }
}
