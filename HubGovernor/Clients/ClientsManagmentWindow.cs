using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubGovernor.Profiles;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Clients
{
    sealed partial class ClientsManagmentWindow: Form
    {
        const int NDXIMG_PROFILE = 0;
        const int NDXIMG_CLIENT = 1;

        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerStatus;
        readonly KeyIndexer m_ndxerClients;
        readonly KeyIndexer m_ndxerClientEnv;


        public ClientsManagmentWindow()
        {
            InitializeComponent();

            m_ndxerProfiles = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.USER_PROFILE);
            m_ndxerStatus = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_STATUS);
            m_ndxerClientEnv = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_ENV);
            m_ndxerClients = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.HUB_CLIENT);
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            LoadRunningClientsAsync();
            RegisterHandlers();

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            UnregisterHandlers();
        }

        //private:
        TreeNode LocateClientNode(uint clID)
        {
            foreach (TreeNode root in m_tvClients.Nodes)
                foreach (TreeNode node in root.Nodes)
                    if ((node.Tag as HubClient).ID == clID)
                        return node;

            return null;
        }

        TreeNode CreateClientNode(HubClient client)
        {
            var node = new TreeNode(client.ID.ToString("X"))
            {
                Tag = client ,
                SelectedImageIndex = NDXIMG_CLIENT ,
                ImageIndex = NDXIMG_CLIENT
            };

            if (AppContext.ClientsManager.IsClientRunning(client.ID))
                node.ForeColor = Color.SteelBlue;

            return node;
        }

        void LoadRunningClientsAsync()
        {
            Dbg.Assert(!InvokeRequired);

            Func<TreeNode[]> load = () =>
            {
                var roots = new List<TreeNode>();

                foreach (HubClient hc in AppContext.ClientsManager.RunningClients)
                {
                    var profile = m_ndxerProfiles.Get(hc.ProfileID) as UserProfile;

                    var root = new TreeNode(profile.Name)
                    {
                        Tag = profile ,
                        SelectedImageIndex = NDXIMG_PROFILE ,
                        ImageIndex = NDXIMG_PROFILE
                    };

                    root.Nodes.Add(CreateClientNode(hc));

                    roots.Add(root);
                }

                return roots.ToArray();
            };


            var waitClue = new Waits.WaitClue(this);

            Action<Task> onErr = t =>
            {
                MessageBox.Show(t.Exception.InnerException.Message , Text);
                EventLogger.Error(t.Exception.InnerException.Message);
                waitClue.LeaveWaitMode();
            };

            Action<Task<TreeNode[]>> onSucces = t =>
            {
                m_tvClients.Nodes.Clear();

                m_tvClients.Nodes.AddRange(t.Result);
                m_tvClients.ExpandAll();

                waitClue.LeaveWaitMode();
            };


            var task = new Task<TreeNode[]>(load , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSucces);
            task.OnError(onErr);

            waitClue.EnterWaitMode();
            task.Start();

        }

        void LoadAllClientsAsync()
        {
            Dbg.Assert(!InvokeRequired);


            Func<IEnumerable<UserProfile>> loadProfiles = () =>
            {
                IEnumerable<UserProfile> profiles = from UserProfile prf in m_ndxerProfiles.Source.Enumerate()
                                                    select prf;
                return profiles;
            };


            Func<IEnumerable<UserProfile> , TreeNode[]> loadClients = (IEnumerable<UserProfile> profiles) =>
            {
                var roots = new List<TreeNode>();

                foreach (UserProfile prf in profiles)
                {
                    var clients = from HubClient client in m_ndxerClients.Source.Enumerate()
                                  where client.ProfileID == prf.ID
                                  select CreateClientNode(client);

                    var root = new TreeNode(prf.Name , clients.ToArray())
                    {
                        Tag = prf ,
                        SelectedImageIndex = NDXIMG_PROFILE ,
                        ImageIndex = NDXIMG_PROFILE
                    };

                    roots.Add(root);
                }

                return roots.ToArray();
            };

            Func<TreeNode[]> load = () =>
            {
                IEnumerable<UserProfile> profiles = loadProfiles();
                return loadClients(profiles);
            };


            var waitClue = new Waits.WaitClue(this);

            Action<Task> onErr = t =>
            {
                MessageBox.Show(t.Exception.InnerException.Message , Text);
                EventLogger.Error(t.Exception.InnerException.Message);
                waitClue.LeaveWaitMode();
            };

            Action<Task<TreeNode[]>> onSucces = t =>
            {
                m_tvClients.Nodes.Clear();

                m_tvClients.Nodes.AddRange(t.Result);
                m_tvClients.ExpandAll();

                waitClue.LeaveWaitMode();
            };


            var task = new Task<TreeNode[]>(load , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSucces);
            task.OnError(onErr);

            waitClue.EnterWaitMode();
            task.Start();

        }

        void UnregisterHandlers()
        {
            AppContext.ClientsManager.ClientClosed -= ClientsManager_ClientClosed;
            AppContext.ClientsManager.ClientStarted -= ClientsManager_ClientStarted;
            m_ndxerStatus.DatumReplaced -= ClientStatus_DatumReplaced;
            m_ndxerClients.DatumReplaced -= Clients_DatumReplaced;
        }

        void RegisterHandlers()
        {
            AppContext.ClientsManager.ClientClosed += ClientsManager_ClientClosed;
            AppContext.ClientsManager.ClientStarted += ClientsManager_ClientStarted;
            m_ndxerStatus.DatumReplaced += ClientStatus_DatumReplaced;
            m_ndxerClients.DatumReplaced += Clients_DatumReplaced;
        }

        void ProcessClientStatus(ClientStatus_t status)
        {
            Dbg.Assert(!InvokeRequired);

            var client = m_tvClients.SelectedNode.Tag as HubClient;
            ManagementMode_t prfMgmnt = AppContext.ClientsManager.GetProfileManagementMode(client.ProfileID);
            var prf = m_ndxerProfiles.Get(client.ProfileID) as UserProfile;

            if (prfMgmnt == ManagementMode_t.Auto)
            {
                if (MessageBox.Show(this ,
                        $"La gestion du profil {prf.Name} sera changée en mode 'manuel'. Poursuivre ?" ,
                        Text , MessageBoxButtons.YesNo ,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            //maj le status
            AppContext.ClientsManager.SetClientStatus(client , status);
        }

        static string GetComprehensiveTime(DateTime dt)
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = now.Subtract(dt);

            if (ts.Days > 0)
                return dt.ToString();

            if (ts.Hours > 0)
                return $"{ts.Hours} heure(s) et {ts.Minutes} Minute(s)";

            if (ts.Minutes > 0)
                return $"{ts.Minutes} Minute(s)";

            return "Quelques secondes";

        }

        void ClearClientInfo()
        {
            if (InvokeRequired)
                Invoke(new Action(ClearClientInfo));
            else
                m_lblContact.Text = m_lblCreationTime.Text = m_lblEMail.Text =
                    m_lblHubArchitecture.Text = m_lblHubVersion.Text =
                    m_lblLastActivity.Text = m_lblMachineName.Text = m_lblOSArchitecture.Text =
                    m_lblOSVersion.Text = m_lblPhone.Text = m_lblStatus.Text = m_lblUserName.Text = "-";

        }

        void SetClientInfo(HubClient hubClient)
        {
            if (InvokeRequired)
                Invoke(new Action<HubClient>(SetClientInfo) , hubClient);
            else
            {
                var seq = from HubClientEnvironment hce in m_ndxerClientEnv.Source.Enumerate()
                          where hce.ClientID == hubClient.ID
                          orderby hce.CreationTime descending
                          select hce;


                HubClientEnvironment clEnv = seq.First();

                var clStatus = m_ndxerStatus.Get(hubClient.ID) as ClientStatus;

                m_lblContact.Text = hubClient.ContactName;
                m_lblCreationTime.Text = hubClient.CreationTime.ToString();
                m_lblEMail.Text = hubClient.ContaclEMail;
                m_lblHubArchitecture.Text = AppArchitectures.GetArchitectureName(clEnv.HubArchitecture);
                m_lblHubVersion.Text = clEnv.HubVersion;
                m_lblLastActivity.Text = GetComprehensiveTime(clStatus.LastSeen);
                m_lblMachineName.Text = clEnv.MachineName;
                m_lblOSArchitecture.Text = clEnv.Is64BitOperatingSystem ? "64 Bits" : "32 Bits";
                m_lblOSVersion.Text = clEnv.OSVersion;
                m_lblPhone.Text = hubClient.ContactPhone;
                m_lblStatus.Text = ClientStatuses.GetStatusName(clStatus.Status);
                m_lblUserName.Text = clEnv.UserName;
            }
        }

        void UpdateStatusButtons(ClientStatus_t status)
        {
            Dbg.Assert(!InvokeRequired);


            m_tsbBanishClient.Checked = m_tsbDisableClient.Checked = m_tsbEnableClient.Checked = false;

            m_tsbReset.Enabled = m_tsbBanishClient.Enabled = m_tsbDisableClient.Enabled =
                m_tsbEnableClient.Enabled = status != ClientStatus_t.Unknown;

            if (status != ClientStatus_t.Unknown)
                switch (status)
                {
                    case ClientStatus_t.Enabled:
                    m_tsbEnableClient.Checked = true;
                    break;

                    case ClientStatus_t.Disabled:
                    m_tsbDisableClient.Checked = true;
                    break;

                    case ClientStatus_t.Banned:
                    m_tsbBanishClient.Checked = true;
                    break;
                }
        }


        ////handlers
        private void Profiles_Click(object sender , EventArgs e)
        {
            var wind = new Profiles.ProfilesWindow();
            wind.Show(Owner);
        }

        private void Clients_AfterSelect(object sender , TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                ClearClientInfo();
                UpdateStatusButtons(ClientStatus_t.Unknown);

            }
            else
            {
                var client = e.Node.Tag as HubClient;
                SetClientInfo(client);

                var clStatus = m_ndxerStatus.Get(client.ID) as ClientStatus;

                UpdateStatusButtons(clStatus.Status);
            }


        }

        private void RunningClientsOnly_Click(object sender , EventArgs e)
        {
            bool showAll = m_tsbRunningClientsOnly.Checked;

            if (showAll)
                LoadAllClientsAsync();
            else
                LoadRunningClientsAsync();

            m_tvClients.SelectedNode = null;
            ClearClientInfo();
            UpdateStatusButtons(ClientStatus_t.Unknown);


            m_tsbRunningClientsOnly.Checked = !showAll;
        }

        private void EnableClient_Click(object sender , EventArgs e)
        {
            if (!(sender as ToolStripButton).Checked)
                ProcessClientStatus(ClientStatus_t.Enabled);
        }

        private void DisableClient_Click(object sender , EventArgs e)
        {
            if (!(sender as ToolStripButton).Checked)
                ProcessClientStatus(ClientStatus_t.Disabled);
        }

        private void BanishClient_Click(object sender , EventArgs e)
        {
            if (!(sender as ToolStripButton).Checked)
                ProcessClientStatus(ClientStatus_t.Banned);
        }

        private void ClientsManager_ClientStarted(uint clID)
        {
            if (InvokeRequired)
                Invoke(new Action<uint>(ClientsManager_ClientStarted) , clID);
            else
            {
                TreeNode selNode = m_tvClients.SelectedNode;
                TreeNode clNode = LocateClientNode(clID);

                if (clNode != null)
                    clNode.ForeColor = Color.SteelBlue;
                else
                {
                    var client = m_ndxerClients.Get(clID) as HubClient;

                    if (client != null)
                    {
                        TreeNode root = null;

                        foreach (TreeNode node in m_tvClients.Nodes)
                            if ((node.Tag as UserProfile).ID == client.ProfileID)
                            {
                                root = node;
                                break;
                            }

                        if (root == null)
                        {
                            var profile = m_ndxerProfiles.Get(client.ProfileID) as UserProfile;

                            root = new TreeNode(profile.Name)
                            {
                                Tag = profile ,
                                SelectedImageIndex = NDXIMG_PROFILE ,
                                ImageIndex = NDXIMG_PROFILE
                            };

                            m_tvClients.Nodes.Add(root);
                        }


                        root.Nodes.Add(CreateClientNode(client));
                        root.Expand();

                        if (selNode != null)
                            m_tvClients.SelectedNode = selNode;
                    }
                }
            }
        }

        private void ClientsManager_ClientClosed(uint clID)
        {
            if (InvokeRequired)
                Invoke(new Action<uint>(ClientsManager_ClientClosed) , clID);
            else
            {
                TreeNode clNode = LocateClientNode(clID);

                if (clNode != null)
                {
                    TreeNode selNode = m_tvClients.SelectedNode;


                    if (m_tsbRunningClientsOnly.Checked)
                    {
                        TreeNode root = clNode.Parent;
                        root.Nodes.Remove(clNode);

                        if (root.Nodes.Count == 0)
                            m_tvClients.Nodes.Remove(root);
                    }
                    else
                        clNode.ForeColor = default(Color);

                    if (selNode == clNode)
                    {
                        ClearClientInfo();
                        UpdateStatusButtons(ClientStatus_t.Unknown);
                    }
                }
            }
        }

        private void ClientStatus_DatumReplaced(IDataRow datum)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(ClientStatus_DatumReplaced) , datum);
            else
            {
                TreeNode selNode = m_tvClients.SelectedNode;

                if (selNode != null && selNode.Parent != null && (selNode.Tag as HubClient).ID == datum.ID)
                {
                    var clStatus = datum as ClientStatus;
                    UpdateStatusButtons(clStatus.Status);
                    m_lblStatus.Text = ClientStatuses.GetStatusName(clStatus.Status);
                    m_lblLastActivity.Text = GetComprehensiveTime(clStatus.LastSeen);
                }
            }
        }

        private void Clients_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(Clients_DatumReplaced) , row);
            else
            {
                TreeNode node = LocateClientNode(row.ID);

                if (node != null)
                {
                    node.Tag = row;

                    if (m_tvClients.SelectedNode == node)
                    {
                        var clStatus = m_ndxerStatus.Get(row.ID) as ClientStatus;

                        SetClientInfo(row as HubClient);
                        UpdateStatusButtons(clStatus.Status);
                    }

                }
            }
        }

    }
}
