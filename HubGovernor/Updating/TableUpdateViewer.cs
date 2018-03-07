using DGD.HubCore.Updating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Updating
{
    sealed partial class TableUpdateViewer: Form
    {
        class ListBoxData
        {
            public ListBoxData(TableUpdate update)
            {
                Update = update;
            }


            public TableUpdate Update { get; }

            public override string ToString() => AppContext.TableManager.GetTable(Update.TableID).Name;
        }


        static Dictionary<ActionCode_t , string> m_actionsName;


        static TableUpdateViewer()
        {
            m_actionsName = new Dictionary<ActionCode_t , string>
            {
                {ActionCode_t.AddRow,"Ajouter une ligne" },
                {ActionCode_t.DeleteRow, "Supprimer une ligne" },
                {ActionCode_t.ReplaceRow, "Remplacer une ligne" },
                {ActionCode_t.ResetTable, "Effacer la table" }
            };
        }

        public TableUpdateViewer(uint updateID)
        {
            InitializeComponent();

            string filePath = System.IO.Path.Combine(AppPaths.DataUpdateFolder , updateID.ToString("X"));
            IEnumerable<TableUpdate> updates = UpdateEngin.LoadTablesUpdate(filePath,AppContext.DatumFactory);
            m_lbTables.Items.AddRange(updates.Select(update => new ListBoxData(update)).ToArray());

            if (m_lbTables.Items.Count > 0)
                m_lbTables.SelectedIndex = 0;
        }


        //private:
        ListViewItem CreateItem(IUpdateAction action)
        {
            switch (action.Code)
            {
                case ActionCode_t.ResetTable:
                return new ListViewItem(m_actionsName[ActionCode_t.ResetTable]);
                
                case ActionCode_t.DeleteRow:
                {
                    var delRow = action as DeleteRow;
                    return new ListViewItem(new[] { m_actionsName[ActionCode_t.DeleteRow] , delRow.RowID.ToString() });
                }

                case ActionCode_t.ReplaceRow:
                {
                    var repRow = action as ReplaceRow;
                    return new ListViewItem(new[] { m_actionsName[ActionCode_t.ReplaceRow] , repRow.RowID.ToString() });
                }

                case ActionCode_t.AddRow:
                {
                    var addRow = action as AddRow;
                    return new ListViewItem(new[] { m_actionsName[ActionCode_t.AddRow] , addRow.Datum.ID.ToString() });
                }

                default:
                Assert(false);
                break;
            }

            return null;
        }

        //handlers:

        private void Tables_SelectedIndexChanged(object sender , EventArgs e)
        {
            int ndx = m_lbTables.SelectedIndex;

            m_lvActions.Items.Clear();

            if (ndx < 0)
                m_lblPostGenerationValue.Text = m_lblPreGenerationValue.Text = "";
            else
            {
                TableUpdate update = (m_lbTables.Items[ndx] as ListBoxData).Update;

                m_lblPostGenerationValue.Text = update.PostGeneration.ToString();
                m_lblPreGenerationValue.Text = update.PreGeneration.ToString();

                m_lvActions.Items.AddRange(update.Actions.Select(act => CreateItem(act)).ToArray());
            }
        }
    }
}
