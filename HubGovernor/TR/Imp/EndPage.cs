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

namespace DGD.HubGovernor.TR.Imp
{
    sealed partial class EndPage: UserControl
    {
        public EndPage()
        {
            InitializeComponent();
        }


        public string[] BadRows 
        {
            get { return m_tbBadRows.Lines; }
            set
            {
                m_tbBadRows.Lines = value;

                m_btnSave.Enabled = m_tbBadRows.Text != "";
            }
        }


        //handlers
        private void Save_Click(object sender , EventArgs e)
        {
            SaveFileDialog sfDlg = null;

            try
            {
                sfDlg = new SaveFileDialog();
                sfDlg.Filter = "Fichiers texte|*.txt|Tous les fichiers|*.*";

                if (sfDlg.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(sfDlg.FileName , m_tbBadRows.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            finally
            {
                sfDlg?.Dispose();
            }
        }
    }
}
