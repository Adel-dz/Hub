//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace DGD.HubGovernor
//{
//    sealed class NetManager
//    {
//        public void Upload(Uri destFileURI, string srcFileName)
//        {
//            //DummyUpload(destFileURI , srcFileName);


//            //string serverURL = AppContext.Settings.AppSettings.ServerURL;
//            string userName = AppContext.Settings.AppSettings.NetUserName;            
//            string password = AppContext.Settings.AppSettings.NetPassword;
                        
//            using (var wClient = new WebClient())
//            {
//                wClient.Credentials = new NetworkCredential(userName , password);
//                //wClient.BaseAddress = serverURL;
//                System.Diagnostics.Debug.WriteLine($"uploading {srcFileName} to {destFileURI}");
//                wClient.UploadFile(destFileURI , srcFileName);
//            }
//        }


//        //private:
//        void DummyUpload(Uri destURI , string srcPath)
//        {
//            //System.IO.File.Copy(srcFilePath , destFileURI.ToString() , true);

//            using (var wClient = new WebClient())
//            {
//                System.Diagnostics.Debug.Print($"Uploading from {srcPath} to {destURI}");
//                wClient.UploadFile(destURI , srcPath);
//            }

//        }
//    }
//}
