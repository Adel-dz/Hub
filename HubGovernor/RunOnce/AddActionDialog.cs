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
        readonly Dictionary<string , RunOnceAction_t> m_actions = new Dictionary<string,RunOnceAction_t>
        {
            {"Réinitialiser les mises à jours", RunOnceAction_t.ResetUpdateInfo },
        };


        public class ClientAction
        {
            public uint ClientID { get; }
            public RunOnceAction_t Action { get; }
        }


        public AddActionDialog()
        {
            InitializeComponent();
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
                      
    }
}
