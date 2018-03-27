using DGD.HubGovernor.Extensions;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Incoterms
{
    public partial class IncotermForm: Form
    {
        readonly Incoterm m_incoterm;
        readonly AttrIndexer<string> m_ndxerNames;
        bool m_dialogEnded;


        public IncotermForm(IDatumProvider srcIncoterms, IDatum datum = null)
        {
            Assert(srcIncoterms != null);
            Assert(datum == null || datum is Incoterm);
            
            InitializeComponent();

            m_ndxerNames = new AttrIndexer<string>(srcIncoterms , d => (d as Incoterm).Name.ToUpper());

            if(datum != null)
            {
                m_incoterm = datum as Incoterm;

                m_tsbReload.Click += delegate
                {
                    m_tbName.Text = m_incoterm.Name;
                };

                Action<IDatum> hdler = delegate (IDatum d)
                {
                    if ((d as Incoterm).ID == m_incoterm.ID)
                        EndDialog();
                };


                m_ndxerNames.DatumDeleted += hdler;
                m_ndxerNames.DatumReplaced += hdler;
            }

            m_tbName.TextChanged += delegate
            {
                UpdateUI();
            };

            m_tsbSave.Click += delegate
            {
                Save();
            };
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_ndxerNames.Connect();

            if (m_incoterm != null)
                m_tbName.Text = m_incoterm.Name;

            UpdateUI();

            base.OnLoad(e);
        }
        
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_ndxerNames.Close();
        }


        //private:
        void EndDialog()
        {
            Assert(!InvokeRequired);

            m_tbName.Enabled = false;
            m_dialogEnded = true;

            UpdateUI();
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbSave.Enabled = m_tbName.TextLength > 0 && !m_dialogEnded;
            m_tsbReload.Enabled = m_incoterm != null && !m_dialogEnded;
        }

        void Save()
        {
            Assert(!InvokeRequired);

            string ictName = m_tbName.GetInputText();

            if(string.IsNullOrWhiteSpace(ictName))
            {
                this.ShowWarning("Champs monétaire mal servi. Veuillez compléter le formulaire.");
                m_tbName.Select();

                return;
            }


            if(m_incoterm != null && m_incoterm.Name == ictName)
            {
                TextLogger.Info("Aucune modification détectée, enregistrement non-nécessaire.");

                Close();
                return;
            }


            IEnumerable<IDatum> data = m_ndxerNames.Get(ictName.ToUpper());

            int count = data.Count();

            if(count == 0 || (count == 1 && m_incoterm != null && m_incoterm.ID == data.Cast<Incoterm>().Single().ID))
            {
                uint id = m_incoterm == null ? AppContext.TableManager.Incoterms.CreateUniqID() :
                    m_incoterm.ID;

                var ict = new Incoterm(id , ictName);

                if (m_incoterm == null)
                    m_ndxerNames.Source.Insert(ict);
                else
                    m_ndxerNames.Source.Replace(m_ndxerNames.IndexOf(m_incoterm.Name.ToUpper()).Single() , ict);

                TextLogger.Info("Enregistrement réussi.");
                Close();
            }
            else
            {
                var logger = new TextLogger(LogSeverity.Warning);
                logger.Put("Duplication de données détectée.");
                logger.Put("Elément trouvé:\n");

                foreach (Incoterm ict in data)
                    logger.PutLine(ict);

                logger.Flush();

                MessageBox.Show("La validation de  données a échouée. " +
                    "Consultez le journal des événements pour plus d’informations." ,
                    null ,
                    MessageBoxButtons.OK ,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
