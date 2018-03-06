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

        //private:
        static void PopulateList(List<string> files , string path, Jobs.ProcessingDialog dlg)
        {
            dlg.Message = $"Traitement du dossier {path}...";
            files.AddRange(Directory.EnumerateFiles(path));

            foreach (string dir in Directory.EnumerateDirectories(path))
                PopulateList(files , dir, dlg);
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
                        PopulateList(files , RootFolder, busyDlg);

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



    }
}
