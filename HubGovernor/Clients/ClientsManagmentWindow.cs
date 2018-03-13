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
            LoadDataAsync();
            RegisterHandlers();

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            UnregisterHandlers();
        }

        //private:
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

        void LoadDataAsync()
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
            //AppContext.ClientsManager.ClientClosed -= ClientsManager_ClientClosed;
            //AppContext.ClientsManager.ClientStarted -= ClientsManager_ClientStarted;
        }

        void RegisterHandlers()
        {
            //AppContext.ClientsManager.ClientClosed += ClientsManager_ClientClosed;
            //AppContext.ClientsManager.ClientStarted += ClientsManager_ClientStarted;
        }


        //void LoadRunningClients()
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action(LoadRunningClients));
        //    else
        //    {
        //        m_lvClients.Items.Clear();

        //        foreach(HubClient cl in AppContext.ClientsManager.RunningClients)
        //        {
        //            var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
        //            {
        //                Tag = cl ,
        //                ImageIndex = NDXIMG_RUNNING
        //            };

        //            m_lvClients.Items.Add(lvi);
        //        }
        //    }
        //}

        //void LoadEnabledClients()
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action(LoadEnabledClients));
        //    else
        //    {
        //        LoadRunningClients();

        //        foreach (HubClient cl in AppContext.ClientsManager.EnabledClients)
        //            if (!AppContext.ClientsManager.IsClientRunning(cl.ID))
        //            {
        //                var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
        //                {
        //                    Tag = cl ,
        //                    ImageIndex = NDXIMG_NOTRUNNING
        //                };

        //                m_lvClients.Items.Add(lvi);
        //            }
        //    }
        //}


        //void LoadAllClients()
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action(LoadAllClients));
        //    else
        //    {
        //        m_lvClients.Items.Clear();

        //        var runningClients = new List<ListViewItem>();
        //        var otherClients = new List<ListViewItem>();

        //        foreach (HubClient cl in AppContext.ClientsManager.AllClients)
        //        {
        //            var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
        //            {
        //                Tag = cl
        //            };

        //            if (AppContext.ClientsManager.IsClientRunning(cl.ID))
        //            {
        //                lvi.ImageIndex = NDXIMG_RUNNING;
        //                runningClients.Add(lvi);
        //            }
        //            else
        //            {
        //                lvi.ImageIndex = NDXIMG_NOTRUNNING;
        //                otherClients.Add(lvi);
        //            }
        //        }

        //        m_lvClients.Items.AddRange(runningClients.ToArray());
        //        m_lvClients.Items.AddRange(otherClients.ToArray());
        //    }
        //}

        static string GetComprehensiveTime(DateTime dt)
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = now.Subtract(dt);

            if (ts.Days > 0)
                return dt.ToString();

            if (ts.Hours > 0)
                return $"{ts.Hours} heure(s) et {ts.Minutes} Minute(s)";

            if(ts.Minutes > 0)
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

        private void SetClientInfo(HubClient hubClient)
        {
            if (InvokeRequired)
                Invoke(new Action<HubClient>(SetClientInfo) , hubClient);
            else
            {
                HubClientEnvironment clEnv =
                    m_ndxerClientEnv.Source.Enumerate().Cast<HubClientEnvironment>().Where(env =>
                    env.ClientID == hubClient.ID).Single();

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

        ////handlers
        private void Profiles_Click(object sender , EventArgs e)
        {
            var wind = new Profiles.ProfilesWindow();
            wind.Show(Owner);
        }

        private void Clients_AfterSelect(object sender , TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
                ClearClientInfo();
            else
                SetClientInfo(e.Node.Tag as HubClient);
        }

        //private void Clients_Click(object sender , EventArgs e)
        //{
        //    var wind = new ClientsWindow();
        //    wind.Show(Owner);
        //}

        //private void ClientsManager_ClientStarted(uint clID)
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action<uint>(ClientsManager_ClientStarted) , clID);
        //    else
        //    {
        //        using (var ndxer = new KeyIndexer(AppContext.TableManager.HubClients.DataProvider))
        //        {
        //            ndxer.Connect();

        //            var client = ndxer.Get(clID) as HubClient;

        //            if (client != null)
        //            {
        //                var lvi = new ListViewItem(ClientsManager.ClientStrID(clID))
        //                {
        //                    Tag = client,
        //                    ImageIndex = NDXIMG_RUNNING
        //                };

        //                m_lvClients.Items.Add(lvi);
        //            }
        //        }
        //    }
        //}

        //private void ClientsManager_ClientClosed(uint clID)
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action<uint>(ClientsManager_ClientClosed) , clID);
        //    else
        //    {
        //        ListViewItem lviClient = null;

        //        foreach (ListViewItem lvi in m_lvClients.Items)
        //        {
        //            var cl = lvi.Tag as HubClient;

        //            if (cl.ID == clID)
        //            {
        //                lviClient = lvi;
        //                break;
        //            }
        //        }


        //        if (lviClient != null)
        //            m_lvClients.Items.Remove(lviClient);
        //    }
        //}

        //private void ClientsType_SelectedIndexChanged(object sender , EventArgs e)
        //{
        //    int selIndex = m_tscbClientsType.SelectedIndex;

        //    switch(selIndex)
        //    {
        //        case NDXITEM_ALL:
        //        LoadAllClients();
        //        break;

        //        case NDXITEM_ENABLED:
        //        LoadEnabledClients();
        //        break;

        //        case NDXITEM_RUNNING:
        //        LoadRunningClients();
        //        break;
        //    }
        //}

        //private void Clients_SelectedIndexChanged(object sender , EventArgs e)
        //{
        //    var Sel = m_lvClients.SelectedItems;

        //    if (Sel.Count == 1)
        //        SetClientInfo(Sel[0].Tag as HubClient);
        //    else
        //        ClearClientInfo();
        //}
    }
}
