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

        public static string UserDataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , APP_BASE_DIR);

        public static string AppDataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) , APP_BASE_DIR);

        public static string TablesFolder => Path.Combine(AppDataFolder , TABLES_DIR);
        public static string DataUpdateFolder => Path.Combine(AppDataFolder , DATA_UPDATE_DIR);
        public static string AppUpdateFolder => Path.Combine(AppDataFolder , APP_UPDATE_DIR);
        public static string LocalManifestPath => Path.Combine(DataUpdateFolder , Names.ManifestFile);
        public static Uri RemoteManifestURI => Uris.GetManifestURI(RemoteBaseURI);
        public static string LocalDataManifestPath => Path.Combine(DataUpdateFolder , Names.DataManifestFile);
        public static string LocalAppManifestPath => Path.Combine(AppUpdateFolder , Names.AppManifestFile);
        public static Uri RemoteDataMainfestURI => Uris.GetDataMainfestURI(RemoteBaseURI);
        public static Uri RemoteAppMainfestURI => Uris.GetAppMainfestURI(RemoteBaseURI);
        public static Uri RemoteDataUpdateDirUri => Uris.GetDataUpdateDirUri(RemoteBaseURI);
        public static Uri RemoteAppUpdateDirUri => Uris.GetAppUpdateDirUri(RemoteBaseURI);
        public static string LocalDialogFolderPath => Path.Combine(AppDataFolder , DLG_DIR);
        public static Uri RemoteDialogDirUri => Uris.GetDialogDirUri(RemoteBaseURI);
        public static string LocalConnectionRespPath => Path.Combine(LocalDialogFolderPath , Names.ConnectionRespFile);
        public static Uri RemoteConnectionRespUri => Uris.GetConnectionRespUri(RemoteBaseURI);
        public static string LocalConnectionReqPath => Path.Combine(LocalDialogFolderPath , Names.ConnectionReqFile);
        public static Uri RemoteConnectionReqUri => Uris.GetConnectionReqUri(RemoteBaseURI);
        public static string LocalProfilesPath => Path.Combine(LocalDialogFolderPath , Names.ProfilesFile);
        public static Uri RemoteProfilesURI => Uris.GetProfilesURI(RemoteBaseURI);
        public static string LogFolder => Path.Combine(AppDataFolder , LOG_DIR);
        public static string GetClientLogPath(uint clID) => Path.Combine(LogFolder , clID.ToString("X"));
        public static string SrvLogPath => Path.Combine(LogFolder , "Sys");

        public static string GetLocalClientDilogPath(uint idClient) => Path.Combine(LocalDialogFolderPath ,
            Names.GetClientDialogFile(idClient));

        public static Uri GetRemoteClientDialogUri(uint clientID) => new Uri(RemoteDialogDirUri ,
            Names.GetClientDialogFile(clientID));

        public static string GetLocalSrvDialogPath(uint idClient) => Path.Combine(LocalDialogFolderPath , 
            Names.GetSrvDialogFile(idClient));

        public static Uri GetRemoteSrvDialogUri(uint clientID) => new Uri(RemoteDialogDirUri ,
            Names.GetSrvDialogFile(clientID));

        public static void CheckFolders()
        {
            if (!Directory.Exists(UserDataFolder))
                Directory.CreateDirectory(UserDataFolder);

            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            if (!Directory.Exists(TablesFolder))
                Directory.CreateDirectory(TablesFolder);

            if (!Directory.Exists(DataUpdateFolder))
                Directory.CreateDirectory(DataUpdateFolder);

            if (!Directory.Exists(AppUpdateFolder))
                Directory.CreateDirectory(AppUpdateFolder);

            if (!Directory.Exists(LocalDialogFolderPath))
                Directory.CreateDirectory(LocalDialogFolderPath);
        }

        //private:
        static Uri RemoteBaseURI => new Uri(AppContext.Settings.AppSettings.ServerURL);
    }
}
