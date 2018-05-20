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
using easyLib;
using easyLib.DB;
using DGD.HubCore.Arch;

namespace DGD.HubGovernor.Arch
{
    public partial class RestorePage: UserControl
    {
        string m_filePath;
        IArchiveContent m_archData;

        public RestorePage()
        {
            InitializeComponent();
        }


        //private:
        void UpdateUI()
        {
            if (m_filePath != null)
                try
                {
                    var archEngin = new ArchiveEngin();
                    m_archData = archEngin.GetArchiveContent(m_filePath);
                    uint ver = BitConverter.ToUInt32(m_archData.ArchiveHeader , 0);
                    SetArchiveInfo(m_archData.CreationTime , ver);
                    m_tsbPreview.Enabled = m_btnStart.Enabled = true;
                }
                catch (Exception ex)
                {
                    m_lblArchiveInfo.ForeColor = Color.Red;
                    m_lblArchiveInfo.Text = ex.Message;
                    m_tsbPreview.Enabled = m_btnStart.Enabled = false;
                    m_archData = null;
                }
        }

        void SetArchiveInfo(DateTime dt , uint ver)
        {
            m_lblArchiveInfo.ForeColor = SystemColors.WindowText;
            string txt = $"Archive créee le: {dt.ToShortDateString()} à {dt.ToShortTimeString()}. \n" +
                $"Version des données: {ver}.";

            m_lblArchiveInfo.Text = txt;
        }

        private void Start_Click(object sender , EventArgs e)
        {
            const string msg = "Le Governor va maintenant être fermé pour permettre au programme d’archivage de restaurer vous données.\n" +
                "Voulez-vous poursuivre ?";

            if (MessageBox.Show(msg , "Restauration" , MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppContext.LogManager.LogUserActivity($"Action utilisateur: Démarrage manuel de la restauration avec l'archive {m_filePath}");
                string args = $"1 \"{m_filePath}\" \"{AppPaths.AppDataFolder}\"";
                System.Diagnostics.Process.Start(Path.Combine(@".\" , "GovDataGuard.exe") , args);

                Application.Exit();
            }
        }

        private void Open_Click(object sender , EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Fichiers Governor Snapshot|*.gss|Tous les fichers|*.*";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    m_filePath = dlg.FileName;
                    m_lblSource.Text = "Archive:\n" + m_filePath;
                    UpdateUI();
                }
            }
        }

        private void Preview_Click(object sender , EventArgs e)
        {
            var viewer = new ArchiveViewer(m_archData);
            viewer.Text = m_filePath;
            viewer.Show(Parent);
        }


    }
}
