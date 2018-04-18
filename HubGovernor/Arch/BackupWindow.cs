using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Arch
{
    public partial class BackupWindow: Form
    {
        readonly BackupPage m_bkupPage;
        readonly RestorePage m_restorePage;


        public BackupWindow()
        {
            InitializeComponent();

            m_restorePage = new RestorePage();
            m_restorePage.Dock = DockStyle.Fill;

            m_bkupPage = new BackupPage();
            m_bkupPage.Dock = DockStyle.Fill;


            m_pagesPanel.Controls.Add(m_bkupPage);
        }


        //protected:


        //private:
        Control CurrentPage => m_pagesPanel.Controls[0];

        //handlerq:
        private void Backup_LinkClicked(object sender , LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentPage != m_bkupPage)
            {
                CurrentPage.Hide();
                m_pagesPanel.Controls.Remove(CurrentPage);                
                m_pagesPanel.Controls.Add(m_bkupPage);
                CurrentPage.Show();
            }
        }

        private void Restore_LinkClicked(object sender , LinkLabelLinkClickedEventArgs e)
        {
            if(CurrentPage != m_restorePage)
            {
                CurrentPage.Hide();
                m_pagesPanel.Controls.Remove(CurrentPage);
                m_pagesPanel.Controls.Add(m_restorePage);
                CurrentPage.Show();
            }
        }
    }
}
