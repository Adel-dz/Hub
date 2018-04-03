using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.ListViewSorters;
using DGD.HubGovernor.Log;
using easyLib;
using easyLib.DB;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor
{
    sealed partial class DatumView: Form
    {
        class ListViewEntry: ListViewItem
        {
            public ListViewEntry(string[] subItems , int ndxDatum) :
                base(subItems)
            {
                DatumIndex = ndxDatum;
            }


            public int DatumIndex { get; set; }
        }


        readonly IDatumProvider m_dataProvider;
        bool m_trackNewRows;


        public event Action<IDatumProvider , int[]> DeleteClicked;
        public event Action<IDatumProvider> AddClicked;
        public event Action<IDatumProvider , IDatum> EditClicked;


        public DatumView(IDatumProvider dataProvider , string text)
        {
            Assert(dataProvider != null);

            InitializeComponent();

            m_dataProvider = dataProvider;
            Text = text;

            //handlers
            m_lvData.SelectedIndexChanged += delegate { UpdateUI(); };
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            Opts.WindowPlacement wp = AppContext.Settings.UserSettings.WindowPlacement[Text];

            if (wp != null)
                Bounds = new Rectangle(wp.Left , wp.Top , wp.Width , wp.Height);

            base.OnLoad(e);
            LoadDataAsync();
            UpdateUI();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            DeleteClicked = null;
            AddClicked = null;
            EditClicked = null;

            UnregisterHandlers();
            m_dataProvider.Close();


            var wp = new Opts.WindowPlacement(Bounds);
            AppContext.Settings.UserSettings.WindowPlacement[Text] = wp;
        }

        //private:
        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_tsbAdd.Enabled = AddClicked != null;
            m_tsbDelete.Enabled = DeleteClicked != null && m_lvData.SelectedIndices.Count > 0;
        }

        void UnregisterHandlers()
        {
            m_dataProvider.DatumDeleted += DataProvider_DatumDeleted; ;
            m_dataProvider.DatumInserted += DataProvider_DatumInserted;
            m_dataProvider.DatumReplaced += DataProvider_DatumReplaced;
        }

        void RegisterHandlers()
        {
            m_dataProvider.DatumDeleted += DataProvider_DatumDeleted; ;
            m_dataProvider.DatumInserted += DataProvider_DatumInserted;
            m_dataProvider.DatumReplaced += DataProvider_DatumReplaced;
        }

        void ConnectProvider()
        {
            m_dataProvider.Connect();
            RegisterHandlers();
        }

        ListViewEntry[] LoadItems()
        {
            Assert(m_dataProvider != null);
            Assert(m_dataProvider.IsConnected);

            var items = new ListViewEntry[m_dataProvider.Count];

            for (int i = 0; i < m_dataProvider.Count; ++i)
            {
                IDatum datum = m_dataProvider.Get(i);

                var lvi = new ListViewEntry(datum.Content , i);
                items[i] = lvi;
            }

            return items;
        }

        void InsertColumns()
        {
            Assert(!InvokeRequired);

            IDataColumn[] colHeader = m_dataProvider.DataSource.Columns;
            ColumnHeader[] columns = new ColumnHeader[colHeader.Length];

            for (int i = 0; i < columns.Length; ++i)
                columns[i] = new ColumnHeader()
                {
                    Text = colHeader[i].Caption ,
                    TextAlign = HorizontalAlignment.Center ,
                    Tag = colHeader[i].DataType ,
                };

            m_lvData.Columns.AddRange(columns);
        }

        void InsertItems(ListViewEntry[] items)
        {
            Assert(!InvokeRequired);

            m_lvData.Items.AddRange(items);

            TextLogger.Info("\nNbre d'enregistrements {0}." , m_dataProvider.Count);
        }

        void LoadDataAsync()
        {
            Func<ListViewEntry[]> loadData = () =>
            {
                ConnectProvider();
                return LoadItems();
            };


            var waitClue = new Waits.WaitClue(this);

            Action<Task<ListViewEntry[]>> onError = t =>
            {
                Exception ex = t.Exception.InnerException;
                TextLogger.Error(ex.Message);

                waitClue.LeaveWaitMode();


                if (ex is CorruptedStreamException == false)
                    ex = new CorruptedStreamException(innerException: ex);

                throw ex;
            };


            Action<Task<ListViewEntry[]>> onSuccess = t =>
            {
                InsertColumns();

                ListViewEntry[] items = t.Result;
                InsertItems(items);
                waitClue.LeaveWaitMode();

                m_tslblStatus.Text = $"Nombre d'neregistrements: {m_lvData.Items.Count}";
            };


            waitClue.EnterWaitMode();

            var loading = new Task<ListViewEntry[]>(loadData , TaskCreationOptions.LongRunning);
            loading.ContinueWith(onSuccess ,
                CancellationToken.None ,
                TaskContinuationOptions.OnlyOnRanToCompletion ,
                TaskScheduler.FromCurrentSynchronizationContext());

            loading.ContinueWith(onError , CancellationToken.None , TaskContinuationOptions.OnlyOnFaulted ,
                TaskScheduler.FromCurrentSynchronizationContext());

            loading.Start();
        }

        IColumnSorter SorterFactory(int ndxColumn)
        {
            var dataType = (ColumnDataType_t)m_lvData.Columns[ndxColumn].Tag;
            IColumnSorter sorter = null;

            switch (dataType)
            {
                case ColumnDataType_t.Text:
                sorter = new TextColumnSorter(ndxColumn);
                break;

                case ColumnDataType_t.Integer:
                sorter = new IntegerColumnSorter(ndxColumn);
                break;

                case ColumnDataType_t.Float:
                sorter = new FloatColumnSorter(ndxColumn);
                break;

                case ColumnDataType_t.Time:
                sorter = new TimeColumnSorter(ndxColumn);
                break;
            }

            Assert(sorter != null);

            return sorter;
        }

        //handlers
        private void DataProvider_DatumReplaced(int ndxDatum , IDatum newDatum)
        {
            if (InvokeRequired)
                Invoke(new Action<int , IDatum>(DataProvider_DatumReplaced) , ndxDatum , newDatum);
            else
            {
                var lvi = new ListViewEntry(newDatum.Content , ndxDatum);

                for (int i = 0; ; ++i)
                    if ((m_lvData.Items[i] as ListViewEntry).DatumIndex == ndxDatum)
                    {
                        m_lvData.Items[i] = lvi;
                        break;
                    }
            }
        }

        private void DataProvider_DatumInserted(int ndxDatum , IDatum datum)
        {
            if (InvokeRequired)
                Invoke(new Action<int , IDatum>(DataProvider_DatumInserted) , ndxDatum , datum);
            else
            {
                //adjust indices
                foreach (ListViewEntry item in m_lvData.Items)
                {
                    if (ndxDatum <= item.DatumIndex)
                        ++item.DatumIndex;
                }


                //add new item
                var lvi = new ListViewEntry(datum.Content , ndxDatum);

                m_lvData.Items.Add(lvi);

                if (m_trackNewRows)
                {
                    lvi.Selected = true;
                    m_lvData.EnsureVisible(lvi.Index);
                }

                m_tslblStatus.Text = $"Nombre d'neregistrements: {m_lvData.Items.Count}";
            }
        }

        private void DataProvider_DatumDeleted(int ndxDatum)
        {
            if (InvokeRequired)
                Invoke(new Action<int>(DataProvider_DatumDeleted) , ndxDatum);
            else
            {
                int ndxItem = -1;
                int ndx = 0;

                while (ndx < m_lvData.Items.Count)
                {
                    var item = m_lvData.Items[ndx] as ListViewEntry;

                    if (ndxDatum < item.DatumIndex)
                        --item.DatumIndex;
                    else if (ndxDatum == item.DatumIndex)
                    {
                        ndxItem = ndx;
                        break;
                    }

                    ++ndx;
                }

                Assert(ndxItem == ndx);

                while (++ndx < m_lvData.Items.Count)
                {
                    var item = m_lvData.Items[ndx] as ListViewEntry;

                    if (ndxDatum < item.DatumIndex)
                        --item.DatumIndex;
                }

                Assert(ndxItem >= 0);
                m_lvData.Items.RemoveAt(ndxItem);

                m_tslblStatus.Text = $"Nombre d'neregistrements: {m_lvData.Items.Count}";
            }
        }

        private void Delete_Click(object sender , EventArgs e)
        {
            var selItems = m_lvData.SelectedItems;
            int[] sels = new int[selItems.Count];

            for (int i = 0; i < sels.Length; ++i)
                sels[i] = (selItems[i] as ListViewEntry).DatumIndex;

            DeleteClicked?.Invoke(m_dataProvider , sels);
        }

        private void Add_Click(object sender , EventArgs e)
        {
            AddClicked?.Invoke(m_dataProvider);
        }

        private void TrackNew_Click(object sender , EventArgs e)
        {
            m_trackNewRows = !m_trackNewRows;
            (sender as ToolStripButton).Checked = m_trackNewRows;
        }

        private void DataView_ItemActivate(object sender , EventArgs e)
        {
            var sel = m_lvData.SelectedIndices;

            if (sel.Count == 1)
            {
                var handler = EditClicked;

                if (handler != null)
                {
                    int ndxDatum = (m_lvData.SelectedItems[0] as ListViewEntry).DatumIndex;
                    IDatum datum = m_dataProvider.Get(ndxDatum);
                    handler(m_dataProvider , datum);
                }
            }
        }

        private void DataView_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            var sorter = m_lvData.ListViewItemSorter as IColumnSorter;

            if (sorter == null || e.Column != sorter.ColumnIndex)
            {
                if (sorter != null)
                    m_lvData.SetColumnHeaderSortIcon(sorter.ColumnIndex , SortOrder.None);

                m_lvData.ListViewItemSorter = SorterFactory(e.Column);
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

        private void AutoSizeColumns_Click(object sender , EventArgs e)
        {
            UseWaitCursor = true;
            m_lvData.AdjustColumnsSize();
            UseWaitCursor = false;
        }
    }

}
