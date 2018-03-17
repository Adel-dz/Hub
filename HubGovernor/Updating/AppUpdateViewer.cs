using DGD.HubCore;
using DGD.HubCore.Updating;
using DGD.HubGovernor.Extensions;
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
    sealed partial class AppUpdateViewer: Form
    {
        IAppUpdate m_update;

        public AppUpdateViewer(IAppUpdate update)
        {
            InitializeComponent();

            m_update = update;
        }


        //protecetd:
        protected override void OnLoad(EventArgs e)
        {
            LoadDataAsync();
            base.OnLoad(e);
        }

        //private:
        ListViewItem CreateLVI(string key , string value = null)
        {
            if (value == null)
                return new ListViewItem(key);

            return new ListViewItem(new string[] { key , value });
        }

        void LoadDataAsync()
        {
            Func<ListViewItem[]> load = () =>
            {
                var items = new List<ListViewItem>();

                items.Add(CreateLVI("ID" , m_update.ID.ToString("X")));
                items.Add(CreateLVI("Crée le" , m_update.CreationTime.ToString()));
                items.Add(CreateLVI("Version" , m_update.Version.ToString()));
                items.Add(CreateLVI("Architecture" , AppArchitectures.GetArchitectureName(m_update.AppArchitecture)));
                items.Add(CreateLVI("" ));
                items.Add(CreateLVI("Fichiers:"));

                string fileName = m_update.ID.ToString("X");
                string filePath = Path.Combine(AppPaths.AppUpdateFolder , fileName);

                foreach (string item in FilesBag.GetContent(filePath))
                    items.Add(CreateLVI(item));

                return items.ToArray();
            };

            var waitDlg = new Waits.WaitClue(this);

            Action<Task<ListViewItem[]>> onSuccess = t =>
            {
                m_lvData.Items.AddRange(t.Result);
                m_lvData.AdjustColumnsSize();
                waitDlg.LeaveWaitMode();
            };

            Action<Task> onErr = t =>
            {
                waitDlg.LeaveWaitMode();
                MessageBox.Show(t.Exception.InnerException.Message , null , 
                    MessageBoxButtons.OK , MessageBoxIcon.Error);
            };

            var task = new Task<ListViewItem[]>(load , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);
            task.Start();
            waitDlg.EnterWaitMode();
        }

        //handlers:
        private void Extract_Click(object sender , EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                string fileName = m_update.ID.ToString("X");
                string filePath = Path.Combine(AppPaths.AppUpdateFolder , fileName);

                FilesBag.Decompress(filePath , dlg.SelectedPath);
            }
        }
    }
}
