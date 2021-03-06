﻿using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubGovernor.Log;
using DGD.HubGovernor.Profiles;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
        readonly Font m_tmFont;


        public ClientsManagmentWindow()
        {
            InitializeComponent();

            m_ndxerProfiles = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.USER_PROFILE);
            m_ndxerStatus = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_STATUS);
            m_ndxerClientEnv = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_ENV);
            m_ndxerClients = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.HUB_CLIENT);

            m_tmFont = new Font(Font , FontStyle.Bold | FontStyle.Italic);
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
            m_tmFont.Dispose();
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

        TreeNode LocateProfileNode(uint profileID)
        {
            foreach (TreeNode node in m_tvClients.Nodes)
                if ((node.Tag as UserProfile).ID == profileID)
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
                AppContext.LogManager.LogSysError("Chargement de la liste des clients actifs: " + t.Exception.InnerException.Message , true);

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
                AppContext.LogManager.LogSysError("Chargement de la liste de tous les clients: " + t.Exception.InnerException.Message , true);
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
            m_ndxerClients.DatumDeleted -= Clients_DatumDeleted;
            AppContext.LogManager.ClientLogAdded -= LogManager_ClientLogAdded;
        }

        void RegisterHandlers()
        {
            AppContext.ClientsManager.ClientClosed += ClientsManager_ClientClosed;
            AppContext.ClientsManager.ClientStarted += ClientsManager_ClientStarted;
            m_ndxerStatus.DatumReplaced += ClientStatus_DatumReplaced;
            m_ndxerClients.DatumReplaced += Clients_DatumReplaced;
            m_ndxerClients.DatumDeleted += Clients_DatumDeleted;
            AppContext.LogManager.ClientLogAdded += LogManager_ClientLogAdded;
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

            AppContext.LogManager.LogUserActivity($"Action utilisateur:  Changement du statut du client {ClientsManager.ClientStrID(client.ID)} en {ClientStatuses.GetStatusName(status)}");

            AppContext.ClientsManager.SetProfileManagementMode(client.ProfileID , ManagementMode_t.Manual);

            //maj le status
            AppContext.ClientsManager.SetClientStatus(client , status);
        }

        static string GetComprehensiveTime(DateTime dt)
        {
            return $"{dt.ToShortDateString()} à {dt.ToShortTimeString()}";
            
            //TimeSpan ts = now.Subtract(dt);

            //if (ts.Days > 0)
            //    return dt.ToString();

            //if (ts.Hours > 0)
            //    return $"{ts.Hours} heure(s) et {ts.Minutes} Minute(s)";

            //if (ts.Minutes > 0)
            //    return $"{ts.Minutes} Minute(s)";

            //return "Quelques secondes";

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
                m_lblCreationTime.Text = GetComprehensiveTime(hubClient.CreationTime);
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

        void AddLog(IEventLog evLog , bool ensureVisible = false)
        {
            m_rtbClientLog.SelectionBullet = true;

            m_rtbClientLog.SelectionIndent = 10;
            m_rtbClientLog.SelectionFont = m_tmFont;
            m_rtbClientLog.SelectedText = evLog.Time.ToString() + ": ";
            m_rtbClientLog.SelectionFont = m_rtbClientLog.Font;

            if (evLog.EventType == EventType_t.Error)
                m_rtbClientLog.SelectionColor = Color.Red;

            m_rtbClientLog.SelectedText = evLog.Text + Environment.NewLine;

            m_rtbClientLog.SelectionBullet = false;
            m_rtbClientLog.AppendText(Environment.NewLine);

            if (ensureVisible)
                m_rtbClientLog.ScrollToCaret();
        }

        void LoadClientLog(uint clID)
        {
            Dbg.Assert(!InvokeRequired);

            ClearLog();

            AppContext.LogManager.ClientLogAdded -= LogManager_ClientLogAdded;

            if (m_tsbShowActivityHistory.Checked)
            {
                bool loggerStarted = AppContext.LogManager.IsLoggerStarted(clID);

                if (!loggerStarted)
                    AppContext.LogManager.StartLogger(clID);

                IEnumerable<IEventLog> logs = AppContext.LogManager.EnumerateClientLog(clID);

                foreach (IEventLog log in logs)
                    AddLog(log);

                if (!loggerStarted)
                    AppContext.LogManager.CloseLogger(clID);

            }
            else if (AppContext.ClientsManager.IsClientRunning(clID))
            {
                IEnumerable<IEventLog> logs = AppContext.ClientsManager.GetClientLog(clID);

                if (logs != null)
                    foreach (IEventLog log in logs)
                        AddLog(log);
            }

            AppContext.LogManager.ClientLogAdded += LogManager_ClientLogAdded;
        }

        void ClearLog()
        {
            m_rtbClientLog.Clear();
            m_rtbClientLog.SelectionBullet = false;
        }

        //handlers
        private void Profiles_Click(object sender , EventArgs e) => new ProfilesWindow().Show(Owner);

        private void Clients_AfterSelect(object sender , TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                ClearClientInfo();
                ClearLog();
                UpdateStatusButtons(ClientStatus_t.Unknown);
            }
            else
            {
                var client = e.Node.Tag as HubClient;
                SetClientInfo(client);

                var clStatus = m_ndxerStatus.Get(client.ID) as ClientStatus;

                UpdateStatusButtons(clStatus.Status);
                LoadClientLog(client.ID);
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
            ClearLog();
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
                    }

                    TreeNode selNode = m_tvClients.SelectedNode;

                    if (selNode != null && selNode.Parent != null)
                    {
                        var hc = selNode.Tag as HubClient;

                        if (hc.ID == clID)
                        {
                            UpdateStatusButtons(ClientStatus_t.Enabled);
                            LoadClientLog(client.ID);
                            SetClientInfo(hc);

                        }
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

                        //if (!m_tsbShowActivityHistory.Checked)
                        //    ClearLog();

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

        void Clients_DatumDeleted(IDataRow row)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(Clients_DatumDeleted) , row);
            else
            {
                TreeNode node = LocateClientNode(row.ID);

                if (node != null)
                {
                    var client = row as HubClient;
                    TreeNode root = LocateProfileNode(client.ProfileID);
                    root.Nodes.Remove(node);

                    if (root.Nodes.Count == 0)
                        m_tvClients.Nodes.Remove(root);
                }
            }
        }

        private void Reset_Click(object sender , EventArgs e)
        {
            const string msg = "Vous êtes sur le point d’envoyer une demande de réinitialisation " +
                "au client sélectionné, celle-ci forcera ce dernier à s’enregistrer une nouvelle " +
                "fois sous un autre ID. Voulez-vous poursuivre ?";

            if (MessageBox.Show(msg , Text , MessageBoxButtons.YesNo , MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            TreeNode selNode = m_tvClients.SelectedNode;

            Dbg.Assert(selNode != null && selNode.Parent != null);

            var client = selNode.Tag as HubClient;

            AppContext.LogManager.LogUserActivity($"Action utilisateur :  Réinitialisation du client {ClientsManager.ClientStrID(client.ID)}");


            AppContext.LogManager.LogSysActivity("Envoi d'une notification de réinitialisation au client " +
                $"{ClientsManager.ClientStrID(client.ID)}" , true);

            AppContext.ClientsManager.SetClientStatus(client , ClientStatus_t.Reseted);
        }

        private void LogManager_ClientLogAdded(uint clID , IEventLog log)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<uint , IEventLog>(LogManager_ClientLogAdded) , clID , log);
            else
            {
                TreeNode selNode = m_tvClients.SelectedNode;

                if (selNode == null || selNode.Parent == null)
                    return;

                if ((selNode.Tag as HubClient).ID != clID)
                    return;

                AddLog(log , true);
            }
        }

        private void ShowActivityHistory_Click(object sender , EventArgs e)
        {
            bool showHistory = !m_tsbShowActivityHistory.Checked;
            m_tsbShowActivityHistory.Checked = showHistory;

            TreeNode selNode = m_tvClients.SelectedNode;

            if (selNode == null || selNode.Parent == null)
                return;

            LoadClientLog((selNode.Tag as HubClient).ID);

        }

        private void DeleteClient_Click(object sender , EventArgs e)
        {
            TreeNode selNode = m_tvClients.SelectedNode;

            Dbg.Assert(selNode != null && selNode.Parent != null);
            var client = selNode.Tag as HubClient;
            var clStatus = m_ndxerStatus.Get(client.ID) as ClientStatus;

            if (clStatus.Status != ClientStatus_t.Reseted)
            {
                const string msg = "Vous ne supprimer le client sélectionné, " +
                    "seuls les clients réinitialisés peuvent être supprimés.";

                MessageBox.Show(msg , Text , MessageBoxButtons.OK);
                return;
            }

            var waitDlg = new Jobs.ProcessingDialog();

            Action<Task> onErr = t =>
            {
                AppContext.LogManager.LogSysError($"Erreur lors de la suppression du client {ClientsManager.ClientStrID(client.ID)}:" +
                    t.Exception.InnerException.Message , true);

                waitDlg.Close();
            };

            Action onSuccess = () =>
            {
                AppContext.LogManager.LogSysActivity($"Action utilisateur: Suppression du client {ClientsManager.ClientStrID(client.ID)}" , true);
                waitDlg.Close();
            };

            var task = new Task(() => AppContext.ClientsManager.DeleteClient(client.ID) , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);
            task.Start();
            waitDlg.ShowDialog(this);
        }

        private void RunOnce_Click(object sender , EventArgs e)
        {
            var wind = new RunOnce.RunOnceWindow();
            wind.Show(Owner);
        }
    }
}
