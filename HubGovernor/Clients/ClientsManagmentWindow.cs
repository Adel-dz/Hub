using DGD.HubCore.DB;
using DGD.HubCore.DLG;
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
        const int NDXIMG_RUNNING = 0;
        const int NDXIMG_NOTRUNNING = 1;
        const int NDXITEM_RUNNING = 0;
        const int NDXITEM_ENABLED = 1;
        const int NDXITEM_ALL = 2;

        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerStatus;


        public ClientsManagmentWindow()
        {
            InitializeComponent();

            m_ndxerProfiles = new KeyIndexer(AppContext.TableManager.Profiles.DataProvider);
            m_ndxerStatus = new KeyIndexer(AppContext.TableManager.ClientsStatus.DataProvider);

            m_tscbClientsType.SelectedIndex = 0;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_ndxerProfiles.Connect();
            m_ndxerStatus.Connect();

            LoadRunningClients();
            RegisterHandlers();

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            m_ndxerStatus.Close();
            m_ndxerProfiles.Close();

            UnregisterHandlers();
        }

        //private:

        void UnregisterHandlers()
        {
            AppContext.ClientsManager.ClientClosed -= ClientsManager_ClientClosed;
            AppContext.ClientsManager.ClientStarted -= ClientsManager_ClientStarted;
        }

        void RegisterHandlers()
        {
            AppContext.ClientsManager.ClientClosed += ClientsManager_ClientClosed;
            AppContext.ClientsManager.ClientStarted += ClientsManager_ClientStarted;
        }


        void LoadRunningClients()
        {
            if (InvokeRequired)
                Invoke(new Action(LoadRunningClients));
            else
            {
                m_lvClients.Items.Clear();

                foreach(HubClient cl in AppContext.ClientsManager.RunningClients)
                {
                    var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
                    {
                        Tag = cl ,
                        ImageIndex = NDXIMG_RUNNING
                    };

                    m_lvClients.Items.Add(lvi);
                }
            }
        }

        void LoadEnabledClients()
        {
            if (InvokeRequired)
                Invoke(new Action(LoadEnabledClients));
            else
            {
                LoadRunningClients();

                foreach (HubClient cl in AppContext.ClientsManager.EnabledClients)
                    if (!AppContext.ClientsManager.IsClientRunning(cl.ID))
                    {
                        var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
                        {
                            Tag = cl ,
                            ImageIndex = NDXIMG_NOTRUNNING
                        };

                        m_lvClients.Items.Add(lvi);
                    }
            }
        }


        void LoadAllClients()
        {
            if (InvokeRequired)
                Invoke(new Action(LoadAllClients));
            else
            {
                m_lvClients.Items.Clear();

                var runningClients = new List<ListViewItem>();
                var otherClients = new List<ListViewItem>();

                foreach (HubClient cl in AppContext.ClientsManager.AllClients)
                {
                    var lvi = new ListViewItem(ClientsManager.ClientStrID(cl.ID))
                    {
                        Tag = cl
                    };

                    if (AppContext.ClientsManager.IsClientRunning(cl.ID))
                    {
                        lvi.ImageIndex = NDXIMG_RUNNING;
                        runningClients.Add(lvi);
                    }
                    else
                    {
                        lvi.ImageIndex = NDXIMG_NOTRUNNING;
                        otherClients.Add(lvi);
                    }
                }

                m_lvClients.Items.AddRange(runningClients.ToArray());
                m_lvClients.Items.AddRange(otherClients.ToArray());
            }
        }


        void ClearClientInfo()
        {
            if (InvokeRequired)
                Invoke(new Action(ClearClientInfo));
            else
                m_lblContact.Text = m_lblProfil.Text = m_lblStatus.Text = "-";
        }

        private void SetClientInfo(HubClient hubClient)
        {
            if (InvokeRequired)
                Invoke(new Action<HubClient>(SetClientInfo) , hubClient);
            else
            {
                m_lblContact.Text = hubClient.ContactName;
                m_lblProfil.Text = (m_ndxerProfiles.Get(hubClient.ProfileID) as Profiles.UserProfile).Name;

                var clStatus = m_ndxerStatus.Get(hubClient.ID) as ClientStatus;

                m_lblStatus.Text = ClientStatuses.GetStatusName(clStatus.Status);
            }
        }

        //handlers
        private void Profiles_Click(object sender , EventArgs e)
        {
            var wind = new Profiles.ProfilesWindow();
            wind.Show(Owner);
        }

        private void Clients_Click(object sender , EventArgs e)
        {
            var wind = new ClientsWindow();
            wind.Show(Owner);
        }

        private void ClientsManager_ClientStarted(uint clID)
        {
            if (InvokeRequired)
                Invoke(new Action<uint>(ClientsManager_ClientStarted) , clID);
            else
            {
                using (var ndxer = new KeyIndexer(AppContext.TableManager.HubClients.DataProvider))
                {
                    ndxer.Connect();

                    var client = ndxer.Get(clID) as HubClient;

                    if (client != null)
                    {
                        var lvi = new ListViewItem(ClientsManager.ClientStrID(clID))
                        {
                            Tag = client,
                            ImageIndex = NDXIMG_RUNNING
                        };

                        m_lvClients.Items.Add(lvi);
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
                ListViewItem lviClient = null;

                foreach (ListViewItem lvi in m_lvClients.Items)
                {
                    var cl = lvi.Tag as HubClient;

                    if (cl.ID == clID)
                    {
                        lviClient = lvi;
                        break;
                    }
                }


                if (lviClient != null)
                    m_lvClients.Items.Remove(lviClient);
            }
        }

        private void ClientsType_SelectedIndexChanged(object sender , EventArgs e)
        {
            int selIndex = m_tscbClientsType.SelectedIndex;

            switch(selIndex)
            {
                case NDXITEM_ALL:
                LoadAllClients();
                break;

                case NDXITEM_ENABLED:
                LoadEnabledClients();
                break;

                case NDXITEM_RUNNING:
                LoadRunningClients();
                break;
            }
        }

        private void Clients_SelectedIndexChanged(object sender , EventArgs e)
        {
            var Sel = m_lvClients.SelectedItems;

            if (Sel.Count == 1)
                SetClientInfo(Sel[0].Tag as HubClient);
            else
                ClearClientInfo();
        }
    }
}
