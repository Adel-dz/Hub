using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using easyLib.DB;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DGD.HubCore.DLG;
using DGD.HubGovernor.Log;

namespace DGD.HubGovernor.Profiles
{
    sealed partial class ProfileForm: Form
    {

        class ComboBoxEntry
        {
            public ComboBoxEntry(string name , ProfilePrivilege_t privilege)
            {
                Privilege = privilege;
                Name = name;
            }


            public ProfilePrivilege_t Privilege { get; }
            public string Name { get; }

            public override string ToString() => Name;
        }

        readonly KeyIndexer m_ndxProfiles;
        readonly UserProfile m_datum;
        bool m_endDialog;

        public ProfileForm(IDatumProvider dp , UserProfile datum = null)
        {
            Dbg.Assert(dp != null);

            InitializeComponent();

            m_ndxProfiles = new KeyIndexer(dp);

            if (datum != null)
            {
                m_datum = datum;
                m_tsbReload.Click += Reload_Click;
            }
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_ndxProfiles.Connect();

            foreach (ProfilePrivilege_t p in ProfilePrivileges.Privileges)
                m_cbPrivilege.Items.Add(new ComboBoxEntry(ProfilePrivileges.GetPrivilegeName(p) , p));

            if (m_cbPrivilege.Items.Count > 0)
                m_cbPrivilege.SelectedIndex = 0;

            if (m_datum != null)
            {
                LoadDatumInfo();
                RegisterHandlers();
            }

            UpdateUI();

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (m_datum != null)
                UnregisterHandlers();

            m_ndxProfiles.Close();
        }


        //private:       
        void EndDialog()
        {
            m_endDialog = true;
            UpdateUI();
        }

        void UpdateUI()
        {
            if (m_endDialog)
            {
                m_tsbReload.Enabled = false;
                m_tsbSave.Enabled = false;
                m_tbProfileName.Enabled = false;
                m_cbPrivilege.Enabled = false;
            }
            else
            {
                m_tsbReload.Enabled = m_datum != null;
                m_tsbSave.Enabled = m_tbProfileName.TextLength > 0;
            }
        }

        void LoadDatumInfo()
        {
            m_tbProfileName.Text = m_datum.Name;

            SelectPrivilege(m_datum.Privilege);
        }

        void SelectPrivilege(ProfilePrivilege_t privilege)
        {
            ComboBoxEntry item = m_cbPrivilege.Items.Cast<ComboBoxEntry>().
                Single(x => x.Privilege == privilege);

            m_cbPrivilege.SelectedItem = item;
        }

        void UnregisterHandlers()
        {
            m_ndxProfiles.DatumDeleted -= Profiles_DatumDeleted;
            m_ndxProfiles.DatumReplaced -= Profiles_DatumReplaced;
        }

        void RegisterHandlers()
        {
            m_ndxProfiles.DatumDeleted += Profiles_DatumDeleted;
            m_ndxProfiles.DatumReplaced += Profiles_DatumReplaced;
        }

        bool AddDatum(string name , ProfilePrivilege_t privilege)
        {
            var seq = from UserProfile p in m_ndxProfiles.Source.Enumerate()
                      where string.Compare(p.Name , name , true) == 0
                      select p;

            if (seq.Count() > 0)
            {
                var logger = new TextLogger(LogSeverity.Warning);
                logger.PutLine("Duplication de données détectée.");
                logger.PutLine(seq.Single().ToString());
                logger.Flush();

                MessageBox.Show("La validation de  données a échouée. " +
                    "Consultez le journal des événements pour plus d’informations." ,
                    null ,
                    MessageBoxButtons.OK ,
                    MessageBoxIcon.Warning);

                return false;
            }

            var profile = new UserProfile(AppContext.TableManager.Products.CreateUniqID() , name , privilege);

            //ajouter gestion des profils
            var mgmntMode = new ProfileManagementMode(profile.ID);

            using (IDatumProvider dp = AppContext.TableManager.ProfileManagementMode.DataProvider)
            {
                dp.Connect();
                dp.Insert(mgmntMode);
            }

            //ajouer le profil
            m_ndxProfiles.Source.Insert(profile);


            return true;
        }

        bool UpdateDatum(string name , HubCore.DLG.ProfilePrivilege_t privilege)
        {
            if (string.Compare(name , m_datum.Name) == 0 && m_datum.Privilege == privilege)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                return true;
            }

            var seq = from UserProfile p in m_ndxProfiles.Source.Enumerate()
                      where string.Compare(p.Name , name , true) == 0 && p.ID != m_datum.ID
                      select p;

            if (seq.Count() > 0)
            {
                var logger = new TextLogger(LogSeverity.Warning);
                logger.PutLine("Duplication de données détectée.");
                logger.PutLine(seq.Single().ToString());
                logger.Flush();

                MessageBox.Show("La validation de  données a échouée. " +
                    "Consultez le journal des événements pour plus d’informations." ,
                    null ,
                    MessageBoxButtons.OK ,
                    MessageBoxIcon.Warning);


                return false;
            }

            int ndx = m_ndxProfiles.IndexOf(m_datum.ID);
            var profile = new UserProfile(m_datum.ID , name , privilege);
            m_ndxProfiles.Source.Replace(ndx , profile);

            return true;
        }


        //handlers:
        private void Profiles_DatumReplaced(IDataRow datum)
        {

            if (datum.ID == m_datum.ID)
            {
                var profile = datum as UserProfile;
                m_tbProfileName.Text = profile.Name;
                SelectPrivilege(profile.Privilege);

                EndDialog();
            }
        }

        private void Profiles_DatumDeleted(IDataRow datum)
        {
            if (datum.ID == m_datum.ID)
                EndDialog();
        }

        private void Save_Click(object sender , EventArgs e)
        {
            string name = m_tbProfileName.GetInputText();

            if (string.IsNullOrWhiteSpace(name))
            {
                string msg = "Nom du profile non-servis. Veuillez compléter le formulaire.";
                MessageBox.Show(msg , null , MessageBoxButtons.OK , MessageBoxIcon.Warning);

                m_tbProfileName.SelectAll();
                return;
            }

            ProfilePrivilege_t privilege = (m_cbPrivilege.SelectedItem as ComboBoxEntry).Privilege;

            if (m_datum != null)
            {
                if (UpdateDatum(name , privilege))
                    Close();
            }
            else if (AddDatum(name , privilege))
            {
                m_tbProfileName.Clear();
                m_cbPrivilege.SelectedIndex = 0;

                m_tbProfileName.Select();
                UpdateUI();
            }

        }

        private void Reload_Click(object sender , EventArgs e) => LoadDatumInfo();

        private void ProfileName_TextChanged(object sender , EventArgs e) =>
            m_tsbSave.Enabled = m_tbProfileName.TextLength > 0;

        private void Settings_Click(object sender , EventArgs e)
        {
            using (var dlg = new Opts.SettingsWizard())
            {
                dlg.ActivePage = Opts.SettingsWizard.SettingPage_t.Input;

                dlg.ShowDialog(Owner);
            }
        }
    }
}
