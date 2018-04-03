using DGD.HubCore.DB;
using DGD.HubGovernor.Countries;
using easyLib.DB;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using easyLib.Extensions;
using DGD.HubGovernor.Extensions;
using easyLib;
using System.Collections.Generic;
using DGD.HubGovernor.Log;

namespace DGD.HubGovernor.Currencies
{
    sealed partial class CurrencyForm: Form
    {
        class CountryListEntry
        {
            public CountryListEntry(Country ctry)
            {
                Country = ctry;
            }


            public Country Country { get; }
            public override string ToString() => Country.Name;
        }


        const string ERR_PROVIDER_MSG = "Enregistrement supprimé. Sélectionnez-en un autre, ou créez-en un nouveau.";
        readonly KeyIndexer m_ndxerCurrencies;
        readonly KeyIndexer m_ndxerCountries;
        readonly Currency m_datum;
        bool m_dialogEnded;


        public CurrencyForm(IDatumProvider currencyProvider, IDatum datum = null)
        {
            Assert(currencyProvider != null);
            Assert(datum == null || datum is Currency);


            InitializeComponent();

            m_ndxerCurrencies = new KeyIndexer(currencyProvider);

            Predicate<IDatum> filter;

            if(datum != null)
            {
                m_datum = datum as Currency;

                filter = d =>
                {
                    var ctry = d as Country;
                    return !ctry.Disabled || ctry.ID == m_datum.CountryID;
                };

                m_ndxerCurrencies.DatumDeleted += CurrenciesIndexer_DatumChanged;
                m_ndxerCurrencies.DatumReplaced += CurrenciesIndexer_DatumChanged;
                m_tsbReload.Click += delegate { FillForm(); };
                m_cbCountries.SelectedIndexChanged += Countries_SelectedIndexChanged;
            }
            else
                filter = ctry => !(ctry as Country).Disabled;


            var dp = new DatumProvider(AppContext.TableManager.Countries.DataProvider , filter);
            m_ndxerCountries = new KeyIndexer(dp);

            //handlers
            m_ndxerCountries.DatumDeleted += CountriesIndexer_DatumDeleted;
            m_ndxerCountries.DatumInserted += CountriesIndexer_DatumInserted;
            m_ndxerCountries.DatumReplaced += CountriesIndexer_DatumReplaced;
            m_tsbAddCountry.Click += AddCountry_Click;
            m_tsbSave.Click += Save_Click;
            m_tbName.TextChanged += delegate { UpdateUI(); };
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            LoadDataAsync();
            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            m_ndxerCountries.Close();
            m_ndxerCurrencies.Dispose();
        }


        //private:
        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbAddCountry.Enabled = !m_dialogEnded;
            m_tsbReload.Enabled = !m_dialogEnded && m_datum != null;
            m_tsbSave.Enabled = !m_dialogEnded && m_tbName.TextLength > 0 &&
                m_ndxerCurrencies.IsConnected && m_ndxerCountries.IsConnected;
        }

        void FillForm()
        {
            Assert(!InvokeRequired);
            Assert(m_datum != null);

            m_tbDescription.Text = m_datum.Description;
            m_tbName.Text = m_datum.Name;

            if (m_datum.CountryID == 0)
                m_cbCountries.SelectedIndex = 0;
            else
                foreach (CountryListEntry entry in m_cbCountries.Items.AsEnumerable<object>().Skip(1))
                    if (entry.Country.ID == m_datum.CountryID)
                    {
                        m_cbCountries.SelectedItem = entry;

                        if (entry.Country.Disabled)
                            m_errorProvider.SetError(m_cbCountries , ERR_PROVIDER_MSG);
                        else
                            m_errorProvider.SetError(m_cbCountries , null);

                        break;
                    }
        }
        
        void ClearForm()
        {
            Assert(!InvokeRequired);

            m_tbDescription.Clear();
            m_tbName.Clear();
            m_cbCountries.SelectedIndex = 0;
            m_errorProvider.SetError(m_cbCountries , null);
            m_tbName.Select();
        }       
                
        void LoadDataAsync()
        {
            Action<Task> onErr = t =>
            {
                string msg = t.Exception.InnerException.Message;
                AppContext.LogManager.LogSysError($"Lecture de la table des monnaies: {msg}");
                
                UseWaitCursor = false;
                MessageBox.Show(msg , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            };


            Action onConnected = () =>
            {
                m_cbCountries.Items.Add("");
                
                if (m_ndxerCountries.Source.Count > 0)
                {
                    IEnumerable<CountryListEntry> countries = from Country ctry in m_ndxerCountries.Source.Enumerate()
                                                              select new CountryListEntry(ctry);
        
                    m_cbCountries.Items.AddRange(countries.ToArray());

                    Assert(m_cbCountries.Items[0].ToString() == "");
                }

                if (m_datum != null)
                    FillForm();
                else
                    m_cbCountries.SelectedIndex = 0;

                UpdateUI();
                UseWaitCursor = false;
            };


            Action connect = () =>
            {
                m_ndxerCurrencies.Connect();
                m_ndxerCountries.Connect();
            };



            var task = new Task(connect , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.OnSuccess(onConnected);

            UseWaitCursor = true;
            task.Start();
        }

        void Save()
        {
            Assert(!InvokeRequired);


            /*
             * - Name required and not uniq
             * - Description and CountryID not required and both not uniq
             * */

            string name = m_tbName.GetInputText();

            //is input ok?
            if (string.IsNullOrWhiteSpace(name))
            {
                this.ShowWarning("Champs monétaire mal servi. Veuillez compléter le formulaire.");
                m_tbName.Select();

                return;
            }


            string descr = m_tbDescription.GetInputText();
            uint idCtry = m_cbCountries.SelectedIndex == 0 ? 0 : (m_cbCountries.SelectedItem as CountryListEntry).Country.ID;

            //any modif?
            if (m_datum != null && name == m_datum.Name && m_datum.Description == descr && idCtry == m_datum.CountryID)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();

                return;
            }


            /*
             * - si idCtry == 0 => name doit etre unique
             * - sinon (name, idCtry) doit etre unique
             *  
             *  #
             *  - checher name -> rows
             *  - si rows.Count = 0 => ok
             *  - si rows.Count = 1 et m_datum != null => ok
             *  - sinon
             *      - si idCtry = 0 => erreur
             *      - sinon 
             *          - pour tout r dans rows
             *              - si r.CountryID = 0 ou r.CountryID == idCtry => erreur
             **/


            //any duplicate?
            bool duplicate = true;

            using (var ndxerNames = new AttrIndexer<string>(m_ndxerCurrencies.Source , d => (d as Currency).Name.ToUpper()))
            using (new AutoReleaser(() => UseWaitCursor = false))
            {
                UseWaitCursor = true;
                ndxerNames.Connect();
                Currency[] rows = ndxerNames.Get(name.ToUpper()).Cast<Currency>().ToArray();

                if (rows.Length == 0 || (rows.Length == 1 && m_datum != null && m_datum.ID == rows[0].ID))                
                    duplicate = false;
                else
                { 
                    var cncy = rows.First();

                    var logger = new TextLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");
                    logger.Put("ID: {0}\n" , cncy.ID);
                    logger.Put("Monnaie: {0}\n" , cncy.Name);
                    logger.Put("Pays ID: {0}\n" , cncy.CountryID);
                    logger.Put("Description ISO: {0}" , cncy.Description);
                    logger.Flush();
                }
            }


            if (duplicate)
                this.ShowWarning("La validation de  données a échouée. " +
                "Consultez le journal des événements pour plus d’informations.");
            else
            {
                uint id = (m_datum?.ID) ?? AppContext.TableManager.Currencies.CreateUniqID();
                var cncy = new Currency(id , name , idCtry , descr);

                if (m_datum == null)
                {
                    AppContext.LogManager.LogUserActivity($"Action utilsateur: Ajout d'une monnaie: {cncy}");
                    m_ndxerCurrencies.Source.Insert(cncy);
                    ClearForm();
                }
                else
                {
                    AppContext.LogManager.LogUserActivity("Action utilsateur: Remplacement d'une monnaie: " +
                        $"ancienne valeur: {m_datum}, nouvelle valeur: {cncy}");

                    int ndx = m_ndxerCurrencies.IndexOf(m_datum.ID);
                    m_ndxerCurrencies.Source.Replace(ndx , cncy);

                    Close();
                }

                TextLogger.Info("Enregistrement réussi.");
            }

        }

        void EndDialog()
        {
            Assert(!InvokeRequired);

            m_tsbAddCountry.Enabled = m_tsbReload.Enabled = m_tsbSave.Enabled = false;
            m_tbDescription.Enabled = m_tbName.Enabled = m_cbCountries.Enabled = false;

            m_dialogEnded = true;
        }

        void CheckSelectedCountry()
        {
            Assert(!InvokeRequired);

            if (m_cbCountries.SelectedIndex > 0 && (m_cbCountries.SelectedItem as CountryListEntry).Country.Disabled)
                m_errorProvider.SetError(m_cbCountries , ERR_PROVIDER_MSG);
            else
                m_errorProvider.SetError(m_cbCountries , null);                            
        }
        
        //handlers
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

        private void CurrenciesIndexer_DatumChanged(IDataRow row)
        {
            if (row.ID == m_datum.ID)
                if (InvokeRequired)
                    BeginInvoke(new Action(EndDialog));
                else
                    EndDialog();
        }

        private void CountriesIndexer_DatumReplaced(IDataRow row)
        {
            /*
             *  - cette event peut etre cause par:
             *      - remplacement (ou désactivation) du non-dernier datum
             *          - DatumReplacing
             *          - DatumDeleting -> Countries_DatumDeleted
             *          - DatumInserted -> Countries_DatumInserted
             *      - remplacement du dernier datum
             *          - DatumReplacing
             *          - DatumReplaced -> ici => row.Disabled peut etre vrai =>
             *              - si row.Disabled est vrai :
             *                  - si en mode non-edit ou m_datum.CountrID != row.ID supprimer l'elt
             *                  - sinon signaler l'erreur si le pays sélectionné est row
             *                  
             * */



            Action replaceCtry = () =>
            {                
                int ndx = 1;

                for (; ; ++ndx)
                    if ((m_cbCountries.Items[ndx] as CountryListEntry).Country.ID == row.ID)
                        break;

                int ndxSel = m_cbCountries.SelectedIndex;

                if ((row as Country).Disabled && (m_datum == null || row.ID != m_datum.CountryID))
                {
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
                for(int i = 1;i < m_cbCountries.Items.Count; ++i)
                    if((m_cbCountries.Items[i] as CountryListEntry).Country.ID == row.ID)
                    {
                        if (m_cbCountries.SelectedIndex == i)
                            m_cbCountries.SelectedIndex = 0;

                        m_cbCountries.Items.RemoveAt(i);
                        break;
                    }                
            };


            if (InvokeRequired)
                BeginInvoke(deleteCtry);
            else
                deleteCtry();
        }

        private void Countries_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (m_datum != null)
                CheckSelectedCountry();
        }

    }
}
