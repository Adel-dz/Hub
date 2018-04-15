using DGD.HubCore.Updating;
using DGD.HubGovernor.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Arch
{
    partial class BackupProcessingDialog: Form
    {
        public BackupProcessingDialog()
        {
            InitializeComponent();

            DataCompressed += BackupProcessingDialog_DataCompressed;
            LogsCompressed += BackupProcessingDialog_LogsCompressed;
            UpdatesCompressed += BackupProcessingDialog_UpdatesCompressed;
            SysFilesCompressed += BackupProcessingDialog_SysFilesCompressed;
        }


        public string DataBackupFile { get; private set; }
        public string LogsBackupFile { get; private set; }
        public string UpdatesBackupFile { get; private set; }
        public string SysBackupFile { get; private set; }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            Parallel.Invoke(ProcessDataFiles , ProcessLogFiles , ProcessSysFiles , ProcessUpdatesFiles);
            base.OnLoad(e);
        }


        //private:
        event Action<string> DataCompressed;
        event Action<string> LogsCompressed;
        event Action<string> UpdatesCompressed;
        event Action<string> SysFilesCompressed;


        void InitProgressBar(ProgressBar pb , int maxValue)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<ProgressBar , int>(InitProgressBar) , pb , maxValue);
            else
                pb.Maximum = maxValue;
        }

        void SetProgress(ProgressBar pb , Label lbl , string txt)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<ProgressBar , Label , string>(SetProgress) , pb , lbl , txt);
            else
            {
                ++pb.Value;
                lbl.Text = txt;
            }
        }

        void ProcessDataFiles()
        {
            string[] files = Directory.GetFiles(AppPaths.TablesFolder);
            InitProgressBar(m_pbData , files.Length);

            var bag = new FilesBag();
            bag.FileCompressed += Bag_DataFileCompressed;

            string relDir = AppPaths.TablesFolder.Remove(0 , AppPaths.AppDataFolder.Length + 1);

            foreach (string file in files)
                bag.Add(file , relDir);

            string tmpFile = Path.GetTempFileName();
            try
            {
                bag.Compress(tmpFile);
            }
            catch(Exception ex)
            {
                TextLogger.Error(ex.Message);
            }

            bag.FileCompressed -= Bag_DataFileCompressed;

            DataCompressed?.Invoke(tmpFile);
        }

        void ProcessUpdatesFiles()
        {
            string[] appFiles = Directory.GetFiles(AppPaths.AppUpdateFolder);
            string[] dataFiles = Directory.GetFiles(AppPaths.DataUpdateFolder);

            InitProgressBar(m_pbUpdates , appFiles.Length + dataFiles.Length);

            var bag = new FilesBag();
            bag.FileCompressed += Bag_UpdatesFileCompressed;

            string relDir = AppPaths.AppUpdateFolder.Remove(0 , AppPaths.AppDataFolder.Length + 1);

            foreach (string file in appFiles)
                bag.Add(file , relDir);

            relDir = AppPaths.DataUpdateFolder.Remove(0 , AppPaths.AppDataFolder.Length + 1);

            foreach (string file in dataFiles)
                bag.Add(file , relDir);


            string tmpFile = Path.GetTempFileName();

            try
            {
                bag.Compress(tmpFile);
            }
            catch(Exception ex)
            {
                TextLogger.Error(ex.Message);
            }

            bag.FileCompressed += Bag_UpdatesFileCompressed;

            UpdatesCompressed?.Invoke(tmpFile);
        }


        void ProcessLogFiles()
        {
            string[] files = Directory.GetFiles(AppPaths.LogFolder);
            InitProgressBar(m_pbLogs , files.Length);

            var bag = new FilesBag();
            bag.FileCompressed += Bag_LogFileCompressed;

            string relDir = AppPaths.TablesFolder.Remove(0 , AppPaths.AppDataFolder.Length + 1);

            foreach (string file in files)
                bag.Add(file , relDir);

            string tmpFile = Path.GetTempFileName();

            try
            {
                bag.Compress(tmpFile);
            }
            catch (Exception ex)
            {
                TextLogger.Error(ex.Message);
            }

            bag.FileCompressed -= Bag_LogFileCompressed;

            LogsCompressed?.Invoke(tmpFile);
        }

        void ProcessSysFiles()
        {
            string[,] files =
                {
                { AppPaths.LocalProfilesPath,AppPaths.AppDataFolder },
                { AppPaths.AppSettingsFilePath,AppPaths.AppDataFolder },
                { AppPaths.UserSettingsFilePath,AppPaths.UserDataFolder }
                };

            InitProgressBar(m_pbLogs , files.GetLength(0));

            var bag = new FilesBag();
            bag.FileCompressed += Bag_SysFileCompressed;


            for (int i = 0; i < files.GetLength(0); ++i)
            {
                string file = files[i , 0];
                string relDir = Path.GetDirectoryName(file.Remove(0 , files[i , 1].Length + 1));

                if (string.IsNullOrWhiteSpace(relDir))
                    bag.Add(file);
                else
                    bag.Add(file , relDir);
            }

            string tmpFile = Path.GetTempFileName();

            try
            {
                bag.Compress(tmpFile);
            }
            catch (Exception ex)
            {
                TextLogger.Error(ex.Message);
            }


            bag.FileCompressed -= Bag_SysFileCompressed;

            SysFilesCompressed?.Invoke(tmpFile);
        }

        void CheckEndProcessing()
        {
            if (DataBackupFile != null && LogsBackupFile != null && UpdatesBackupFile != null &&
                    SysBackupFile != null)
                if (InvokeRequired)
                    BeginInvoke(new Action(Close));
                else
                    Close();
        }


        //handlers:
        private void Bag_DataFileCompressed(string file) =>
            SetProgress(m_pbData , m_lblProcessingData , $"Archivage du fichier {file}...");

        private void Bag_UpdatesFileCompressed(string file) =>
            SetProgress(m_pbUpdates , m_lblProcessingUpdates , $"Archivage du fichier {file}...");

        private void Bag_LogFileCompressed(string file) =>
            SetProgress(m_pbLogs , m_lblProcessingLogs , $"Archivage du fichier {file}...");

        private void Bag_SysFileCompressed(string file) =>
            SetProgress(m_pbSysFiles , m_lblProcessingSysFile , $"Archivage du fichier {file}...");

        private void BackupProcessingDialog_SysFilesCompressed(string file)
        {
            SysBackupFile = file;
            CheckEndProcessing();
        }

        private void BackupProcessingDialog_UpdatesCompressed(string file)
        {
            UpdatesBackupFile = file;
            CheckEndProcessing();
        }

        private void BackupProcessingDialog_LogsCompressed(string file)
        {
            LogsBackupFile = file;
            CheckEndProcessing();
        }

        private void BackupProcessingDialog_DataCompressed(string file)
        {
            DataBackupFile = file;
            CheckEndProcessing();
        }

    }
}
