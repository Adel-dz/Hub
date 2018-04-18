using DGD.HubCore.Arch;
using DGD.HubCore.Updating;
using easyLib;
using easyLib.DB;
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

namespace GovDataGuard
{
    public partial class RestoreWindow: Form
    {
        readonly string m_filePath;
        readonly string m_destFolder;

        public RestoreWindow(string filePath , string destFolder)
        {
            InitializeComponent();

            m_filePath = filePath;
            m_destFolder = destFolder;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Action<Task> onErr = t => Close();

            var task = new Task(Restore , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.Start();
        }

        //private:
        void SetMessage(string txt)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string>(SetMessage) , txt);
            else
                m_lbMessage.Text = txt;
        }

        void Restore()
        {
            var archEngin = new ArchiveEngin();
            archEngin.Initializing += ArchEngin_Initializing;
            archEngin.Restoring += ArchEngin_Restoring;
            archEngin.Done += ArchEngin_Done;

            archEngin.Restore(m_filePath , m_destFolder);
        }

        //handlers:
        private void ArchEngin_Done()
        {
            if (InvokeRequired)
                BeginInvoke(new Action(Close));
            else
                Close();
        }

        private void ArchEngin_Restoring() => SetMessage("Restauration...");
        private void ArchEngin_Initializing() => SetMessage("Initialisation...");
    }
}
