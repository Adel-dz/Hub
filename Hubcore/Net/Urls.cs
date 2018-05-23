using System;

namespace DGD.HubCore.Net
{
    public static class Urls
    {
#if DEBUG
        const string ROOT_DIR = "/hub_tst/";
#else
        const string ROOT_DIR = "/hub/";
#endif


        public static string ManifestURL => ROOT_DIR + Names.ManifestFile;
        public static string DataManifestURL => ROOT_DIR + Names.DataManifestFile;
        public static string DataUpdateDirURL => ROOT_DIR + "updata/";
        public static string AppUpdateDirURL => ROOT_DIR + "upapp/";
        public static string AppManifestURL => ROOT_DIR + Names.AppManifestFile;
        public static string DialogDirURL => ROOT_DIR + "dlg/";
        public static string ProfilesURL => ROOT_DIR + "dlg/" + Names.ProfilesFile;
        public static string ConnectionReqURL => DialogDirURL + Names.ConnectionReqFile;
        public static string ConnectionRespURL => DialogDirURL + Names.ConnectionRespFile;
    }
}
