using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubGovernor.Clients;
using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.ListViewSorters;
using easyLib.DB;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace DGD.HubGovernor.Profiles
{
    public partial class ProfilesWindow: Form
    {
        enum ProfileStatus_t
        {
            NotAssigned,
            AssignedAndEnabled,
            AssignedAndDisbaled,
            AssignedAndBanned
        }

        static readonly string[] m_profileStatusNames =
        {
            "Non attribué",
            "Attribué, activé",
            "Attribué, désactivé",
            "Attribué, banni"
        };

        readonly IDatumProvider m_dpProfiles;
        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerMgmntMode;
        readonly Dictionary<ColumnDataType_t , Func<int , IColumnSorter>> m_colSorters;


        public ProfilesWindow()
        {
            InitializeComponent();

            m_dpProfiles = AppContext.TableManager.Profiles.DataProvider;
            m_ndxerProfiles = new KeyIndexer(m_dpProfiles);
            m_ndxerMgmntMode = new KeyIndexer(AppContext.TableManager.ProfileManagementMode.DataProvider);

            m_colSorters = new Dictionary<ColumnDataType_t , Func<int , IColumnSorter>>
            {
                { ColumnDataType_t.Integer, ndxCol => new IntegerColumnSorter(ndxCol) },
                { ColumnDataType_t.Text, ndxCol => new TextColumnSorter(ndxCol) },
            };

        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_ndxerProfiles.Connect();
            m_ndxerMgmntMode.Connect();

            LoadData();
            RegisterHandlers();

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.Cancel)
                return;

            UnregisterHandlers();
            m_ndxerMgmntMode.Close();
            m_ndxerProfiles.Close();

        }

        //private:
        ProfileStatus_t GetProfileStatus(UserProfile profile)
        {
            using (IDatumProvider dpClients = AppContext.TableManager.HubClients.DataProvider)
            {
                dpClients.Connect();

                var clients = from HubClient cl in dpClients.Enumerate()
                              where cl.ProfileID == profile.ID
                              select cl;

                if (!clients.Any())
                    return ProfileStatus_t.NotAssigned;

                ProfileStatus_t result = ProfileStatus_t.AssignedAndBanned;

                using (IDatumProvider dpStatus = AppContext.TableManager.ClientsStatus.DataProvider)
                {
                    dpStatus.Connect();

                    var clStatus = from ClientStatus cs in dpStatus.Enumerate()
                                   join client in clients on cs.ClientID equals client.ID
                                   select cs;

                    foreach (ClientStatus cs in clStatus)
                        if (cs.Status == ClientStatus_t.Enabled)
                        {
                            result = ProfileStatus_t.AssignedAndEnabled;
                            break;
                        }
                        else if (cs.Status == ClientStatus_t.Disabled)
                            result = ProfileStatus_t.AssignedAndDisbaled;
                }

                return result;
            }
        }

        void LoadData()
        {
            Dbg.Assert(!InvokeRequired);

            m_lvData.Items.Clear();

            var seq = from UserProfile p in m_dpProfiles.Enumerate()
                      let strs = new[]
                         {
                              p.ID.ToString(),
                              p.Name,
                              ProfilePrivileges.GetPrivilegeName(p.Privilege),
                              m_profileStatusNames[(int) GetProfileStatus(p)],
                              ProfileManagementMode.GetManagementModeName(
                                  AppContext.ClientsManager.GetProfileManagementMode(p.ID))
                          }
                      select new ListViewItem(strs)
                      {
                          Tag = p
                      };

            m_lvData.Items.AddRange(seq.ToArray());
        }

        void RegisterHandlers()
        {
            m_ndxerProfiles.DatumDeleted += Profiles_DatumDeleted;
            m_ndxerProfiles.DatumInserted += Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced += Profiles_DatumReplaced;
            m_ndxerMgmntMode.DatumReplaced += ManagementMode_DatumReplaced;
        }

        void UnregisterHandlers()
        {
            m_ndxerProfiles.DatumDeleted -= Profiles_DatumDeleted;
            m_ndxerProfiles.DatumInserted -= Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced -= Profiles_DatumReplaced;
            m_ndxerMgmntMode.DatumReplaced -= ManagementMode_DatumReplaced;
        }


        //handlers:
        private void NewProfile_Click(object sender , EventArgs e)
        {
            var form = new ProfileForm(m_dpProfiles);
            form.Show(Owner);
        }

        private void Data_ItemActivate(object sender , EventArgs e)
        {
            var sel = m_lvData.SelectedIndices;

            if (sel.Count != 1)
                return;

            var profile = m_lvData.Items[sel[0]].Tag as UserProfile;
            var form = new ProfileForm(m_dpProfiles , profile);

            form.Show(Owner);
        }

        private void Data_SelectedIndexChanged(object sender , EventArgs e)
        {
            m_tsbDeleteProfile.Enabled = m_lvData.SelectedItems.Count > 0;

            if (m_lvData.SelectedItems.Count == 1)
            {
                m_tsbClients.Enabled = m_tsbAutoManagement.Enabled = true;
                UserProfile prf = m_lvData.SelectedItems[0].Tag as UserProfile;

                m_tsbAutoManagement.Checked =
                    AppContext.ClientsManager.GetProfileManagementMode(prf.ID) == ManagementMode_t.Auto;
            }
            else
                m_tsbClients.Enabled = m_tsbAutoManagement.Enabled = false;
        }

        private void DeleteProfile_Click(object sender , EventArgs e)
        {
            List<UserProfile> profiles = (from ListViewItem lvi in m_lvData.SelectedItems
                                          select lvi.Tag as UserProfile).ToList();


            int initCount = profiles.Count;

            using (IDatumProvider dpClients = AppContext.TableManager.HubClients.DataProvider)
            {
                dpClients.Connect();

                foreach (HubClient hc in dpClients.Enumerate())
                {
                    int ndx = profiles.FindIndex(p => p.ID == hc.ProfileID);

                    if (ndx >= 0)
                        profiles.RemoveAt(ndx);
                }



                if (profiles.Count() == 0)
                {
                    const string msg = "Tous les profiles sélectionnés pour la suppression " +
                        "sont associés à des clients. " +
                        "Pour supprimer un profile celui-ci ne doit être associé à aucun client.";

                    MessageBox.Show(msg , Text);
                    return;
                }

                if (initCount != profiles.Count)
                {
                    const string msg = "Certains profiles candidats à la suppression sont associés " +
                        "à des clients. Ceux-ci ne seront pas supprimés. Voulez-vous poursuivre ?";

                    if (MessageBox.Show(msg , Text , MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
            }

            const string msgContinue = "Veuillez confirmer la suppression. Poursuivre ?";

            if (MessageBox.Show(msgContinue , Text , MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;


            foreach (IDataRow row in profiles)
            {
                int ndx = m_ndxerProfiles.IndexOf(row.ID);
                m_dpProfiles.Delete(ndx);

                //sup. le mode gestion
               m_ndxerMgmntMode.Source.Delete(m_ndxerMgmntMode.IndexOf(row.ID));
            }


            TextLogger.Info($"{profiles.Count()} profiles(s) supprimé(s).");
        }

        private void Profiles_DatumReplaced(IDataRow row)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(Profiles_DatumReplaced) , row);
            else
            {
                var profile = row as UserProfile;

                //pas besoin de remplacer le mode gestion car la form du profil
                //ne le prend pas en charge 

                for (int i = 0; i < m_lvData.Items.Count; ++i)
                    if ((m_lvData.Items[i].Tag as UserProfile).ID == profile.ID)
                    {
                        var strs = new string[]
                        {
                              profile.ID.ToString(),
                              profile.Name,
                              ProfilePrivileges.GetPrivilegeName(profile.Privilege),
                              m_profileStatusNames[(int) GetProfileStatus(profile)],
                              ProfileManagementMode.GetManagementModeName(
                                  AppContext.ClientsManager.GetProfileManagementMode(profile.ID))
                    };

                        var newItem = new ListViewItem(strs)
                        { Tag = profile };

                        m_lvData.Items[i] = newItem;
                        break;
                    }
            }
        }

        private void Profiles_DatumInserted(IDataRow row)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(Profiles_DatumInserted) , row);
            else
            {
                var profile = row as UserProfile;
                var strs = new string[]
                {
                              profile.ID.ToString(),
                              profile.Name,
                              ProfilePrivileges.GetPrivilegeName(profile.Privilege),
                              m_profileStatusNames[(int) GetProfileStatus(profile)],
                              ProfileManagementMode.GetManagementModeName(
                                  AppContext.ClientsManager.GetProfileManagementMode(profile.ID))
                };

                var lvi = new ListViewItem(strs)
                { Tag = profile };

                m_lvData.Items.Add(lvi);
            }
        }

        private void Profiles_DatumDeleted(IDataRow row)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<IDataRow>(Profiles_DatumDeleted) , row);
            else
            {
                for (int i = 0; i < m_lvData.Items.Count; ++i)
                    if ((m_lvData.Items[i].Tag as UserProfile).ID == row.ID)
                    {
                        m_lvData.Items.RemoveAt(i);
                        break;
                    }
            }
        }

        private void AdjustColumns_Click(object sender , EventArgs e)
        {
            UseWaitCursor = true;
            m_lvData.AdjustColumnsSize();
            UseWaitCursor = false;
        }

        private void ManagementMode_DatumReplaced(IDataRow row)
        {

            if (InvokeRequired)
                Invoke(new Action<IDataRow>(ManagementMode_DatumReplaced) , row);
            else
            {
                var lvi = (from item in m_lvData.Items.AsEnumerable<ListViewItem>()
                           where (item.Tag as UserProfile).ID == row.ID
                           select item).Single();

                ManagementMode_t mode = (m_ndxerMgmntMode.Get(row.ID) as ProfileManagementMode).ManagementMode;

                if (lvi.Selected)
                    m_tsbAutoManagement.Checked = mode == ManagementMode_t.Auto;

                lvi.SubItems[m_colMgmntMode.Index].Text = ProfileManagementMode.GetManagementModeName(mode);
            }
        }

        private void AutoManagement_Click(object sender , EventArgs e)
        {
            ListViewItem lvi = m_lvData.SelectedItems[0];
            var prf = lvi.Tag as UserProfile;

            var prfMgmntMode = m_ndxerMgmntMode.Get(prf.ID) as ProfileManagementMode;

            ManagementMode_t mode = m_tsbAutoManagement.Checked ? ManagementMode_t.Manual :
                ManagementMode_t.Auto;

            m_tsbAutoManagement.Checked = !m_tsbAutoManagement.Checked;
            prfMgmntMode.ManagementMode = mode;
            m_ndxerMgmntMode.Source.Replace(m_ndxerMgmntMode.IndexOf(prf.ID) , prfMgmntMode);
        }

        private void Clients_Click(object sender , EventArgs e)
        {
            var prf = m_lvData.SelectedItems[0].Tag as UserProfile;

            var clWind = new Clients.ClientsWindow(ndxerProfiles: m_ndxerProfiles , idProfile: prf.ID);
            clWind.Show(Owner);
        }

        private void Data_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            var sorter = m_lvData.ListViewItemSorter as IColumnSorter;

            if (sorter == null || e.Column != sorter.ColumnIndex)
            {
                if (sorter != null)
                    m_lvData.SetColumnHeaderSortIcon(sorter.ColumnIndex , SortOrder.None);

                sorter = m_colSorters[(ColumnDataType_t)m_lvData.Columns[e.Column].Tag](e.Column);
                m_lvData.ListViewItemSorter = sorter;
                m_lvData.SetColumnHeaderSortIcon(e.Column , SortOrder.Ascending);
            }
            else
            {
                sorter.SortDescending = !sorter.SortDescending;
                m_lvData.Sort();

                m_lvData.SetColumnHeaderSortIcon(e.Column ,
                    sorter.SortDescending ? SortOrder.Descending : SortOrder.Ascending);
            }
        }

    }
}