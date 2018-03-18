﻿using System;

namespace DGD.HubCore.Net
{
    public static class Uris
    {
#if DEBUG
        const string ROOT_DIR = "hub_tst/";
#else
        const string ROOT_DIR = "hub/";
#endif


        public static Uri GetManifestURI(Uri baseUri) => new Uri(baseUri , ROOT_DIR + Names.ManifestFile);
        public static Uri GetDataMainfestURI(Uri baseUri) => new Uri(baseUri , ROOT_DIR + Names.DataManifestFile);
        public static Uri GetDataUpdateDirUri(Uri baseUri) => new Uri(baseUri , ROOT_DIR + "updata/");
        public static Uri GetAppUpdateDirUri(Uri baseUri) => new Uri(baseUri , ROOT_DIR + "upapp/");
        public static Uri GetAppMainfestURI(Uri baseUri) => new Uri(baseUri , ROOT_DIR + Names.AppManifestFile);
        public static Uri GetDialogDirUri(Uri baseUri) => new Uri(baseUri , ROOT_DIR + "dlg/");
        public static Uri GetProfilesURI(Uri baseUri) => new Uri(baseUri , ROOT_DIR + "dlg/" + Names.ProfilesFile);
        public static Uri GetConnectionReqUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionReqFile);
        public static Uri GetConnectionRespUri(Uri baseUri) => new Uri(GetDialogDirUri(baseUri) , Names.ConnectionRespFile);
    }
}
