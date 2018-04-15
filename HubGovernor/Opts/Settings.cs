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
            try
            {
                Load();
            }
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);

                TextLogger.Warning("Erreur lors du chargement des paramètrs.");
            }
        }

        public AppSettings AppSettings { get; } = new AppSettings();
        public UserSettings UserSettings { get; } = new UserSettings();

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

            using (FileStream fs = File.OpenRead(appFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
            {
                var br = new BinaryReader(gzs);
                AppSettings.Load(br);
            }


            string userFilePath = AppPaths.UserSettingsFilePath;
#if DEBUG
            System.Diagnostics.Debug.Print($"Lecture du ficher {userFilePath}...\n");
#endif

            using (FileStream fs = File.OpenRead(userFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
            {
                var br = new BinaryReader(gzs);
                UserSettings.Load(br);
            }

            
        }

        public void Reset()
        {
            AppSettings.Reset();
            UserSettings.Reset();
        }
    }
}
