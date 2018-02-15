//using System;
//using System.Net;

//namespace DGD.Hub
//{
//    public class NetEngin
//    {
//        public void Upload(Uri destFileURI, string srcFilePath)
//        {            
//            using (var wClient = new WebClient())
//            {
//                string userName = Program.Settings.NetUserName;
//                string password = Program.Settings.NetPassword;
//                wClient.Credentials = new NetworkCredential(userName , password);
//                System.Diagnostics.Debug.WriteLine($"uploading {srcFilePath} to {destFileURI}");
//                wClient.UploadFile(destFileURI , srcFilePath);
//            }
//        }

//        public void Download(string destPath, Uri srcURI)
//        {
//            using (var wClient = new WebClient())
//            {
//                string userName = Program.Settings.NetUserName;
//                string password = Program.Settings.NetPassword;
//                wClient.Credentials = new NetworkCredential(userName , password);
//                //wClient.BaseAddress = Program.Settings.ServerURI;
//                System.Diagnostics.Debug.WriteLine($"Downloading {srcURI} to {destPath}");
//                wClient.DownloadFile(srcURI, destPath);
//            }
//        }

//        //private:
//        void DummyUpload(Uri destURI , string srcPath)
//        {
//            using (var wClient = new WebClient())
//            {
//                System.Diagnostics.Debug.Print($"Uploading from {srcPath} to {destURI}");
//                wClient.UploadFile(destURI , srcPath);
//            }

//        }

//        void DummyDownload(string destPath, Uri srcURI)
//        {
//            using (var wClient = new WebClient())
//            {
//                System.Diagnostics.Debug.Print($"Downloading from {srcURI} to {destPath}");
//                wClient.DownloadFile(srcURI , destPath);
//            }
//        }
//    }
//}
