using DGD.HubGovernor.Admin;
using DGD.HubGovernor.Log;
using DGD.HubGovernor.TR;
using DGD.HubGovernor.Updating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor
{
    sealed partial class MainWindow: Form
    {
        const string OPT_KEY = "MAIN";
        readonly TextLogWindow m_logWindow;

        public MainWindow()
        {
            InitializeComponent();

            m_logWindow = new TextLogWindow();

            m_logWindow.VisibleChanged += logWindow_VisibleChanged;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            Opts.WindowPlacement wp = AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY];

            if (wp != null)
                Location = new Point(wp.Left , wp.Top);
            else
            {
                int ScreenWidth = Screen.GetWorkingArea(this).Width;
                int x = (ScreenWidth - Width) >> 1;

                Left = x > 0 ? x : 0;
                Top = 0;
            }

            if (!AppContext.Settings.UserSettings.LogWindowHidden)
                m_logWindow.Show(this);

            TextLogger.Info($"Hub Governor version: {Assembly.GetExecutingAssembly().GetName().Version}");
            TextLogger.Info($"Version des données: {AppContext.Settings.AppSettings.DataGeneration}");

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Point xy = Location;
            AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY] = new Opts.WindowPlacement(xy.X , xy.Y,0,0);
        }

        //handlers
        private void Repository_Click(object sender , EventArgs e)
        {
            new RepositoryWindow().Show(this);
        }

        private void TR_Click(object sender , EventArgs e)
        {
            var view = new TRSpotViewer();
            view.Show(this);
        }

        private void CheckIntegrity_Click(object sender , EventArgs e)
        {
            using (var dlg = new IntegrityCheckerDialog())
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (var verifier = new IntegrityVerifier(dlg.SelectedTables))
                        verifier.Run();
                }
            }
        }

        private void Update_Click(object sender , EventArgs e)
        {
            var wind = new UpdatesWindow();
            wind.Show(this);
        }

        private void Settings_Click(object sender , EventArgs e)
        {
            using (var dlg = new Opts.SettingsWizard())
            {
                dlg.ShowDialog(this);
            }
        }

        private void ClientWindow_Click(object sender , EventArgs e)
        {
            var wind = new Clients.ClientsManagmentWindow();
            wind.Show(this);
        }

        private void SysLog_Click(object sender , EventArgs e) => new SysLogWindow().Show(this);

        private void LogView_Click(object sender , EventArgs e)
        {
            m_logWindow.Visible = !m_tsbLogView.Checked;
        }

        private void logWindow_VisibleChanged(object sender , EventArgs e)
        {
            m_tsbLogView.Checked = m_logWindow.Visible;            
        }

        private void Backup_Click(object sender , EventArgs e)
        {
            new Arch.BackupWindow().Show(this);
        }

        private void RunOnce_Click(object sender , EventArgs e)
        {
            new RunOnce.RunOnceWindow().Show(this);
        }
    }
}
