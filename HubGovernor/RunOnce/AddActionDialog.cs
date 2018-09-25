using DGD.HubCore.RunOnce;
using DGD.HubGovernor.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.RunOnce
{
    partial class AddActionDialog: Form
    {
        public class ClientAction
        {
            public ClientAction(uint idClient , RunOnceAction_t action)
            {
                ClientID = idClient;
                Action = action;
            }


            public uint ClientID { get; }
            public RunOnceAction_t Action { get; }
        }



        readonly Dictionary<string , RunOnceAction_t> m_actions = new Dictionary<string , RunOnceAction_t>
        {
            {"Réinitialiser les mises à jours", RunOnceAction_t.ResetUpdateInfo },
        };


        public AddActionDialog()
        {
            InitializeComponent();
        }


        public IEnumerable<ClientAction> UserSelection
        {
            get
            {
                RunOnceAction_t[] actions = (from string s in m_lbActions.CheckedItems
                                           select m_actions[s]).ToArray();

                var result = new List<ClientAction>();

                foreach (uint id in m_lbClients.CheckedItems)
                    foreach (RunOnceAction_t action in actions)
                        result.Add(new ClientAction(id , action));

                return result;
            }
        }


        
        //protected:
        protected override void OnLoad(EventArgs e)
        {
            LoadClients();
            LoadActions();

            base.OnLoad(e);
        }


        //private:
        void LoadClients()
        {
            var ids = from ClientStatus cls in AppContext.AccessPath.GetDataProvider(InternalTablesID.CLIENT_STATUS).Enumerate()
                      where cls.Status != HubCore.DLG.ClientStatus_t.Reseted
                      select (object)cls.ClientID;

            m_lbClients.Items.AddRange(ids.ToArray());
        }

        void LoadActions()
        {
            m_lbActions.Items.AddRange(m_actions.Keys.ToArray());
        }


        //handlers:
        private void Actions_ItemCheck(object sender , ItemCheckEventArgs e)
        {
            bool enableAdd = (e.NewValue == CheckState.Checked || m_lbActions.CheckedIndices.Count > 1) &&
                m_lbClients.CheckedIndices.Count > 0;

            m_btnAdd.Enabled = enableAdd;
        }

        private void Clients_ItemCheck(object sender , ItemCheckEventArgs e)
        {
            bool enableAdd = (e.NewValue == CheckState.Checked || m_lbClients.CheckedIndices.Count > 1) && 
                m_lbActions.CheckedIndices.Count > 0;

            m_btnAdd.Enabled = enableAdd;
        }

        private void ToggleSelection_Click(object sender , EventArgs e)
        {
            for (int i = 0; i < m_lbClients.Items.Count; ++i)
                m_lbClients.SetItemChecked(i , !m_lbClients.GetItemChecked(i));
        }
    }
}
