using DGD.HubCore.Updating;
using easyLib.Extensions;
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

namespace DGD.HubGovernor.Updating
{
    sealed partial class AppUpdateDialog: Form
    {
        public AppUpdateDialog()
        {
            InitializeComponent();

            m_cmbSystem.SelectedIndex = 0;
        }


        public string RootFolder { get; set; }

        public IEnumerable<string> Files
        {
            get
            {
                foreach (ListViewItem lvi in m_lvFiles.Items)
                    yield return lvi.Tag as string;
            }
        }

        public Version Version
        {
            get { return new Version(m_tbVersion.Text); }
            set { m_tbVersion.Text = value.ToString(); }
        }

        public AppArchitecture_t AppArchitecture => AppArchitecture_t.Win7SP1;

        //private:
        void UpdateUI()
        {
            m_btnDelete.Enabled = m_lvFiles.SelectedIndices.Count > 0;

            Version ver;
            m_btnOK.Enabled = m_lvFiles.Items.Count > 0 && m_tbVersion.Text.Length > 0 &&
                Version.TryParse(m_tbVersion.Text , out ver);
        }

        static void PopulateList(List<string> files , string path , Jobs.ProcessingDialog dlg)
        {
            dlg.Message = $"Traitement du dossier {path}...";
            files.AddRange(Directory.EnumerateFiles(path));

            foreach (string dir in Directory.EnumerateDirectories(path))
                PopulateList(files , dir , dlg);
        }

        //handlers:
        private void Insert_Click(object sender , EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Sélectionnez le dossier racine.";


                if (RootFolder != null)
                    dlg.SelectedPath = RootFolder;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    RootFolder = dlg.SelectedPath;

                    var files = new List<string>();

                    var busyDlg = new Jobs.ProcessingDialog();

                    Func<ListViewItem[]> enumFiles = () =>
                    {
                        PopulateList(files , RootFolder , busyDlg);

                        busyDlg.Message = "Finalisation...";

                        files.Sort();
                        IEnumerable<string> seq = files.Distinct();

                        var items = from fl in seq
                                    let fName = Path.GetFileName(fl)
                                    let relDir = Path.GetDirectoryName(fl.Remove(0 , RootFolder.Length + 1))
                                    select new ListViewItem(new string[] { fName , relDir })
                                    {
                                        Tag = fl
                                    };

                        return items.ToArray();
                    };



                    Action<Task> onErr = t =>
                    {
                        busyDlg.Dispose();

                        MessageBox.Show(t.Exception.InnerException.Message ,
                            null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    };

                    Action<Task<ListViewItem[]>> onSuccess = t =>
                    {
                        m_lvFiles.Items.Clear();
                        m_lvFiles.Items.AddRange(t.Result);

                        busyDlg.Dispose();
                    };


                    var task = new Task<ListViewItem[]>(enumFiles , TaskCreationOptions.LongRunning);
                    task.OnSuccess(onSuccess);
                    task.OnError(onErr);
                    task.Start();

                    busyDlg.ShowDialog(this);
                }
            }
        }

        private void Delete_Click(object sender , EventArgs e)
        {
            var selItems = new ListViewItem[m_lvFiles.SelectedItems.Count];
            m_lvFiles.SelectedItems.CopyTo(selItems , 0);

            for (int i = 0; i < selItems.Length; ++i)
                m_lvFiles.Items.Remove(selItems[i]);
        }

        private void Files_SelectedIndexChanged(object sender , EventArgs e) => UpdateUI();

        private void Version_TextChanged(object sender , EventArgs e) => UpdateUI();
    }
}
