using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using easyLib.DB;
using easyLib.Extensions;

namespace GovDataGuard
{
    partial class BackupLastStagePage: UserControl
    {
        long m_totalSize;

        public event Action ProcessingDone;


        public BackupLastStagePage()
        {
            InitializeComponent();
        }

        public uint DataVersion { get; set; }
        public string DestFolder { get; set; }
        public string DataSourceFile { get; set; }
        public string LogsSourceFile { get; set; }
        public string UpdatesSourceFile { get; set; }
        public string SysSourceFile { get; set; }

        public void Start()
        {
            m_totalSize = new FileInfo(DataSourceFile).Length +
                new FileInfo(LogsSourceFile).Length +
                new FileInfo(UpdatesSourceFile).Length +
                new FileInfo(SysSourceFile).Length;

            m_progressBar.Maximum = int.MaxValue;
            ProcessingDone += EndProcessing;

            Action<Task> onErr = t =>
            {
                File.WriteAllText(AppPaths.LogFilePath , t.Exception.InnerException.Message);
                System.Diagnostics.Process.Start(Path.Combine(@".\" , "HubGovernor.exe"));
                Application.Exit();
            };

            var task = new Task(CreateBackup , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.Start();            
        }
        

        //private:
        void SetProgress(long len)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<long>(SetProgress) , len);
            else
            {
                int val = (int)((len * int.MaxValue) / m_totalSize);

                if (val < 0)
                    val = int.MaxValue;

                m_progressBar.Value = val;
            }
        }

        void EndProcessing()
        {
            if (InvokeRequired)
                BeginInvoke(new Action(EndProcessing));
            else
            {
                System.Diagnostics.Process.Start(Path.Combine(@".\" , "HubGovernor.exe"));
                Application.Exit();
            }
        }

        void CreateBackup()
        {
            if (!Directory.Exists(DestFolder))
                Directory.CreateDirectory(DestFolder);

            string fileName = $"Sauvegarde du {DateTime.Now}.gss";
            fileName = fileName.Replace('/' , '-');
            fileName = fileName.Replace(':' , '.');

            using (var fs = new FileStream(Path.Combine(DestFolder, fileName) , FileMode.Create))
            {
                var writer = new TableWriter(fs);

                writer.Write(AppData.FileSignature);
                writer.Write(DateTime.Now);
                writer.Write(DataVersion);


                const int SZ_BUFFER = 1014;
                byte[] buffer = new byte[SZ_BUFFER];
                long copiedBytes = 0;

                using (FileStream fsData = File.OpenRead(DataSourceFile))
                {
                    long fileLen = fsData.Length;
                    writer.Write((byte)BackupFile_t.Data);
                    writer.Write(fileLen);

                    while (fileLen > 0)
                    {
                        int len = Math.Min(SZ_BUFFER , (int)fileLen);

                        len = fsData.Read(buffer , 0 , len);
                        writer.Write(buffer , 0 , len);
                        fileLen -= len;
                        copiedBytes += len;
                        SetProgress(copiedBytes);
                    }
                }

                File.Delete(DataSourceFile);


                using (FileStream fsUpdates = File.OpenRead(UpdatesSourceFile))
                {
                    long fileLen = fsUpdates.Length;
                    writer.Write((byte)BackupFile_t.Updates);
                    writer.Write(fileLen);

                    while (fileLen > 0)
                    {
                        int len = Math.Min(SZ_BUFFER , (int)fileLen);

                        len = fsUpdates.Read(buffer , 0 , len);
                        writer.Write(buffer , 0 , len);
                        fileLen -= len;
                        copiedBytes += len;
                        SetProgress(copiedBytes);
                    }
                }

                File.Delete(UpdatesSourceFile);


                using (FileStream fsLogs = File.OpenRead(LogsSourceFile))
                {
                    long fileLen = fsLogs.Length;
                    writer.Write((byte)BackupFile_t.Logs);
                    writer.Write(fileLen);                    

                    while (fileLen > 0)
                    {
                        int len = Math.Min(SZ_BUFFER , (int)fileLen);

                        len = fsLogs.Read(buffer , 0 , len);
                        writer.Write(buffer , 0 , len);
                        fileLen -= len;
                        copiedBytes += len;
                        SetProgress(copiedBytes);
                    }
                }

                File.Delete(LogsSourceFile);


                using (FileStream fsSys = File.OpenRead(SysSourceFile))
                {
                    long fileLen = fsSys.Length;
                    writer.Write((byte)BackupFile_t.Sys);
                    writer.Write(fileLen);

                    while (fileLen > 0)
                    {
                        int len = Math.Min(SZ_BUFFER , (int)fileLen);

                        len = fsSys.Read(buffer , 0 , len);
                        writer.Write(buffer , 0 , len);
                        fileLen -= len;
                        copiedBytes += len;
                        SetProgress(copiedBytes);
                    }                    
                }

                File.Delete(SysSourceFile);
            }

            ProcessingDone?.Invoke();
        }
    }
}
