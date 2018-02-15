using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using static easyLib.DebugHelper;


namespace DGD.HubCore.Net
{
    public class NetEngin
    {
        readonly ICredential m_credential;

        public NetEngin(ICredential credential)
        {
            m_credential = credential;
        }


        public void Upload(Uri destFileURI , string srcFilePath)
        {
            using (var wClient = new WebClient())
            {
                if (m_credential != null)
                    wClient.Credentials = new NetworkCredential(m_credential.UserName , m_credential.Password);

                LogDbgInfo($"uploading {srcFilePath} to {destFileURI}");

                string encFilePath;
                using (FileLocker.Lock(srcFilePath))
                    encFilePath = EncodeFile(srcFilePath);                

                using (new AutoReleaser(() => File.Delete(encFilePath)))
                    wClient.UploadFile(destFileURI , encFilePath);

                LogDbgInfo("Upload done.");
            }
        }

        public void Upload(Uri destDirUri , IEnumerable<string> srcPaths)
        {
            using (var wClient = new WebClient())
            {
                if (m_credential != null)
                    wClient.Credentials = new NetworkCredential(m_credential.UserName , m_credential.Password);

                foreach (string srcFilePath in srcPaths)
                {
                    var destFileUri = new Uri(destDirUri , Path.GetFileName(srcFilePath));

                    LogDbgInfo($"uploading {srcFilePath} to {destFileUri}");

                    string encFilePath;

                    using (FileLocker.Lock(srcFilePath))
                        encFilePath = EncodeFile(srcFilePath);                    

                    using (new AutoReleaser(() => File.Delete(encFilePath)))
                        wClient.UploadFile(destFileUri , encFilePath);

                    LogDbgInfo("Upload done.");
                }
            }
        }

        public void Download(string destPath , Uri srcURI)
        {
            using (var wClient = new WebClient())
            {
                if (m_credential != null)
                    wClient.Credentials = new NetworkCredential(m_credential.UserName , m_credential.Password);

                LogDbgInfo($"Downloading {srcURI} to {destPath}");

                string tmpFile = Path.GetTempFileName();
                wClient.DownloadFile(srcURI , tmpFile);

                using (new AutoReleaser(() => File.Delete(tmpFile)))
                {
                    string decFilePath = DecodeFile(tmpFile);

                    using (FileLocker.Lock(destPath))
                    {
                        if (File.Exists(destPath))
                            File.Delete(destPath);

                        File.Move(decFilePath , destPath);
                    }

                    LogDbgInfo("Download done.");
                }
            }
        }

        public void Download(string destFolder , IEnumerable<Uri> srcUris)
        {
            using (var wClient = new WebClient())
            {
                if (m_credential != null)
                    wClient.Credentials = new NetworkCredential(m_credential.UserName , m_credential.Password);

                string tmpFile = Path.GetTempFileName();

                using (new AutoReleaser(() => File.Delete(tmpFile)))
                    foreach (Uri srcUri in srcUris)
                    {
                        string destPath = Path.Combine(destFolder , Path.GetFileName(srcUri.ToString()));

                        LogDbgInfo($"Downloading {srcUri} to {destPath}");
                        wClient.DownloadFile(srcUri , tmpFile);


                        string decFilePath = DecodeFile(tmpFile);


                        using (FileLocker.Lock(destPath))
                        {
                            if (File.Exists(destPath))
                                File.Delete(destPath);

                            File.Move(decFilePath , destPath);
                            LogDbgInfo("Download done.");

                        }
                    }
            }
        }


        //private:
        void DummyUpload(Uri destURI , string srcPath)
        {
            using (var wClient = new WebClient())
            {
                System.Diagnostics.Debug.Print($"Uploading from {srcPath} to {destURI}");
                wClient.UploadFile(destURI , srcPath);
            }

        }

        void DummyDownload(string destPath , Uri srcURI)
        {
            using (var wClient = new WebClient())
            {
                System.Diagnostics.Debug.Print($"Downloading from {srcURI} to {destPath}");
                wClient.DownloadFile(srcURI , destPath);
            }
        }

        string EncodeFile(string filePath)
        {
            string tmpFile = Path.GetTempFileName();
            using (FileStream ofs = File.OpenWrite(tmpFile))
            using (var xs = new XorStream(ofs))
            using (var gzs = new GZipStream(xs , CompressionMode.Compress))
            using (FileStream ifs = File.OpenRead(filePath))
                ifs.CopyTo(gzs);

            return tmpFile;
        }

        string DecodeFile(string filePath)
        {
            string tmpFile = Path.GetTempFileName();

            using (FileStream ifs = File.OpenRead(filePath))
            using (var xs = new XorStream(ifs))
            using (var gzs = new GZipStream(xs , CompressionMode.Decompress))
            using (FileStream ofs = File.OpenWrite(tmpFile))
                gzs.CopyTo(ofs);

            return tmpFile;
        }
    }
}
