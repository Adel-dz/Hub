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

namespace DGD.HubGovernor.Arch
{
    partial class BackupPage: UserControl
    {
        public BackupPage()
        {
            InitializeComponent();
        }


        //private:
        void CreateArchiveHeader(string archFile)
        {
            File.WriteAllBytes(archFile, BitConverter.GetBytes(AppContext.Settings.AppSettings.DataGeneration));
        }

        //handlers:
        private void Start_Click(object sender , EventArgs e)
        {
            AppContext.LogManager.LogUserActivity("Action utilisateur: Démarrage manuel de la sauvegarde");
            string archFile = $"Archive {DateTime.Now.Ticks}.gss";
            string archPath = Path.Combine(AppContext.Settings.UserSettings.BackupFolder , archFile);

            CreateArchiveHeader(archPath);

            string args = $"0 \"{archPath}\" \"{AppPaths.AppDataFolder}\"";
            System.Diagnostics.Process.Start(Path.Combine(@".\" , "GovDataGuard.exe") , args);

            Application.Exit();
        }
    }
}
