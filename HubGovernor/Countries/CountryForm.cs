using DGD.HubCore.DB;
using easyLib;
using easyLib.DB;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using easyLib.Extensions;
using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.Log;

namespace DGD.HubGovernor.Countries
{
    sealed partial class CountryForm: Form
    {
        readonly KeyIndexer m_ndxerCountries;
        Country m_datum;
        bool m_dialogEnded;


        public CountryForm(IDatumProvider countryProvider , IDatum datum = null)
        {
            Assert(countryProvider != null);
            Assert(datum == null || datum is Country);

            InitializeComponent();

            m_nudInternalCode.Maximum = CountryRow.MAX_INTERNAL_CODE;
            m_ndxerCountries = new KeyIndexer(countryProvider);            


            if (datum != null)
            {
                m_datum = datum as Country;

                m_ndxerCountries.DatumDeleted += Countries_DatumChanged;
                m_ndxerCountries.DatumReplaced += Countries_DatumChanged;

                m_tsbReload.Click += delegate { ShowDatum(); };
            }


            //handlers
            EventHandler txtChanged = delegate
            {
                UpdateUI();
            };


            m_tbName.TextChanged += txtChanged;
            m_nudInternalCode.TextChanged += txtChanged;
            m_tsbSave.Click += delegate { SaveDatum(); };
        }


        //protecetd:
        protected override void OnLoad(EventArgs e)
        {
            if (m_datum != null)
                ShowDatum();

            ConnectIndexerAsync();

            UpdateUI();
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                base.OnFormClosing(e);
                m_ndxerCountries.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }


        //private:
        void ConnectIndexerAsync()
        {
            Action<Task> onError = t =>
            {
                m_dialogEnded = true;
                Exception ex = t.Exception.InnerException;
                MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            };

         
            var connecting = new Task(m_ndxerCountries.Connect , TaskCreationOptions.LongRunning);            
            connecting.OnError(onError);
            connecting.OnSuccess(UpdateUI);


            connecting.Start();
        }

        void EndDialog()
        {
            Assert(!InvokeRequired);

            m_dialogEnded = true;
            m_nudInternalCode.Enabled = m_tbIsoCode.Enabled = m_tbName.Enabled = false;
            UpdateUI();
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbSave.Enabled = !m_dialogEnded &&
                    m_tbName.TextLength > 0 &&
                    m_nudInternalCode.Value > 0 &&
                    m_ndxerCountries.IsConnected;


            m_tsbReload.Enabled = !m_dialogEnded && m_datum != null;
        }

        void ShowDatum()
        {
            Assert(!InvokeRequired);

            m_nudInternalCode.Value = m_datum.InternalCode;
            m_tbIsoCode.Text = m_datum.IsoCode;
            m_tbName.Text = m_datum.Name;
        }

        void SaveDatum()
        {
            Assert(!InvokeRequired);

            /* - check that fields are filled
             * - in edit mode:
             *      - if no change then exit (case sensitive)
             *  - iterate through rows and check that:
             *      - Name, Code are uniq
             *      - if isoCode is set then it must be uniq
             *   - if no duplicate then save
             *   - otherwise inform
             * */

            string name = m_tbName.GetInputText();
            var code = (ushort)m_nudInternalCode.Value;
            string isoCode = m_tbIsoCode.GetInputText();

            //input is ok?
            if (string.IsNullOrWhiteSpace(name) || code == 0)
            {
                string msg = "Certains champs sont mal servis. Veuillez compléter le formulaire.";
                MessageBox.Show(msg , null , MessageBoxButtons.OK , MessageBoxIcon.Warning);

                if (string.IsNullOrWhiteSpace(name))
                    m_tbName.SelectAll();
                else
                    m_nudInternalCode.Select();

                return;
            }


            //are there any modif?
            if(m_datum != null && name == m_datum.Name && code == m_datum.InternalCode && 
                isoCode == m_datum.IsoCode)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");

                Close();
                return;
            }
                
            

            Predicate<IDatum> filter = d =>
            {
                var row = d as Country;

                return (row.InternalCode == code ||
                    string.Compare(name , row.Name , true) == 0 ||
                    (isoCode != "" && string.Compare(isoCode , row.IsoCode , true) == 0) ) &&
                    (m_datum == null || m_datum.ID != row.ID);
            };



            bool duplicate = true;

            using (new AutoReleaser(() => UseWaitCursor = false))
            using (var dp = new DatumProvider(m_ndxerCountries.Source , filter))
            {
                UseWaitCursor = true;
                dp.Connect();

                //any dupliacte data?
                if(dp.Count == 0)
                {
                    uint id = m_datum == null? AppContext.TableManager.Countries.CreateUniqID() : m_datum.ID;
                    var ctry = new Country(id , name , code , isoCode);

                    if (m_datum == null)
                    {
                        AppContext.LogManager.LogUserActivity($"Action utilisateur :  Ajout d’un pays: {ctry}");
                        m_ndxerCountries.Source.Insert(ctry);                        

                        m_tbIsoCode.Clear();
                        m_tbName.Clear();
                        m_nudInternalCode.Value = 0;
                        m_tbName.Select();
                    }
                    else
                    {
                        AppContext.LogManager.LogUserActivity("Action utilisateur :  Remplacement d’un pays: " + 
                            $"ancienne valeur: {m_datum}, nouvelle vaeur: {ctry}");

                        int ndx = m_ndxerCountries.IndexOf(id);
                        m_ndxerCountries.Source.Replace(ndx , ctry);
                        Close();                        
                    }

                    duplicate = false;
                }
                
                
                if (duplicate)
                {
                    var logger = new TextLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");

                    foreach (Country ctry in dp.Enumerate())
                    {
                        logger.Put("ID: {0}\n" , ctry.ID);
                        logger.Put("Pays: {0}\n" , ctry.Name);
                        logger.Put("Code: {0}\n" , ctry.InternalCode);
                        logger.Put("Code ISO: {0}" , ctry.IsoCode);
                    }

                    logger.Flush();
                }
            }


            if (duplicate)
                MessageBox.Show("La validation de  données a échouée. " +
                    "Consultez le journal des événements pour plus d’informations." ,
                    null ,
                    MessageBoxButtons.OK ,
                    MessageBoxIcon.Warning);
            else
                TextLogger.Info("Enregistrement réussi.");            
        }
        
        //handlers
        private void Countries_DatumChanged(IDataRow row)
        {
            if (row.ID == m_datum.ID)
                if (InvokeRequired)
                    BeginInvoke(new Action(EndDialog));
                else
                    EndDialog();
        }
    }
}
