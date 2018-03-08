using System;

namespace DGD.HubCore.Net
{
    public static class Uris
    {
        public static Uri GetManifestURI(Uri baseUri) => new Uri(baseUri , "hub_tst/" + Names.ManifestFile);
        public static Uri GetDataMainfestURI(Uri baseUri) => new Uri(baseUri , "hub_tst/" + Names.DataManifestFile);
        public static Uri GetDataUpdateDirUri(Uri baseUri) => new Uri(baseUri , "hub_tst/updata/");
        public static Uri GetAppUpdateDirUri(Uri baseUri) => new Uri(baseUri , "hub_tst/upapp/");
        public static Uri GetDialogDirUri(Uri baseUri) => new Uri(baseUri , "hub_tst/dlg/");
        public static Uri GetProfilesURI(Uri baseUri) => new Uri(baseUri , "hub_tst/dlg/" + Names.ProfilesFile);
        public static Uri GetConnectionReqUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionReqFile);
        public static Uri GetConnectionRespUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionRespFile);
    }
}
