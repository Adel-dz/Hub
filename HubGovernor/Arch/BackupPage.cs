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

namespace DGD.HubGovernor.Arch
{
    partial class BackupPage: UserControl
    {
        public BackupPage()
        {
            InitializeComponent();
        }


        //private:

        //handlers:
        private void Start_Click(object sender , EventArgs e)
        {
            AppContext.LogManager.LogUserActivity("Action utilisateur: Démarrage manuel de la sauvegarde");
            string args = $"{AppContext.Settings.UserSettings.BackupFolder} {AppContext.Settings.AppSettings.DataGeneration}";
            System.Diagnostics.Process.Start(Path.Combine(@".\" , "GovDataGuard.exe") , args);

            Application.Exit();
        }
    }
}
