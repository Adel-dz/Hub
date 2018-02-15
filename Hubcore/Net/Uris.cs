using System;

namespace DGD.HubCore.Net
{
    public static class Uris
    {
        public static Uri GetManifestURI(Uri baseUri) => new Uri(baseUri , "hub/" + Names.ManifestFile);
        public static Uri GetDataMainfestURI(Uri baseUri) => new Uri(baseUri , "hub/" + Names.DataManifestFile);
        public static Uri GetUpdateDataDirUri(Uri baseUri) => new Uri(baseUri , "hub/updata/");
        public static Uri GetDialogDirUri(Uri baseUri) => new Uri(baseUri , "hub/dlg/");
        public static Uri GetProfilesURI(Uri baseUri) => new Uri(baseUri , "hub/dlg/" + Names.ProfilesFile);
        public static Uri GetConnectionReqUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionReqFile);
        public static Uri GetConnectionRespUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionRespFile);
    }
}
