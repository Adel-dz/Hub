using DGD.HubCore.DB;
using DGD.HubGovernor.Extensions;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR.Imp
{
    sealed partial class ImportWizardDialog: Form, IImportWizard
    {
        readonly string m_filePath;
        readonly ConfigPage m_configPage;
        readonly PreviewPage m_previewPage;
        readonly EndPage m_endPage;
        List<int> m_ndxBadRows;
        IDictionary<uint , List<IDataRow>> m_importData;
        uint[] m_importedTable;


        public static event Action BeginUpdate;
        public static event Action<uint[]> EndUpdate;


        public ImportWizardDialog(string filePath)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));

            InitializeComponent();

            m_filePath = filePath;

            m_configPage = new ConfigPage(filePath , this)
            {
                Parent = m_clientPanel ,
                Dock = DockStyle.Fill
            };


            m_previewPage = new PreviewPage(this)
            {
                Parent = m_clientPanel ,
                Dock = DockStyle.Fill ,
                Visible = false
            };

            m_endPage = new EndPage
            {
                Parent = m_clientPanel ,
                Dock = DockStyle.Fill ,
                Visible = false
            };
        }


        public bool CanAdvance
        {
            get { return m_btnNext.Enabled; }
            set
            {
                if (m_btnNext.Enabled != value)
                    m_btnNext.Enabled = value;
            }
        }


        //protected:
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (m_importedTable == null)
                m_importedTable = new uint[0];


            EndUpdate?.Invoke(m_importedTable);

            base.OnFormClosing(e);
        }

        //private:
        string[] ReadBadRows()
        {
            var badRows = new string[m_ndxBadRows.Count];

            m_ndxBadRows.Sort();

            using (StreamReader reader = File.OpenText(m_filePath))
            {
                for (int i = 0, ndxLine = 0; i < m_ndxBadRows.Count; ++i, ++ndxLine)
                {
                    while (ndxLine < m_ndxBadRows[i])
                    {
                        reader.ReadLine();
                        ++ndxLine;
                    }

                    badRows[i] = reader.ReadLine();
                }
            }

            return badRows;
        }

        void ImportData()
        {
            BeginUpdate?.Invoke();
            

            foreach(uint idTable in m_importData.Keys)
                using (IDatumProvider dp = AppContext.TableManager.GetTable(idTable).DataProvider)
                {
                    dp.Connect();

                    foreach (IDataRow row in m_importData[idTable])
                        dp.Insert(row);

                }

            m_importedTable = m_importData.Keys.ToArray();
        }

        //handlers
        private void Next_Click(object sender , EventArgs e)
        {
            UseWaitCursor = true;

            try
            {
                if (m_previewPage.Visible)
                {
                    ImportData();
                    
                    m_endPage.BadRows = ReadBadRows();
                                        
                    m_btnNext.Enabled = m_btnPrevious.Enabled = false;
                    m_previewPage.Visible = false;
                    m_endPage.Visible = true;

                    m_btnCancel.Text = "Fermer";
                    m_btnCancel.DialogResult = DialogResult.OK;

                }
                else if (m_configPage.Visible)
                {
                    var wBox = new Waits.WaitClue(this);
                    wBox.EnterWaitMode();


                    Func<IDictionary<uint,List<IDataRow>>> runDriver = () =>
                    {
                        ImportInfo info = m_configPage.ImportData;

                        using (var driver = new ImportEngin(info.Data , info.ColumnsMapping))
                        {
                            driver.Run();
                            m_ndxBadRows = driver.IgnoredRows;
                            IDictionary<uint , List<IDataRow>> dic = driver.ImportedData;
                            var seq = from idTable in dic.Keys
                                      where dic[idTable].Count == 0
                                      select idTable;

                            foreach (uint idTable in seq.ToArray())
                                dic.Remove(idTable);

                            return dic;
                        }
                    };


                    Action<Task> onErr = t =>
                    {
                        wBox.LeaveWaitMode();
                        this.ShowError(t.Exception.InnerException.Message);
                    };


                    Action<Task<IDictionary<uint,List<IDataRow>>>> onSuccess = t =>
                    {
                        m_importData = t.Result;
                        m_previewPage.SetPreviewData(m_importData , m_configPage.ImportData.Data.Length ,
                            m_ndxBadRows.Count);

                        m_configPage.Visible = false;
                        m_previewPage.Visible = true;
                        m_btnPrevious.Enabled = true;
                        wBox.LeaveWaitMode();

                    };


                    var task = new Task<IDictionary<uint, List<IDataRow>>>(runDriver , 
                        TaskCreationOptions.LongRunning);
                    task.OnError(onErr);
                    task.OnSuccess(onSuccess);
                    task.Start();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void Previous_Click(object sender , EventArgs e)
        {
            try
            {
                if (m_previewPage.Visible)
                {
                    m_previewPage.Visible = false;
                    m_configPage.Visible = true;
                    CanAdvance = true;
                    m_btnPrevious.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }
    }
}
