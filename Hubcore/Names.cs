namespace DGD.HubCore
{
    public static class Names
    {
        public static string ManifestFile => "manifest.g";
        public static string DataManifestFile => "datamanifest.g";
        public static string ConnectionReqFile => "cxnreq.h";
        public static string ConnectionRespFile => "cxnresp.g";
        public static string ProfilesFile => "profiles.g";

        public static string GetClientDialogFile(uint clientID) => clientID.ToString("x") + ".h";
        public static string GetSrvDialogFile(uint clientID) => clientID.ToString("x") + ".g";
    }
}
