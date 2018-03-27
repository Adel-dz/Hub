using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using easyLib.DB;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


/*
 * - les données sont inserees automatiquement par l'app => pas d'addition ni de suppression.
 * - la modification est permise pour des raisons de maintenance (cas ou une source change
 *      de nom).
 * */



namespace DGD.HubGovernor.Suppliers
{
    sealed partial class DataSuppliersForm: Form
    {
        readonly KeyIndexer m_ndxerSuppliers;
        readonly DataSupplier m_datum;
        bool m_frozen;


        public DataSuppliersForm(IDatumProvider srcSupplier, IDatum datum)
        {
            Assert(srcSupplier != null);
            Assert(datum != null);
            Assert(datum is DataSupplier);

            InitializeComponent();

            m_datum = datum as DataSupplier;
            m_ndxerSuppliers = new KeyIndexer(AppContext.TableManager.Suppiers.DataProvider);

            //handlers
            m_ndxerSuppliers.DatumDeleted += SuppliersIndexer_DatumChanged;
            m_ndxerSuppliers.DatumReplaced += SuppliersIndexer_DatumChanged;
            m_tbName.TextChanged += delegate { UpdateUI(); };
            m_tsbReload.Click += delegate { FillForm(); };
            m_tsbSave.Click += Save_Click;

            FillForm();
            ConnectAsync();
        }
        

        //protected:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_ndxerSuppliers.Close();
        }


        //private:
        void FillForm()
        {
            Assert(!InvokeRequired);

            m_tbName.Text = m_datum.Name;
            Update();
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbReload.Enabled = !m_frozen;
            m_tsbSave.Enabled = !m_frozen && m_tbName.TextLength > 0 && m_ndxerSuppliers.IsConnected;
        }

        void ConnectAsync()
        {
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

            var task = new Task(m_ndxerSuppliers.Connect);
            task.OnError(onErr);
            task.OnSuccess(onConnected);

            task.Start();
        }

        void Freeze()
        {
            Assert(!InvokeRequired);

            m_tbName.Enabled = false;
            m_frozen = true;
        }

        void Save()
        {
            Assert(!InvokeRequired);

            string name = m_tbName.GetInputText();

            //is input ok
            if(string.IsNullOrWhiteSpace(name))
            {
                this.ShowWarning("Nom de source mal servis. Veuillez compléter le formulaire.");
                m_tbName.Select();

                return;
            }


            //any modifs?
            if(m_datum.Name == name)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();
                return;
            }


            //any dupliacte?
            var rows = m_ndxerSuppliers.Source.Count == 0 ? Enumerable.Empty<DataSupplier>() :
                from ds in m_ndxerSuppliers.Source.Enumerate().Cast<DataSupplier>()
                where string.Compare(ds.Name ,name) == 0 && ds.ID != m_datum.ID
                select ds;

            if(rows.Any())
            {
                DataSupplier supplier = rows.First();

                var logger = new TextLogger(LogSeverity.Warning);
                logger.Put("Duplication de données détectée.");
                logger.Put("Elément trouvé:\n");
                logger.Put("ID: {0}\n" , supplier.ID);
                logger.Put("Source: {0}" , supplier.Name);
                logger.Flush();

                this.ShowWarning("La validation de  données a échouée. " +
                    "Consultez le journal des événements pour plus d’informations.");
            }
            else
            {
                var supplier = new DataSupplier(m_datum.ID , name);
                int ndx = m_ndxerSuppliers.IndexOf(m_datum.ID);

                m_ndxerSuppliers.Source.Replace(ndx , supplier);
                Close();

                TextLogger.Info("Enregistrement réussi.");
            }
        }
        
        //handlers:
        private void SuppliersIndexer_DatumChanged(IDataRow datum)
        {
            if (datum.ID == m_datum.ID)
                if (InvokeRequired)
                    BeginInvoke(new Action(Freeze));
                else
                    Freeze();
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

    }
}
