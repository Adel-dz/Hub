using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.Log;
using easyLib;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Units
{
    sealed partial class UnitForm: Form
    {
        readonly KeyIndexer m_ndxerUnits;
        readonly Unit m_datum;
        bool m_frozen;



        public UnitForm(IDatumProvider srcUnits , IDatum datum = null)
        {
            Assert(srcUnits != null);
            Assert(datum == null || datum is Unit);

            InitializeComponent();

            m_ndxerUnits = new KeyIndexer(srcUnits);

            if (datum != null)
            {
                m_datum = datum as Unit;
                m_tsbReload.Click += delegate { FillForm(); };

                m_ndxerUnits.DatumDeleted += UnitsIndexer_DatumChanged;
                m_ndxerUnits.DatumReplaced += UnitsIndexer_DatumChanged;

                FillForm();
            }

            m_tbName.TextChanged += delegate { UpdateUI(); };
            m_tsbSave.Click += Save_Click;

            ConnectAsync();
        }


        //protected:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_ndxerUnits.Close();
        }


        //private:
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
                UseWaitCursor = false;
                UpdateUI();
            };

            UseWaitCursor = true;

            var task = new Task(m_ndxerUnits.Connect , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.OnSuccess(onConnected);
            task.Start();
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbReload.Enabled = !m_frozen && m_datum != null;
            m_tsbSave.Enabled = !m_frozen && m_tbName.TextLength > 0 && m_ndxerUnits.IsConnected;
        }

        void FillForm()
        {
            Assert(!InvokeRequired);
            Assert(m_datum != null);

            m_tbDescription.Text = m_datum.Description;
            m_tbName.Text = m_datum.Name;
            UpdateUI();
        }

        void Freeze()
        {
            Assert(!InvokeRequired);

            m_frozen = true;
            m_tbName.Enabled = m_tbDescription.Enabled = false;

            UpdateUI();
        }

        void ClearForm()
        {
            Assert(!InvokeRequired);

            m_tbDescription.Clear();
            m_tbName.Clear();
            m_tbName.Select();
        }

        void Save()
        {
            Assert(!InvokeRequired);

            string name = m_tbName.GetInputText();

            //is input ok?
            if (string.IsNullOrWhiteSpace(name))
            {
                this.ShowWarning("Unité mal servie. Veuillez compléter le formulaire.");
                m_tbName.Select();

                return;
            }

            string descr = m_tbDescription.GetInputText();


            //any modif?
            if (m_datum != null && name == m_datum.Name && descr == m_datum.Description)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();
                return;
            }



            //any duplicate?
            using (new AutoReleaser(() => UseWaitCursor = false))
            {
                UseWaitCursor = true;

                var seq = m_ndxerUnits.Source.Count == 0 ? Enumerable.Empty<Unit>() :
                from u in m_ndxerUnits.Source.Enumerate().Cast<Unit>()
                where string.Compare(u.Name , name , true) == 0 &&
                    (m_datum == null || m_datum.ID != u.ID)
                select u;

                if (seq.Any())
                {
                    Unit unit = seq.First();

                    var logger = new TextLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");
                    logger.Put("ID: {0}\n" , unit.ID);
                    logger.Put("Unité: {0}\n" , unit.Name);
                    logger.Put("Description: {0}" , unit.Description);
                    logger.Flush();

                    this.ShowWarning("La validation de  données a échouée. " +
                        "Consultez le journal des événements pour plus d’informations.");
                }
                else
                {
                    uint id = (m_datum?.ID) ?? AppContext.TableManager.Units.CreateUniqID();
                    var unit = new Unit(id , name , descr);

                    if (m_datum != null)
                    {
                        int ndx = m_ndxerUnits.IndexOf(id);
                        m_ndxerUnits.Source.Replace(ndx , unit);

                        Close();
                    }
                    else
                    {
                        m_ndxerUnits.Source.Insert(unit);
                        ClearForm();
                    }

                    TextLogger.Info("Enregistrement réussi.");
                }
            }
        }

        //handlers:
        private void UnitsIndexer_DatumChanged(IDataRow datum)
        {
            if (m_datum.ID == datum.ID)
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

