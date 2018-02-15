using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using easyLib;
using easyLib.DB;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Products
{
    sealed partial class ProductForm: Form
    {
        readonly Product m_datum;
        readonly KeyIndexer m_ndxerProducts;
        bool m_frozen;
        

        public ProductForm(IDatumProvider srcProduct, IDatum datum = null)
        {
            Assert(srcProduct != null);
            Assert(datum == null || datum is Product);

            InitializeComponent();

            m_ndxerProducts = new KeyIndexer(srcProduct);

            if(datum != null)
            {
                m_datum = datum as Product;
                FillForm();
                m_tsbReload.Click += delegate { FillForm(); };
            }

            //handlers
            m_tsbSave.Click += Save_Click;
            m_ndxerProducts.DatumDeleted += ProductsIndexer_DatumChanged;
            m_ndxerProducts.DatumReplaced += ProductsIndexer_DatumChanged;

            EventHandler txtChangedHandler = delegate { UpdateUI(); };
            m_tbSubHeading.TextChanged += txtChangedHandler;
            m_tbName.TextChanged+= txtChangedHandler;

            ConnectAsync();
        }


        //protected:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_ndxerProducts.Close();
        }


        //private:
        void FillForm()
        {
            Assert(!InvokeRequired);
            Assert(m_datum != null);

            m_tbSubHeading.Text = m_datum.SubHeading.ToString();
            m_tbName.Text = m_datum.Name;            
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbReload.Enabled = !m_frozen && m_datum != null;
            m_tsbSave.Enabled = !m_frozen && m_tbName.TextLength > 0 && 
                m_tbSubHeading.TextLength > 0 && m_ndxerProducts.IsConnected;
        }

        void ConnectAsync()
        {
            Assert(!InvokeRequired);


            Action<Task> onErr = t =>
            {
                UseWaitCursor = false;
                this.ShowError(t.Exception.InnerException.Message);
            };

            Action onConnected = () =>
            {
                UpdateUI();
                UseWaitCursor = false;
            };

            UseWaitCursor = true;
            var task = new Task(m_ndxerProducts.Connect , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.OnSuccess(onConnected);

            task.Start();
        }

        void ClearForm()
        {
            Assert(!InvokeRequired);

            m_tbName.Clear();
            m_tbSubHeading.Clear();
            m_tbName.Select();
        }

        void Save()
        {
            Assert(!InvokeRequired);

            string name = m_tbName.GetInputText();
            SubHeading subHeading = SubHeading.Parse(m_tbSubHeading.GetInputText());

            //is input ok?
            if(string.IsNullOrWhiteSpace(name) || subHeading == null)
            {
                this.ShowWarning("Champs mal servis. Veuillez compléter le formulaire.");

                if (string.IsNullOrWhiteSpace(name))
                    m_tbName.SelectAll();
                else
                    m_tbSubHeading.Select();

                return;
            }

            //any modif.?
            if(m_datum != null && name == m_datum.Name && subHeading.Value == m_datum.SubHeading.Value)
            {
                EventLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();
                return;
            }


            //any duplicates?
            Predicate<IDatum> filter = d =>
            {
                var prod = d as Product;
                return prod.SubHeading.Value == subHeading.Value && 
                    string.Compare(name , prod.Name , true) == 0 &&
                    (m_datum == null || m_datum.ID != prod.ID);
            };

            bool duplicate = true;
            using (var dp = new DatumProvider(m_ndxerProducts.Source , filter))
            using (new AutoReleaser(() => UseWaitCursor = false))
            {
                UseWaitCursor = true;

                dp.Connect();

                if (dp.Count == 0)
                    duplicate = false;
                else
                {
                    Product prod = dp.Get(0) as Product;

                    var logger = new EventLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");
                    logger.Put("ID: {0}\n" , prod.ID);
                    logger.Put("Pays: {0}\n" , prod.Name);
                    logger.Put("Code: {0}" , prod.SubHeading);
                    logger.Flush();
                }
            }

            if(duplicate)
                this.ShowWarning("La validation de  données a échouée. " + 
                     "Consultez le journal des événements pour plus d’informations.");
            else
            {
                uint id = (m_datum?.ID) ?? AppContext.TableManager.Products.CreateUniqID();
                var prod = new Product(id , name , subHeading);

                if (m_datum == null)
                {
                    m_ndxerProducts.Source.Insert(prod);
                    ClearForm();
                }
                else
                {
                    int ndx = m_ndxerProducts.IndexOf(m_datum.ID);
                    m_ndxerProducts.Source.Replace(ndx , prod);

                    Close();
                }

                EventLogger.Info("Enregistrement réussi.");
            }
        }

        void Freeze()
        {
            Assert(!InvokeRequired);

            m_tbName.Enabled = m_tbSubHeading.Enabled = false;
            m_frozen = true;

            UpdateUI();
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

        private void ProductsIndexer_DatumChanged(IDataRow row)
        {
            if (m_datum.ID == row.ID)
                if (InvokeRequired)
                    BeginInvoke(new Action(Freeze));
                else
                    Freeze();
        }
    }
}
