using DGD.HubCore.Net;
using DGD.HubGovernor.Log;
using easyLib;
using System;
using System.IO;
using System.IO.Compression;

namespace DGD.HubGovernor.Opts
{

    sealed class Settings
    {
        public Settings()
        {
            Load();
        }

        public AppSettings AppSettings { get; } = new AppSettings();
        public UserSettings UserSettings { get; } = new UserSettings();

        public IConnectionParam NetworkSettings
        {
            get
            {
                string host = AppSettings.ServerURL;
                ICredential credantial = null;
                IProxy proxy = null;

                if (string.IsNullOrWhiteSpace(host))
                    host = "ftp://douane.gov.dz";

                string user = AppSettings.UserName;
                string pass = AppSettings.Password;

                if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
                    credantial = new Credential(user , pass);

                if (AppSettings.EnableProxy)
                    proxy = new Proxy(AppSettings.ProxyAddress , AppSettings.ProxyPort);

                return new ConnectionParam(host , credantial , proxy);
            }
        }

        public void Save()
        {
            string appFilePath = AppPaths.AppSettingsFilePath;

            using (FileStream fs = File.Create(appFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Compress))
            {
                var bw = new BinaryWriter(gzs);
                AppSettings.Save(bw);
            }


            string userFilePath = AppPaths.UserSettingsFilePath;

            using (FileStream fs = File.Create(userFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Compress))
            {
                var bw = new BinaryWriter(gzs);
                UserSettings.Save(bw);
            }

        }

        public void Load()
        {
            string appFilePath = AppPaths.AppSettingsFilePath;
#if DEBUG
            System.Diagnostics.Debug.Print($"Lecture du ficher {appFilePath}...\n");
#endif
            try
            {
                using (FileStream fs = File.OpenRead(appFilePath))
                using (var xs = new XorStream(fs))
                using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
                {
                    var br = new BinaryReader(gzs);

                    AppSettings.Load(br);
                }
            }
            catch (Exception ex)
            {
                AppContext.LogManager.LogSysError("Erreur lors du chargement des paramètrs de l'application: " +
                    ex.Message);
            }
            


            string userFilePath = AppPaths.UserSettingsFilePath;
#if DEBUG
            System.Diagnostics.Debug.Print($"Lecture du ficher {userFilePath}...\n");
#endif
            try
            {
                using (FileStream fs = File.OpenRead(userFilePath))
                using (var xs = new XorStream(fs))
                using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
                {
                    var br = new BinaryReader(gzs);
                    UserSettings.Load(br);
                }
            }
            catch (Exception ex)
            {
                AppContext.LogManager.LogSysError("Erreur lors du chargement des paramètrs de l'utilisateur: " +
                    ex.Message);
            }

        }

        public void Reset()
        {
            AppSettings.Reset();
            UserSettings.Reset();
        }
    }
}
