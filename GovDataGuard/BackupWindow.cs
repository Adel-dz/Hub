using System;
using System.IO;
using System.Windows.Forms;

namespace GovDataGuard
{
    public partial class BackupWindow: Form
    {
        readonly string m_destFolder;
        readonly BackupFirstStagePage m_firstPage;
        BackupLastStagePage m_lastPage;
        readonly uint m_dataVersion;


        public BackupWindow(string[] args)
        {
            InitializeComponent();

            m_destFolder = args[0];
            m_dataVersion = uint.Parse(args[1]);

            m_firstPage = new BackupFirstStagePage();
            m_firstPage.EndProcessing += RunLastStage;

            Controls.Add(m_firstPage);
            m_firstPage.Dock = DockStyle.Fill;                        
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            File.Delete(AppPaths.LogFilePath);

            try
            {
                m_firstPage.Start();
            }
            catch(Exception ex)
            {
                File.WriteAllText(AppPaths.LogFilePath , ex.Message);
                System.Diagnostics.Process.Start(Path.Combine(@".\" , "HubGovernor.exe"));
                Application.Exit();
            }
        }

        //private:
        void RunLastStage()
        {
            if (InvokeRequired)
                BeginInvoke(new Action(RunLastStage));
            else
            {

                m_lastPage = new BackupLastStagePage();
                m_lastPage.DataSourceFile = m_firstPage.DataBackupFile;
                m_lastPage.UpdatesSourceFile = m_firstPage.UpdatesBackupFile;
                m_lastPage.LogsSourceFile = m_firstPage.LogsBackupFile;
                m_lastPage.SysSourceFile = m_firstPage.SysBackupFile;
                m_lastPage.DataVersion = m_dataVersion;
                m_lastPage.DestFolder = m_destFolder;
                                
                Controls.Clear();
                Controls.Add(m_lastPage);
                m_lastPage.Dock = DockStyle.Fill;
                m_lastPage.Start();
            }
        }      
    }
}
