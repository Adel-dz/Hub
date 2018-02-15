using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DGD.HubGovernor.Admin
{
    sealed partial class IntegrityCheckerDialog: Form
    {
        public IntegrityCheckerDialog()
        {
            InitializeComponent();

            foreach (IDataTable table in AppContext.TableManager.Tables)
                m_lbTables.Items.Add(table);
        }


        public IEnumerable<IDataTable> SelectedTables => m_lbTables.CheckedItems.Cast<IDataTable>();


        //private:

        //handlrs
        private void CheckAll_Click(object sender , EventArgs e)
        {
            for (int i = 0; i < m_lbTables.Items.Count; ++i)
                m_lbTables.SetItemCheckState(i , CheckState.Checked);
        }

        private void ToggleCheck_Click(object sender , EventArgs e)
        {
            for (int i = 0; i < m_lbTables.Items.Count; ++i)
                m_lbTables.SetItemChecked(i , !m_lbTables.GetItemChecked(i));
        }
    }
}
