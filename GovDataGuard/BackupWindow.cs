using DGD.HubCore.Arch;
using easyLib.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovDataGuard
{
    public partial class BackupWindow: Form
    {
        readonly string m_srcFolder;
        readonly string m_filePath;


        public BackupWindow(string filePath, string srcFolder)
        {
            InitializeComponent();

            m_srcFolder = srcFolder;
            m_filePath = filePath;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Action<Task> onErr = t =>
            {
                File.Delete(m_filePath);
                Close();
            };


            var task = new Task(CreateBackup , TaskCreationOptions.LongRunning);
            task.OnError(onErr);

            task.Start();
            
        }


        //private:
        void SetMessage(string txt)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string>(SetMessage) , txt);
            else
                m_lblMessage.Text = txt;
        }

        void CreateBackup()
        {
            var bkupEngin = new ArchiveEngin();

            bkupEngin.Compressing += BkupEngin_Compressing;
            bkupEngin.Initializing += BkupEngin_Initializing;
            bkupEngin.Done += BkupEngin_Done;

            byte[] header = File.ReadAllBytes(m_filePath);

            bkupEngin.Backup(m_filePath , m_srcFolder , header);
        }

        private void BkupEngin_Done()
        {
            if (InvokeRequired)
                BeginInvoke(new Action(Close));
            else
                Close();
        }


        //handlers:
        private void BkupEngin_Initializing() => SetMessage("Initialisation...");
        private void BkupEngin_Compressing() => SetMessage("Compression...");
    }
}
