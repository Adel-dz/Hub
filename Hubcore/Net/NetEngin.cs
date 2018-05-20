using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using static easyLib.DebugHelper;


namespace DGD.HubCore.Net
{
    public class NetEngin
    {
        static readonly int[] m_waitTimes = { 2 , 3 , 5 };// , 7 , 11 , 13 , 17 , 19 , 23 , 29 };
        readonly IConnectionParam m_cxnParam;

        public NetEngin(IConnectionParam connectionParam)
        {
            Assert(connectionParam != null);

            m_cxnParam = connectionParam;
        }


        public void Upload(string destFilePath , string srcFilePath , bool retryOnErr = false)
        {
            Assert(Uri.IsWellFormedUriString(destFilePath , UriKind.Relative));

            using (var ftpClient = CreateFtpClient())
            {
                LogDbgInfo($"uploading {srcFilePath} to {destFilePath}");

                string encFilePath;
                using (FileLocker.Lock(srcFilePath))
                    encFilePath = EncodeFile(srcFilePath);

                using (new AutoReleaser(() => File.Delete(encFilePath)))
                    if (retryOnErr)
                    {
                        int ndxSleepTime = 0;

                        while (true)
                            try
                            {
                                if (!ftpClient.IsConnected)
                                    ftpClient.Connect();

                                ftpClient.UploadFile(encFilePath , destFilePath);
                                LogDbgInfo("Upload done.");
                                break;
                            }
                            catch (Exception ex)
                            {
                                LogDbgInfo(ex.Message);

                                if (ndxSleepTime == m_waitTimes.Length)
                                    throw;

                                LogDbgInfo($"Seleeping {m_waitTimes[ndxSleepTime]} seconds.");
                                Thread.Sleep(m_waitTimes[ndxSleepTime++] * 1000);
                                LogDbgInfo("Retrying...");
                                continue;
                            }
                    }
                    else
                    {
                        ftpClient.Connect();
                        ftpClient.UploadFile(encFilePath , destFilePath);
                        LogDbgInfo("Upload done.");
                    }
            }
        }

        public void Upload(string destDir , IEnumerable<string> srcPaths , bool retryOnErr = false)
        {
            Assert(Uri.IsWellFormedUriString(destDir , UriKind.Relative));

            if (!destDir.EndsWith("/"))
                destDir += '/';

            using (var ftpClient = CreateFtpClient())
                foreach (string srcFilePath in srcPaths)
                {
                    var destFilePath = destDir + Path.GetFileName(srcFilePath);

                    LogDbgInfo($"uploading {srcFilePath} to {destFilePath}");

                    string encFilePath;

                    using (FileLocker.Lock(srcFilePath))
                        encFilePath = EncodeFile(srcFilePath);

                    using (new AutoReleaser(() => File.Delete(encFilePath)))
                        if (retryOnErr)
                        {
                            int ndxSleepTime = 0;

                            while (true)
                                try
                                {
                                    if (!ftpClient.IsConnected)
                                        ftpClient.Connect();

                                    ftpClient.UploadFile(encFilePath , destFilePath);
                                    LogDbgInfo("Upload done.");
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    LogDbgInfo(ex.Message);

                                    if (ndxSleepTime == m_waitTimes.Length)
                                        throw;

                                    LogDbgInfo($"Seleeping {m_waitTimes[ndxSleepTime]} seconds.");
                                    Thread.Sleep(m_waitTimes[ndxSleepTime++] * 1000);
                                    LogDbgInfo("Retrying...");
                                    continue;
                                }
                        }
                        else
                        {
                            ftpClient.Connect();
                            ftpClient.UploadFile(encFilePath , destFilePath);
                            LogDbgInfo("Upload done.");
                        }
                }
        }

        public void Download(string destPath , string srcFilePath , bool retryOnErr = false)
        {
            Assert(Uri.IsWellFormedUriString(srcFilePath , UriKind.Relative));

            using (var ftpClient = CreateFtpClient())
            {
                LogDbgInfo($"Downloading {srcFilePath} to {destPath}");

                string tmpFile = Path.GetTempFileName();

                if (retryOnErr)
                {
                    int ndxSleepTime = 0;

                    while (true)
                        try
                        {
                            if (!ftpClient.IsConnected)
                                ftpClient.Connect();

                            ftpClient.DownloadFile(tmpFile , srcFilePath);
                            LogDbgInfo("Download done.");
                            break;
                        }
                        catch (Exception ex)
                        {
                            LogDbgInfo(ex.Message);

                            if (ndxSleepTime == m_waitTimes.Length)
                                throw;

                            LogDbgInfo($"Seleeping {m_waitTimes[ndxSleepTime]} seconds.");
                            Thread.Sleep(m_waitTimes[ndxSleepTime++] * 1000);
                            LogDbgInfo("Retrying...");
                            continue;
                        }
                }
                else
                {
                    ftpClient.Connect();
                    ftpClient.DownloadFile(tmpFile , srcFilePath);
                    LogDbgInfo("Download done.");
                }

                using (new AutoReleaser(() => File.Delete(tmpFile)))
                {
                    string decFilePath = DecodeFile(tmpFile);

                    using (FileLocker.Lock(destPath))
                    {
                        if (File.Exists(destPath))
                            File.Delete(destPath);

                        File.Move(decFilePath , destPath);
                    }

                }
            }
        }

        public void Download(string destFolder , IEnumerable<string> srcURLs , bool retryOnErr = false)
        {
            Assert(!srcURLs.Any(s => Uri.IsWellFormedUriString(s, UriKind.Relative) == false));

            using (var ftpClient = CreateFtpClient())
            {
                string tmpFile = Path.GetTempFileName();

                using (new AutoReleaser(() => File.Delete(tmpFile)))
                    foreach (string srcUrl in srcURLs)
                    {
                        string destPath = Path.Combine(destFolder , Path.GetFileName(srcUrl));

                        LogDbgInfo($"Downloading {srcUrl} to {destPath}");

                        if (retryOnErr)
                        {
                            int ndxSleepTime = 0;

                            while (true)
                                try
                                {
                                    if (!ftpClient.IsConnected)
                                        ftpClient.Connect();

                                    ftpClient.DownloadFile(tmpFile , srcUrl);
                                    LogDbgInfo("Download done.");
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    LogDbgInfo(ex.Message);

                                    if (ndxSleepTime == m_waitTimes.Length)
                                        throw;

                                    LogDbgInfo($"Seleeping {m_waitTimes[ndxSleepTime]} seconds.");
                                    Thread.Sleep(m_waitTimes[ndxSleepTime++] * 1000);
                                    LogDbgInfo("Retrying...");
                                    continue;
                                }
                        }
                        else
                        {
                            ftpClient.Connect();
                            ftpClient.DownloadFile(tmpFile , srcUrl);
                            LogDbgInfo("Download done.");
                        }


                        string decFilePath = DecodeFile(tmpFile);
                        
                        using (FileLocker.Lock(destPath))
                        {
                            if (File.Exists(destPath))
                                File.Delete(destPath);

                            File.Move(decFilePath , destPath);
                        }
                    }
            }
        }


        //private:
        FluentFTP.IFtpClient CreateFtpClient()
        {
            FluentFTP.IFtpClient ftpClient;

            if (m_cxnParam.Proxy != null)
            {
                var proxy = new FluentFTP.Proxy.ProxyInfo();
                proxy.Host = m_cxnParam.Proxy.Host;
                proxy.Port = m_cxnParam.Proxy.Port;
                ftpClient = new FluentFTP.Proxy.FtpClientHttp11Proxy(proxy);
                ftpClient.Host = m_cxnParam.Host;
                ftpClient.DataConnectionType = FluentFTP.FtpDataConnectionType.PASV;
            }
            else
                ftpClient = new FluentFTP.FtpClient(m_cxnParam.Host);

            if (m_cxnParam.Credential != null)
                ftpClient.Credentials = new NetworkCredential(m_cxnParam.Credential.UserName , m_cxnParam.Credential.Password);

            return ftpClient;
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
