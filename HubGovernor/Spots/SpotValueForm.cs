using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.Log;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Spots
{
    sealed partial class SpotValueForm: Form
    {
        readonly SpotValue m_spotValue;
        readonly KeyIndexer m_ndxerValues;
        bool m_dialogEnded;


        public SpotValueForm(IDatumProvider srcValues , IDatum datum)
        {
            Assert(datum is SpotValue);
            Assert(srcValues != null);

            InitializeComponent();

            m_nudPrice.Maximum = decimal.MaxValue;

            m_ndxerValues = new KeyIndexer(srcValues);
            m_spotValue = datum as SpotValue;

            m_nudPrice.DecimalPlaces = AppContext.Settings.AppSettings.PriceDecimalPlaces;

            ConnectAsync();            

            m_nudPrice.Value = (decimal)m_spotValue.Price;
            m_dtpSpotTime.Value = m_spotValue.SpotTime;

            m_nudPrice.TextChanged += delegate { UpdateUI(); };
            m_tsbReload.Click += delegate
            {
                m_nudPrice.Value = (decimal)m_spotValue.Price + 1;  //si m_nudPrice.Value == m_spotValue.Price l'affichage
                m_nudPrice.Value = (decimal)m_spotValue.Price;      // n'est pas mis a jour
                m_dtpSpotTime.Value = m_spotValue.SpotTime;
            };

            m_tsbSave.Click += delegate { Save(); };

            m_ndxerValues.DatumDeleted += FreezeDialog;
            m_ndxerValues.DatumReplaced += FreezeDialog;            
        }


        //protected:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_ndxerValues.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            UpdateUI();

            base.OnLoad(e);
        }


        //private:
        void FreezeDialog(IDataRow datum)
        {
            Assert(!InvokeRequired);

            if (datum.ID == m_spotValue.ID)
            {
                m_dialogEnded = true;
                m_nudPrice.Enabled = m_dtpSpotTime.Enabled = false;
                UpdateUI();
            }
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbReload.Enabled = !m_dialogEnded;
            m_tsbSave.Enabled = !m_dialogEnded && !string.IsNullOrEmpty(m_nudPrice.Text);
        }

        void ConnectAsync()
        {
            Assert(!InvokeRequired);

            var waitBox = new Waits.WaitClue(this);

            Action<Task> onErr = t =>
            {
                waitBox.LeaveWaitMode();
                this.ShowError(t.Exception.InnerException.Message);
                Close();
            };


            var task = new Task(m_ndxerValues.Connect);
            task.OnError(onErr);
            task.OnSuccess(waitBox.LeaveWaitMode);

            task.Start();
            waitBox.EnterWaitMode();
        }

        void Save()
        {
            Assert(!InvokeRequired);

            var price = (double)m_nudPrice.Value;
            DateTime dtSpot = m_dtpSpotTime.Value;

            if(m_spotValue.Price == price && m_spotValue.SpotTime == dtSpot)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");
                Close();

                return;
            }


            //verification contrainte 8
            using (var ndxer = new AttrIndexer<DateTime>(m_ndxerValues.Source , d => (d as SpotValue).SpotTime))
            {
                ndxer.Connect();

                IEnumerable<SpotValue> values = from SpotValue sv in ndxer.Get(dtSpot)
                                                where sv.ProductID == m_spotValue.ProductID &&
                                                sv.SupplierID == m_spotValue.SupplierID &&
                                                sv.ValueContextID == m_spotValue.ValueContextID
                                                select sv;

                if(values.Count() > 1 || (values.Count() == 1 && values.Single().ID != m_spotValue.ID) )
                {
                    var logger = new TextLogger(LogSeverity.Warning);
                    logger.Put("Duplication de données détectée.");
                    logger.Put("Elément trouvé:\n");

                    foreach (SpotValue sv in values)
                        logger.PutLine(sv);

                    logger.Flush();

                    MessageBox.Show("La validation de  données a échouée. " +
                        "Consultez le journal des événements pour plus d’informations." ,
                        null ,
                        MessageBoxButtons.OK ,
                        MessageBoxIcon.Warning);

                    return;
                }


                var newValue = new SpotValue(m_spotValue.ID , price , dtSpot , m_spotValue.ProductID , 
                    m_spotValue.ValueContextID , m_spotValue.SupplierID, 0);
                m_ndxerValues.Source.Replace(m_ndxerValues.IndexOf(m_spotValue.ID) , newValue);

                TextLogger.Info("Enregistrement réussi.");
                Close();
            }
        }
    }
}
