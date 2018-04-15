using DGD.HubCore.DB;
using DGD.HubGovernor.Countries;
using easyLib.DB;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using easyLib.Extensions;
using DGD.HubGovernor.Extensions;
using easyLib;
using DGD.HubGovernor.Log;

namespace DGD.HubGovernor.Places
{
    sealed partial class PlaceForm: Form
    {
        class CountryListEntry
        {
            public CountryListEntry(Country country)
            {
                Assert(country != null);

                Country = country;
            }

            public Country Country { get; }

            public override string ToString() => Country.Name;
        }


        const string ERR_PROVIDER_MSG = "Enregistrement supprimé. Sélectionnez-en un autre, ou créez-en un nouveau.";
        readonly KeyIndexer m_ndxerPlaces;
        readonly KeyIndexer m_ndxerCountries;
        readonly Place m_datum;
        bool m_dialogEnded;


        public PlaceForm(IDatumProvider dataProvider , IDatum datum = null)
        {
            Assert(dataProvider != null);
            Assert(datum == null || datum is Place);

            InitializeComponent();

            m_ndxerPlaces = new KeyIndexer(dataProvider);


            if (datum != null)
            {
                m_datum = datum as Place;

                var dp = new DatumProvider(AppContext.TableManager.Countries.DataProvider , d =>
                 {
                     var ctry = d as Country;
                     return !ctry.Disabled || ctry.ID == m_datum.CountryID;
                 });

                m_ndxerCountries = new KeyIndexer(dp);

                m_ndxerPlaces.DatumDeleted += PlacesIndexer_DatumChanged;
                m_ndxerPlaces.DatumReplaced += PlacesIndexer_DatumChanged;
                m_tsbReload.Click += delegate { FillForm(); };                
            }
            else
                m_ndxerCountries = new KeyIndexer(AppContext.TableManager.Countries.RowProvider);

            //handlers            
            m_ndxerCountries.DatumDeleted += CountriesIndexer_DatumDeleted;
            m_ndxerCountries.DatumInserted += CountriesIndexer_DatumInserted;
            m_ndxerCountries.DatumReplaced += CountriesIndexer_DatumReplaced;
            //m_tsbAddCountry.Click += AddCountry_Click;
            m_tsbSave.Click += Save_Click;
            m_tbName.TextChanged += delegate { UpdateUI(); };
            m_cbCountries.SelectedIndexChanged += Countries_SelectedIndexChanged;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            ConnectAsync();
            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            m_ndxerCountries.Dispose();
            m_ndxerPlaces.Dispose();
        }


        //private:
        int LocateCountry(uint idCtry)
        {
            Assert(!InvokeRequired);

            for (int i = 0; i < m_cbCountries.Items.Count; ++i)
                if ((m_cbCountries.Items[i] as CountryListEntry).Country.ID == idCtry)
                    return i;

            return -1;
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbAddCountry.Enabled = !m_dialogEnded;
            m_tsbReload.Enabled = !m_dialogEnded && m_datum != null;
            m_tsbSave.Enabled = !m_dialogEnded && m_tbName.TextLength > 0 && m_cbCountries.SelectedIndex >= 0 &&
                m_ndxerPlaces.IsConnected && m_ndxerCountries.IsConnected;
        }

        void FillForm()
        {
            Assert(!InvokeRequired);
            Assert(m_datum != null);

            m_tbName.Text = m_datum.Name;
            m_cbCountries.SelectedIndex = LocateCountry(m_datum.CountryID);
        }

        void ClearForm()
        {
            Assert(!InvokeRequired);

            m_tbName.Clear();
            m_cbCountries.SelectedIndex = -1;
            m_tbName.Select();
        }

        void ConnectAsync()
        {
            Action connect = () =>
            {
                m_ndxerPlaces.Connect();
                m_ndxerCountries.Connect();
            };

            Action<Task> onErr = (t) =>
            {
                UseWaitCursor = false;
                string msg = t.Exception.InnerException.Message;
                MessageBox.Show(msg , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            };

            Action onConnected = () =>
            {
                if (m_ndxerCountries.Source.Count > 0)
                {
                    var countries = from Country ctry in m_ndxerCountries.Source.Enumerate()
                                    select new CountryListEntry(ctry);

                    m_cbCountries.Items.AddRange(countries.ToArray());
                }

                if (m_datum != null)
                    FillForm();
                else
                    m_cbCountries.SelectedIndex = -1;

                UpdateUI();
                UseWaitCursor = false;
            };


            var task = new Task(connect , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.OnSuccess(onConnected);

            UseWaitCursor = true;
            task.Start();
        }

        void CheckSelectedCountry()
        {
            Assert(!InvokeRequired);

            if (m_cbCountries.SelectedIndex >= 0 && (m_cbCountries.SelectedItem as CountryListEntry).Country.Disabled)
                m_errProvider.SetError(m_cbCountries , ERR_PROVIDER_MSG);
            else
                m_errProvider.SetError(m_cbCountries , null);
        }

        void EndDialog()
        {
            Assert(!InvokeRequired);

            m_tsbAddCountry.Enabled = m_tsbReload.Enabled = m_tsbSave.Enabled = false;
            m_tbName.Enabled = m_cbCountries.Enabled = false;

            m_dialogEnded = true;
        }

        void Save()
        {
            Assert(!InvokeRequired);

            //is input ok?
            string name = m_tbName.GetInputText();

            if (string.IsNullOrWhiteSpace(name))
            {
                this.ShowWarning("Champs monétaire mal servi. Veuillez compléter le formulaire.");
                m_tbName.Select();

                return;
            }
                        
            uint idCtry = (m_cbCountries.SelectedItem as CountryListEntry).Country.ID;

            if(m_datum != null && name == m_datum.Name && idCtry == m_datum.CountryID)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();

                return;
            }


            /*
             *  - selon containte 4 (name, idCtry) est unique
             *  
             **/

            //any duplicate?           
            Predicate<IDatum> filter = d =>
            {
                var place = d as Place;
                return string.Compare(place.Name , name , true) == 0 && place.CountryID == idCtry;
            };

            bool duplicate = true;

            using (var dp = new DatumProvider(m_ndxerPlaces.Source , filter))
            using (new AutoReleaser(() => UseWaitCursor = false))
            {
                UseWaitCursor = true;
                dp.Connect();

                if (dp.Count == 0 || (dp.Count == 1 && m_datum != null && m_datum.ID != (dp.Get(0) as Place).ID))
                    duplicate = false;
                else
                {
                    var place = dp.Get(0) as Place;

                    var logger = new TextLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");
                    logger.Put("ID: {0}\n" , place.ID);
                    logger.Put("Lieu: {0}\n" , place.Name);
                    logger.Put("Pays: {0}" , (m_cbCountries.SelectedItem as CountryListEntry).Country );
                    logger.Flush();
                }
            }


            if (duplicate)
                this.ShowWarning("La validation de  données a échouée. " +
                "Consultez le journal des événements pour plus d’informations.");
            else
            {
                uint id = (m_datum?.ID) ?? AppContext.TableManager.Places.CreateUniqID();
                var place = new Place(id , name , idCtry);

                if (m_datum == null)
                {
                    m_ndxerPlaces.Source.Insert(place);
                    ClearForm();
                }
                else
                {
                    int ndx = m_ndxerPlaces.IndexOf(m_datum.ID);
                    m_ndxerPlaces.Source.Replace(ndx , place);

                    Close();
                }

                TextLogger.Info("Enregistrement réussi.");
            }
        }
                
        //handlers
        private void CountriesIndexer_DatumReplaced(IDataRow row)
        {
            /*
             *  - remplacement (ou désactivation) du non-dernier datum
             *          - DatumReplacing
             *          - DatumDeleting
             *          - DatumInserted
             *  - remplacement du dernier datum
             *          - DatumReplacing
             *          - DatumReplaced
             *  - cette event ne peut etre cause que par le remplacement du dernier datum
             *  - a cause du filtre utilse dans le m_srcCountries row.Disabled vrai =>
             *      m_datum.CountryID = row.ID
             * */


            Action replaceCtry = () =>
            {
                int ndx = LocateCountry(row.ID);
                int ndxSel = m_cbCountries.SelectedIndex;

                if ((row as Country).Disabled)
                {
                    Assert(m_datum != null && m_datum.CountryID == (row as Country).ID);

                    if (ndx == ndxSel)
                        m_cbCountries.SelectedIndex = 0;

                    m_cbCountries.Items.RemoveAt(ndx);
                }
                else
                {
                    m_cbCountries.Items[ndx] = new CountryListEntry(row as Country);

                    if (ndx == ndxSel)
                        CheckSelectedCountry();
                }
            };



            if (InvokeRequired)
                BeginInvoke(replaceCtry);
            else
                replaceCtry();
        }

        private void CountriesIndexer_DatumInserted(IDataRow row)
        {
            /*
             *  - cette notification peut naitre suite a:
             *      - un remplacement (ou desactivation) d'un non-dernier datum:
             *              - DatumReplacing
             *              - DatumDeleted
             *              - DatumInserted
             *      - une insertion
             *      - filtre de m_srcCountries => tjrs row.Disabled != false ou 
             *          m_Datum.CountryID = row.ID
             **/


            if (InvokeRequired)
                BeginInvoke(new Action(() => m_cbCountries.Items.Add(new CountryListEntry(row as Country))));
            else
                m_cbCountries.Items.Add(new CountryListEntry(row as Country));
        }
    
        private void CountriesIndexer_DatumDeleted(IDataRow row)
        {
            /*
             *  - notification ayant pour source:
             *      - suppression => suite une tache de maintenance:
             *          - un datum desactivé ne peut etre supprimer si il est utilsé =>
             *              row est orphelin => si row.Disbaled est vrai alors row peut etre ignoré
             *      - remplacement (désactivation) d'un non-dernier datum:
             *          - DatumReplaced
             *          - DatumDeleted => row.Disabled ne peut etre vrai
             *          - DatumInserted
             * */

            if ((row as Country).Disabled)
                return;


            Action deleteCtry = () =>
            {
                int ndx = LocateCountry(row.ID);

                if (m_cbCountries.SelectedIndex == ndx)
                    m_cbCountries.SelectedIndex = 0;

                m_cbCountries.Items.RemoveAt(ndx);
            };


            if (InvokeRequired)
                BeginInvoke(deleteCtry);
            else
                deleteCtry();
        }

        private void Countries_SelectedIndexChanged(object sender , EventArgs e)
        {
            CheckSelectedCountry();
            UpdateUI();
        }

        private void PlacesIndexer_DatumChanged(IDataRow row)
        {
            if (row.ID == m_datum.ID)
                if (InvokeRequired)
                    BeginInvoke(new Action(EndDialog));
                else
                    EndDialog();

        }

        private void Save_Click(object sender , EventArgs e)
        {
            try
            {
                Save();
            }
            catch(Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void AddCountry_Click(object sender , EventArgs e)
        {
            try
            {
                var dataProvider = AppContext.TableManager.Countries.RowProvider;
                var form = new CountryForm(dataProvider);

                form.Show(Owner);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }
    }
}
