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

        static NetEngin()
        {
#if !DEBUG
            FluentFTP.FtpTrace.EnableTracing = false;
#endif
        }

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
                string encFilePath = EncodeFile(srcFilePath);

                FluentFTP.FtpVerify flags = FluentFTP.FtpVerify.Throw | FluentFTP.FtpVerify.OnlyChecksum;
                flags |= FluentFTP.FtpVerify.Retry;

                using (new AutoReleaser(() => File.Delete(encFilePath)))
                    try
                    {
                        ftpClient.Connect();
                        ftpClient.UploadFile(encFilePath , destFilePath , createRemoteDir: true , verifyOptions: flags);
                    }
                    catch (FluentFTP.FtpException ex)
                    {
                        throw ex.InnerException ?? ex;
                    }
            }
        }

        public void Upload(string destDir , IEnumerable<string> srcPaths , bool retryOnErr = false)
        {
            Assert(Uri.IsWellFormedUriString(destDir , UriKind.Relative));

            List<FileInfo> files = new List<FileInfo>();

            foreach(string src in srcPaths)
            {
                string tmpFile = EncodeFile(src);
                string destPath = Path.Combine(Path.GetDirectoryName(tmpFile) , Path.GetFileName(src));

                if (File.Exists(destPath))
                    File.Delete(destPath);

                File.Move(tmpFile , destPath);
                files.Add(new FileInfo(destPath));
            }

            FluentFTP.FtpVerify flags = FluentFTP.FtpVerify.Throw | FluentFTP.FtpVerify.OnlyChecksum;
            flags |= FluentFTP.FtpVerify.Retry;

            using (var ftpClient = CreateFtpClient())
            using (new AutoReleaser(() => files.ForEach((fi) => fi.Delete())))
                try
                {
                    ftpClient.Connect();
                    ftpClient.UploadFiles(files , destDir , createRemoteDir: true ,
                        verifyOptions: flags , errorHandling: FluentFTP.FtpError.Throw);
                }
                catch (FluentFTP.FtpException ex)
                {
                    throw ex.InnerException ?? ex;
                }
        }

        public void Download(string destPath , string srcFilePath , bool retryOnErr = false)
        {
            Assert(Uri.IsWellFormedUriString(srcFilePath , UriKind.Relative));

            string tmpFile = Path.GetTempFileName();

            FluentFTP.FtpVerify flags = FluentFTP.FtpVerify.Throw | FluentFTP.FtpVerify.OnlyChecksum;
            flags |= FluentFTP.FtpVerify.Retry;

            using (var ftpClient = CreateFtpClient())
                try
                {
                    ftpClient.Connect();
                    ftpClient.DownloadFile(tmpFile , srcFilePath , verifyOptions: flags);
                }
                catch (FluentFTP.FtpException ex)
                {
                    throw ex.InnerException ?? ex;
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

        public void Download(string destFolder , IEnumerable<string> srcURLs , bool retryOnErr = false)
        {
            Assert(!srcURLs.Any(s => Uri.IsWellFormedUriString(s , UriKind.Relative) == false));

            string tmpDir = Path.GetTempPath();

            FluentFTP.FtpVerify flags = FluentFTP.FtpVerify.Throw | FluentFTP.FtpVerify.OnlyChecksum;
            flags |= FluentFTP.FtpVerify.Retry;

            using (var ftpClient = CreateFtpClient())
                try
                {
                    ftpClient.Connect();
                    ftpClient.DownloadFiles(tmpDir , srcURLs , verifyOptions: flags , errorHandling: FluentFTP.FtpError.Throw);
                }
                catch (FluentFTP.FtpException ex)
                {
                    throw ex.InnerException ?? ex;
                }


            foreach (string src in srcURLs)
            {
                string srcFile = Path.GetFileName(src);
                string dlFile = Path.Combine(tmpDir , srcFile);

                string decFilePath = DecodeFile(dlFile);

                string destPath = Path.Combine(destFolder , srcFile);

                using (FileLocker.Lock(destPath))
                {
                    if (File.Exists(destPath))
                        File.Delete(destPath);

                    File.Move(decFilePath , destPath);
                }

                File.Delete(dlFile);
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
            {
                using (FileLocker.Lock(filePath))
                using (FileStream ifs = File.OpenRead(filePath))
                    ifs.CopyTo(gzs);
            }

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
