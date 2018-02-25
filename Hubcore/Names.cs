namespace DGD.HubCore
{
    public static class Names
    {
        public static string ManifestFile => "manifest.gov";
        public static string DataManifestFile => "datamanifest.gov";
        public static string ConnectionReqFile => "cxnreq.hub";
        public static string ConnectionRespFile => "cxnresp.gov";
        public static string ProfilesFile => "profiles.gov";

        public static string GetClientDialogFile(uint clientID) => clientID.ToString("X") + ".hub";
        public static string GetSrvDialogFile(uint clientID) => clientID.ToString("X") + ".gov";
    }
}
