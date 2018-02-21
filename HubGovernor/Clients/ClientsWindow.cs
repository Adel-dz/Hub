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
    sealed partial class ClientsWindow: Form
    {
        const int NDX_IMG_PROFILE = 0;
        const int NDX_IMG_CLIENT = 1;

        readonly KeyIndexer m_ndxerClients;
        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerStatus;
        readonly uint m_profilesID;


        public ClientsWindow(KeyIndexer ndxerClients = null , KeyIndexer ndxerProfiles = null ,
            KeyIndexer ndxerStatus = null , uint idProfile = 0)
        {
            InitializeComponent();

            m_ndxerClients = ndxerClients ?? new KeyIndexer(AppContext.TableManager.HubClients.DataProvider);
            m_ndxerProfiles = ndxerProfiles ?? new KeyIndexer(AppContext.TableManager.Profiles.DataProvider);
            m_ndxerStatus = ndxerStatus ?? new KeyIndexer(AppContext.TableManager.ClientsStatus.DataProvider);

            m_profilesID = idProfile;
        }

        //protected:

        protected override void OnLoad(EventArgs e)
        {
            m_ndxerClients.Connect();
            m_ndxerProfiles.Connect();
            m_ndxerStatus.Connect();

            LoadDataAsync();

            RegisterHandlers();

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            UnregisterHandlers();

            m_ndxerStatus.Close();
            m_ndxerClients.Close();
            m_ndxerProfiles.Close();
        }

        //Private:
        void LoadDataAsync()
        {
            Func<IEnumerable<UserProfile>> loadProfiles = () =>
            {
                IEnumerable<UserProfile> profiles;


                if (m_profilesID == 0)
                    profiles = from UserProfile prf in m_ndxerProfiles.Source.Enumerate()
                               select prf;
                else
                    profiles = from UserProfile prf in m_ndxerProfiles.Source.Enumerate()
                               where prf.ID == m_profilesID
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
                                    select new TreeNode(client.ID.ToString("X"))
                                    {
                                        Tag = client ,
                                        SelectedImageIndex = NDX_IMG_CLIENT ,
                                        ImageIndex = NDX_IMG_CLIENT
                                    };

                      var root = new TreeNode(prf.Name , clients.ToArray())
                      {
                          Tag = prf ,
                          SelectedImageIndex = NDX_IMG_PROFILE ,
                          ImageIndex = NDX_IMG_PROFILE
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

        void FillClientInfo(HubClient client)
        {
            Dbg.Assert(!InvokeRequired);

            m_lblContactName.Text = client.ContactName;
            m_lblCreationTime.Text = client.CreationTime.ToString();
            m_lblEMail.Text = string.IsNullOrWhiteSpace(client.ContaclEMail) ? "-" : client.ContaclEMail;
            m_lblMachineName.Text = client.MachineName;
            m_lblPhone.Text = string.IsNullOrWhiteSpace(client.ContactPhone) ? "-" : client.ContactPhone;

            ClientStatus_t status = (m_ndxerStatus.Get(client.ID) as ClientStatus).Status;
            m_lblStatus.Text = ClientStatuses.GetStatusName(status);
        }

        void ClearClientInfo()
        {
            Dbg.Assert(!InvokeRequired);

            m_lblContactName.Text = "-";
            m_lblCreationTime.Text = "-";
            m_lblEMail.Text = "-";
            m_lblMachineName.Text = "-";
            m_lblPhone.Text = "-";
            m_lblStatus.Text = "-";
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

        void UpdateStatusButtons(ClientStatus_t status)
        {
            Dbg.Assert(!InvokeRequired);

            m_tsbBanishClient.Checked = m_tsbDisableClient.Checked = m_tsbEnableClient.Checked = false;

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

                default:
                Dbg.Assert(false);
                break;
            }
        }

        void RegisterHandlers()
        {
            m_ndxerStatus.Source.DatumReplaced += ClientStatus_DatumReplaced;
            m_ndxerProfiles.DatumDeleted += Profiles_DatumDeleted;
            m_ndxerProfiles.DatumInserted += Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced += Profiles_DatumReplaced;
            m_ndxerClients.DatumInserted += Clients_DatumInserted;
            m_ndxerClients.DatumReplaced += Clients_DatumReplaced;
        }

        void UnregisterHandlers()
        {
            m_ndxerStatus.Source.DatumReplaced -= ClientStatus_DatumReplaced;
            m_ndxerProfiles.DatumDeleted -= Profiles_DatumDeleted;
            m_ndxerProfiles.DatumInserted -= Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced -= Profiles_DatumReplaced;
            m_ndxerClients.DatumInserted -= Clients_DatumInserted;
            m_ndxerClients.DatumReplaced -= Clients_DatumReplaced;

        }



        //handlers:
        private void Clients_AfterSelect(object sender , TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                m_tsbBanishClient.Enabled = m_tsbDisableClient.Enabled = m_tsbEnableClient.Enabled = false;
                ClearClientInfo();
            }
            else
            {
                var client = e.Node.Tag as HubClient;
                FillClientInfo(client);
                m_tsbBanishClient.Enabled = m_tsbDisableClient.Enabled = m_tsbEnableClient.Enabled = true;
                var clStatus = m_ndxerStatus.Get(client.ID) as ClientStatus;
                UpdateStatusButtons(clStatus.Status);
            }
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

        private void ClientStatus_DatumReplaced(int ndx , easyLib.DB.IDatum datum)
        {

            if (InvokeRequired)
                Invoke(new Action<int , easyLib.DB.IDatum>(ClientStatus_DatumReplaced) , ndx , datum);
            else
            {
                var clStatus = datum as ClientStatus;

                TreeNode selNode = m_tvClients.SelectedNode;

                if (selNode != null && selNode.Parent != null && (selNode.Tag as HubClient).ID == clStatus.ClientID)
                {
                    UpdateStatusButtons(clStatus.Status);
                    m_lblStatus.Text = ClientStatuses.GetStatusName(clStatus.Status);
                }
            }
        }

        private void Clients_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(Clients_DatumReplaced) , row);
            else
            {
                var client = row as HubClient;

                foreach (TreeNode root in m_tvClients.Nodes)
                    foreach (TreeNode node in root.Nodes)
                    {
                        var cl = node.Tag as HubClient;

                        if (cl.ID == client.ID)
                            node.Tag = client;

                        if (m_tvClients.SelectedNode == node)
                            FillClientInfo(client);

                        break;
                    }
            }
        }

        private void Clients_DatumInserted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(Clients_DatumInserted) , row);
            else
            {
                foreach (TreeNode root in m_tvClients.Nodes)
                {
                    var prf = root.Tag as UserProfile;

                    if ((row as HubClient).ProfileID == prf.ID)
                    {
                        var client = row as HubClient;
                        var node = new TreeNode(row.ID.ToString("X"))
                        {
                            Tag = client ,
                            ImageIndex = NDX_IMG_CLIENT ,
                            SelectedImageIndex = NDX_IMG_CLIENT
                        };

                        root.Nodes.Add(node);

                        break;
                    }
                }
            }
        }

        private void Profiles_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(Profiles_DatumReplaced) , row);
            else
            {
                var prf = row as UserProfile;

                foreach (TreeNode root in m_tvClients.Nodes)
                    if ((root.Tag as UserProfile).ID == prf.ID)
                    {
                        root.Tag = prf;
                        root.Text = prf.Name;

                        break;
                    }
            }
        }

        private void Profiles_DatumDeleted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(Profiles_DatumDeleted) , row);
            else
                for(int i = 0; i < m_tvClients.Nodes.Count; ++i)
                    if((m_tvClients.Nodes[i].Tag as UserProfile).ID == row.ID)
                    {
                        m_tvClients.Nodes.RemoveAt(i);
                        break;
                    }
        }

        private void Profiles_DatumInserted(IDataRow row)
        {
            if (InvokeRequired)
                Invoke(new Action<IDataRow>(Profiles_DatumInserted) , row);
            else
            {
                var root = new TreeNode((row as UserProfile).Name)
                {
                    Tag = row ,
                    ImageIndex = NDX_IMG_PROFILE ,
                    SelectedImageIndex = NDX_IMG_PROFILE
                };

                m_tvClients.Nodes.Add(root);
            }
        }

    }
}
