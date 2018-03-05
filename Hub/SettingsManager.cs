using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using easyLib;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;


namespace DGD.Hub
{
    sealed class SettingsManager: IDisposable, ICredential
    {
        const string APP_SETTINGS_SIGNATURE = "HUBAPPPARAM1";
        const string USER_SETTINGS_SIGNATURE = "HUBUSERPARAM1";
        const string APP_BASE_FOLDER = "DGD.Hub";
        const string APP_REGISTRY_KEY = @"Software\DGD.Hub";
        const int DEFAULT_MRU_SIZE = 100;

        uint m_dataGeneration;
        uint m_updateKey;
        ClientInfo m_clInfo;


        public SettingsManager()
        {
            LoadAppSettings();

            try
            {
                LoadUserSettings();
            }
            catch
            {
                if (MRUSubHeading == null)
                {
                    MRUSubHeading = new MRUList<SubHeading>(DEFAULT_MRU_SIZE);
                }
            }
        }

        public string UserName => DecodeString(new byte[] { 169 , 158 , 147 , 154 , 138 , 141 });
        public string Password => DecodeString(new byte[] { 137 , 158 , 147 , 154 , 138 , 141 });
        public bool IsMaximized { get; set; }
        public Rectangle FrameRectangle { get; set; }
        public MRUList<SubHeading> MRUSubHeading { get; private set; }

        public int MRUSubHeadingSize
        {
            get { return MRUSubHeading.Capacity; }

            set
            {
                if (MRUSubHeading.Capacity == value)
                    return;

                var mruList = new MRUList<SubHeading>(MRUSubHeading , value);
                MRUSubHeading = mruList;
            }
        }

        public ClientInfo ClientInfo
        {
            get
            {
                if (m_clInfo == null)
                {
                    byte[] fileData = LoadCriticalDataFromFile();
                    byte[] regData = LoadCriticalDataFromRegistry();
                    byte[] data = regData;

                    //sync file data and reg data
                    if(fileData == null || fileData.Length == 0)
                    {
                        if (regData != null && regData.Length > 0)
                            SaveCriticalDataToFile(regData);
                    }
                    else if(regData == null || regData.Length == 0)
                    {
                        SaveCriticalDataToRegistry(fileData);
                        data = fileData;
                    }
                    else
                    {
                        bool same = fileData.Length == regData.Length;

                        if(same)
                            for(int i = 0; i < regData.Length;++i)
                                if(regData[i] != fileData[i])
                                {
                                    data = regData;
                                    same = false;
                                    break;
                                }

                        if (!same)
                            SaveCriticalDataToFile(regData);
                    }

                    if (data != null && data.Length > 0)
                    {
                        var ms = new MemoryStream(data);
                        var reader = new RawDataReader(ms , Encoding.UTF8);

                        m_clInfo = ClientInfo.LoadClientInfo(reader);
                    }
                }

                return m_clInfo;
            }

            set
            {
                m_clInfo = value;

                byte[] data;

                if (value == null)
                {
                    data = new byte[0];
                }
                else
                {
                    var ms = new MemoryStream();
                    var writer = new RawDataWriter(ms , Encoding.UTF8);
                    value.Write(writer);

                    data = ms.ToArray();
                }

                SaveCriticalDataToFile(data);
                SaveCriticalDataToRegistry(data);
            }
        }

        public uint DataGeneration
        {
            get { return m_dataGeneration; }

            set
            {
                m_dataGeneration = value;
                SaveAppSettings();
            }
        }

        public uint UpdateKey
        {
            get { return m_updateKey; }

            set
            {
                m_updateKey = value;
                SaveAppSettings();
            }
        }

        public static Uri ServerURI => new Uri("ftp://douane.gov.dz");
        public static Uri DataUpdateDirURI => Uris.GetUpdateDataDirUri(ServerURI);
        public static Uri ManifestURI => Uris.GetManifestURI(ServerURI);
        public static Uri DataManifestURI => Uris.GetDataMainfestURI(ServerURI);
        public static Uri ProfilesURI => Uris.GetProfilesURI(ServerURI);
        public static Uri ConnectionReqURI => Uris.GetConnectionReqUri(ServerURI);
        public static Uri ConnectionRespURI => Uris.GetConnectionRespUri(ServerURI);
        public static int DialogTimerInterval => 30 * 1000;
        public static int UpdateTimerInterval => 1 * 60 * 1000;
        public static int ConnectionTimerInterval => 30 * 1000;
        public static int MaxConnectAttemps => 3;

        public static string AppDataFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) , APP_BASE_FOLDER);

        public static string UserDataFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , APP_BASE_FOLDER);

        public static string TablesFolder => Path.Combine(AppDataFolder , "Tbl");
        public static string DialogFolder => Path.Combine(AppDataFolder , "Dlg");
        public static Uri DialogDirUri => Uris.GetDialogDirUri(ServerURI);


        public static string GetClientDialogFilePath(uint idClient) =>
            Path.Combine(DialogFolder , Names.GetClientDialogFile(idClient));

        public static Uri GetClientDialogURI(uint clientID) =>
            new Uri(Uris.GetDialogDirUri(ServerURI) , Names.GetClientDialogFile(clientID));

        public static string GetSrvDialogFilePath(uint idClient) =>
            Path.Combine(DialogFolder , Names.GetSrvDialogFile(idClient));

        public static Uri GetServerDialogURI(uint clientID) =>
          new Uri(Uris.GetDialogDirUri(ServerURI) , Names.GetSrvDialogFile(clientID));

        public void Dispose()
        {
            SaveAppSettings();
            SaveUserSettings();
        }


        //private:
        static string AppSettingsFilePath => Path.Combine(AppDataFolder , "param");
        static string UserSettingsFilePath => Path.Combine(UserDataFolder , "param");
        static string CriticalDataFilePath => Path.Combine(AppDataFolder , "bootstrap");


        static string DecodeString(byte[] data)
        {
            var ms = new MemoryStream(data);
            var xs = new XorStream(ms);

            byte[] buff = new byte[data.Length];
            xs.Read(buff , 0 , buff.Length);

            return Encoding.UTF8.GetString(buff);
        }

        void SaveAppSettings()
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            using (FileStream fs = File.Create(AppSettingsFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Compress))
            {
                var writer = new RawDataWriter(xs , Encoding.UTF8);

                writer.Write(Encoding.UTF8.GetBytes(APP_SETTINGS_SIGNATURE));
                writer.Write(m_dataGeneration);
                writer.Write(m_updateKey);
            }
        }

        void SaveUserSettings()
        {
            if (!Directory.Exists(UserDataFolder))
                Directory.CreateDirectory(UserDataFolder);

            using (FileStream fs = File.Create(UserSettingsFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Compress))
            {
                var writer = new RawDataWriter(xs , Encoding.UTF8);

                writer.Write(Encoding.UTF8.GetBytes(USER_SETTINGS_SIGNATURE));
                writer.Write(IsMaximized);
                writer.Write(FrameRectangle.Left);
                writer.Write(FrameRectangle.Top);
                writer.Write(FrameRectangle.Width);
                writer.Write(FrameRectangle.Height);

                writer.Write(MRUSubHeadingSize);
                writer.Write(MRUSubHeading.Count);

                foreach (SubHeading sh in MRUSubHeading)
                    writer.Write(sh.Value);

            }
        }

        void LoadUserSettings()
        {
            //if (!File.Exists(UserSettingsFilePath))
            //    return;

            using (FileStream fs = File.OpenRead(UserSettingsFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
            {
                var reader = new RawDataReader(xs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(USER_SETTINGS_SIGNATURE);

                for (int i = 0; i < sign.Length; ++i)
                    if (sign[i] != reader.ReadByte())
                        throw new CorruptedFileException(UserSettingsFilePath);

                IsMaximized = reader.ReadBoolean();
                int x = reader.ReadInt();
                int y = reader.ReadInt();
                int w = reader.ReadInt();
                int h = reader.ReadInt();

                FrameRectangle = new Rectangle(x , y , w , h);


                int mruSize = reader.ReadInt();
                int mruCount = reader.ReadInt();
                MRUSubHeading = new MRUList<SubHeading>(mruSize);

                for (int i = 0; i < mruCount; ++i)
                    MRUSubHeading.Add(new SubHeading(reader.ReadULong()));
            }
        }

        void LoadAppSettings()
        {
            if (!File.Exists(AppSettingsFilePath))
                return;

            using (FileStream fs = File.OpenRead(AppSettingsFilePath))
            using (var xs = new XorStream(fs))
            using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
            {
                var reader = new RawDataReader(xs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(APP_SETTINGS_SIGNATURE);

                for (int i = 0; i < sign.Length; ++i)
                    if (sign[i] != reader.ReadByte())
                        throw new CorruptedFileException(AppSettingsFilePath);

                m_dataGeneration = reader.ReadUInt();
                m_updateKey = reader.ReadUInt();
            }

        }

        void SaveCriticalDataToFile(byte[] data)
        {
            data = Encode(data);
            File.WriteAllBytes(CriticalDataFilePath , data);
        }

        byte[] LoadCriticalDataFromFile()
        {
            try
            {
                byte[] data = File.ReadAllBytes(CriticalDataFilePath);
                return Encode(data);
            }
            catch
            {
                return null;
            }
        }

        void SaveCriticalDataToRegistry(byte[] data)
        {
            data = Encode(data);

            using (RegistryKey appKey = Registry.CurrentUser.CreateSubKey(APP_REGISTRY_KEY))
                appKey.SetValue(null , data , RegistryValueKind.Binary);
        }

        byte[] LoadCriticalDataFromRegistry()
        {
            byte[] data = null;

            using (RegistryKey appKey = Registry.CurrentUser.OpenSubKey(APP_REGISTRY_KEY))
                if (appKey != null)
                    data = appKey.GetValue(null) as byte[];

            if (data != null)
                data = Encode(data);

            return data;
        }

        byte[] Encode(byte[] data)
        {
            if (data.Length > 1)
            {
                byte key = data[0];
                byte[] encData = new byte[data.Length];
                encData[0] = key;

                for (int i = 1; i < data.Length; ++i)
                    encData[i] = (byte)(key ^ data[i]);

                return encData;
            }

            return data;
        }
    }
}
