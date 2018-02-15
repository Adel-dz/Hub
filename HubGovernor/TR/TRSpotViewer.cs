using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.ListViewSorters;
using DGD.HubGovernor.Waits;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR
{
    sealed partial class TRSpotViewer: Form
    {
        readonly TRDatumBuilder m_dataBuilder = new TRDatumBuilder();
        readonly Dictionary<ColumnDataType_t , Func<int , IColumnSorter>> m_colSorters;

        public TRSpotViewer()
        {
            InitializeComponent();

            m_colSorters = new Dictionary<ColumnDataType_t , Func<int , IColumnSorter>>
            {
                { ColumnDataType_t.Float,  ndxCol => new FloatColumnSorter(ndxCol) },
                { ColumnDataType_t.Integer, ndxCol => new IntegerColumnSorter(ndxCol) },
                { ColumnDataType_t.Text, ndxCol => new TextColumnSorter(ndxCol) },
                { ColumnDataType_t.Time, ndxCol => new TimeColumnSorter(ndxCol) }
            };

        }


        //proteceted:
        protected override void OnLoad(EventArgs e)
        {

            Imp.ImportWizardDialog.BeginUpdate += ImportWizardDialog_BeginUpdate;
            Imp.ImportWizardDialog.EndUpdate += ImportWizardDialog_EndUpdate;

            var wc = new WaitClue(this);


            Action<Task> onErr = (t) =>
            {
                this.ShowError(t.Exception.InnerException.Message);
                wc.LeaveWaitMode();
            };

            Action OnSuccess = () =>
            {
                wc.LeaveWaitMode();
                LoadDataAsync();
            };

            var task = new Task(RegisterHandlers , TaskCreationOptions.LongRunning);
            task.OnError(onErr);
            task.OnSuccess(OnSuccess);

            wc.EnterWaitMode();
            task.Start();

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Imp.ImportWizardDialog.BeginUpdate -= ImportWizardDialog_BeginUpdate;
            Imp.ImportWizardDialog.EndUpdate -= ImportWizardDialog_EndUpdate;

            UnregisterHandlers();
        }


        //private:
        void RegisterHandlers()
        {
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).DatumInserted += SpotValue_DatumInserted;
        }

        void UnregisterHandlers()
        {
            AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).DatumInserted -= SpotValue_DatumInserted;
        }

        void LoadDataAsync()
        {
            var wb = new Waits.WaitClue(this);

            Func<ListViewItem[]> load = () =>
            {
                var lvItems = new List<ListViewItem>();

                foreach (ITRDatum d in m_dataBuilder.BuildAll())
                    lvItems.Add(new ListViewItem(d.GetContent()) { Tag = d.SpotValue.ID });

                return lvItems.ToArray();
            };


            Action<Task<ListViewItem[]>> onSuccess = (t) =>
            {
                m_lvData.BeginUpdate();
                m_lvData.Items.Clear();
                m_lvData.Items.AddRange(t.Result);
                m_lvData.EndUpdate();
                wb.LeaveWaitMode();
            };

            Action<Task> onErr = t =>
            {
                this.ShowError(t.Exception.InnerException.Message);
                wb.LeaveWaitMode();
            };

            var task = new Task<ListViewItem[]>(load , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);

            wb.EnterWaitMode();
            task.Start();
        }


        //handlers
        private void ImportData_Click(object sender , EventArgs e)
        {

            OpenFileDialog ofDlg = null;
            Imp.ImportWizardDialog importDlg = null;


            try
            {
                string filePath = null;

                ofDlg = new OpenFileDialog();

                ofDlg.Filter = "Fichiers texte|*.txt; *.csv|Tous les fichiers|*.*";

                if (ofDlg.ShowDialog(Owner) != DialogResult.OK)
                    return;

                filePath = ofDlg.FileName;

                importDlg = new Imp.ImportWizardDialog(filePath);
                importDlg.ShowDialog(Owner);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
            finally
            {
                ofDlg?.Dispose();

                if (importDlg != null)
                {
                    importDlg.Dispose();
                }
            }
        }

        private void SpotValue_DatumInserted(HubCore.DB.IDataRow datum)
        {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(() => SpotValue_DatumInserted(datum)));
            else
            {
                var sv = datum as SpotValue;
                ITRDatum row = m_dataBuilder.Build(sv.ID);

                var lvi = new ListViewItem(row.GetContent())
                {
                    Tag = sv.ID
                };

                m_lvData.Items.Add(lvi);
            }
        }

        private void ImportWizardDialog_EndUpdate(uint[] tables)
        {
            if (tables.Length > 0)
                LoadDataAsync();

            RegisterHandlers();
        }

        private void ImportWizardDialog_BeginUpdate()
        {
            UnregisterHandlers();
        }

        private void View_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            var sorter = m_lvData.ListViewItemSorter as IColumnSorter;

            if (sorter == null || e.Column != sorter.ColumnIndex)
            {
                if (sorter != null)
                    m_lvData.SetColumnHeaderSortIcon(sorter.ColumnIndex , SortOrder.None);

                sorter = m_colSorters[(ColumnDataType_t)m_lvData.Columns[e.Column].Tag](e.Column);
                m_lvData.ListViewItemSorter = sorter;
                m_lvData.SetColumnHeaderSortIcon(e.Column , SortOrder.Ascending);
            }
            else
            {
                sorter.SortDescending = !sorter.SortDescending;
                m_lvData.Sort();

                m_lvData.SetColumnHeaderSortIcon(e.Column ,
                    sorter.SortDescending ? SortOrder.Descending : SortOrder.Ascending);
            }
        }

    }
}
