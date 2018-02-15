using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using easyLib.DB;
using DGD.HubGovernor.Extensions;
using DGD.HubCore.DB;
using DGD.HubCore;
using easyLib;

namespace DGD.HubGovernor.TR.Imp
{
    sealed partial class PreviewPage: UserControl
    {
        class CountryProvider: IDatumProvider, IDataSource
        {
            readonly PreviewPage m_owner;
            readonly IDatumProvider m_dpCountries;
            IList<IDataRow> m_impCountries;

            public event Action<int> DatumDeleted;
            public event Action<int> DatumDeleting;
            public event Action<int , IDatum> DatumInserted;
            public event Action<int , IDatum> DatumReplaced;
            public event Action<int , IDatum> DatumReplacing;
            public event Action SourceResetted;


            public CountryProvider(PreviewPage owner)
            {
                m_owner = owner;
                m_dpCountries = AppContext.TableManager.Countries.RowProvider;
            }


            public int Count
            {
                get
                {
                    Assert(IsConnected);

                    return m_impCountries.Count + m_dpCountries.Count;
                }
            }

            public IDataSource DataSource => this;
            public bool IsConnected => m_dpCountries.IsConnected;
            public bool IsDisposed => m_dpCountries.IsDisposed;
            public string Name => m_dpCountries.DataSource.Name;
            public uint ID => m_dpCountries.DataSource.ID;
            public int RowCount => Count;
            public IDataColumn[] Columns => m_dpCountries.DataSource.Columns;
            public IDatumProvider DataProvider => this;


            public void Close()
            {
                m_dpCountries.Close();

                DebugHelper.UnregisterProvider(this);
            }

            public void Connect()
            {
                m_dpCountries.Connect();

                if (m_owner.m_impData.ContainsKey(TablesID.COUNTRY))
                    m_impCountries = m_owner.m_impData[TablesID.COUNTRY];
                else
                    m_impCountries = new List<IDataRow>();

                DebugHelper.RegisterProvider(this , m_dpCountries);
            }

            public void Delete(int ndxItem)
            {
                Assert(IsConnected);
                Assert(ndxItem < Count);

                DatumDeleting?.Invoke(ndxItem);

                if (ndxItem < m_dpCountries.Count)
                    m_dpCountries.Delete(ndxItem);
                else
                    m_impCountries.RemoveAt(ndxItem - m_dpCountries.Count);

                DatumDeleted?.Invoke(ndxItem);
            }

            public void Dispose()
            {
                if (!IsDisposed)
                {
                    m_dpCountries.Dispose();
                    m_impCountries = null;

                    DatumDeleted = DatumDeleting = null;
                    DatumInserted = null;
                    DatumReplaced = DatumReplacing = null;
                    SourceResetted = null;

                    DebugHelper.UnregisterProvider(this);
                }
            }

            public IEnumerable<IDatum> Enumerate()
            {
                Assert(IsConnected);

                IEnumerable<IDatum> seq = m_dpCountries.Enumerate();

                return seq.Concat(m_impCountries);
            }

            public IEnumerable<IDatum> Enumerate(int ndxFirst)
            {
                Assert(ndxFirst < Count);


                if (ndxFirst < m_dpCountries.Count)
                    return m_dpCountries.Enumerate(ndxFirst).Concat(m_impCountries);

                return m_impCountries.Skip(ndxFirst - m_dpCountries.Count);
            }

            public IDatum Get(int ndx)
            {
                Assert(IsConnected);
                Assert(ndx < Count);

                if (ndx < m_dpCountries.Count)
                    return m_dpCountries.Get(ndx);

                return m_impCountries[ndx - m_dpCountries.Count];
            }

            public void Insert(IDatum item)
            {
                Assert(IsConnected);

                m_impCountries.Add(item as IDataRow);
                DatumInserted?.Invoke(Count - 1 , item);
            }

            public void Replace(int ndxItem , IDatum item)
            {
                Assert(IsConnected);
                Assert(ndxItem < Count);

                DatumReplacing?.Invoke(ndxItem , Get(ndxItem));

                if (ndxItem < Count)
                    m_dpCountries.Replace(ndxItem , item);
                else
                    m_impCountries[ndxItem - m_dpCountries.Count] = item as IDataRow;

                DatumReplaced(ndxItem , item);
            }
        }



        IDictionary<uint , List<IDataRow>> m_impData;
        readonly IImportWizard m_impWizard;
        readonly List<IDataRow> m_modifiedCountries = new List<IDataRow>();
        readonly List<IDataRow> m_modifiedPlaces = new List<IDataRow>();
        Font m_boldFont;


        public PreviewPage(IImportWizard impWiz)
        {
            Assert(impWiz != null);

            InitializeComponent();

            m_impWizard = impWiz;
        }


        public IDictionary<uint , List<IDataRow>> PreviewData => m_impData;

        public void SetPreviewData(IDictionary<uint , List<IDataRow>> data , int rowsCount , int badRowsCount)
        {
            m_impData = data;
            m_lblImportStatus.Text = $"{rowsCount - badRowsCount} / {rowsCount}  ligne(s) importée(s).";

            m_lvTables.Items.Clear();
            m_lvData.Clear();

            IEnumerable<IDataTable> tables = from id in m_impData.Keys
                                             select AppContext.TableManager.GetTable(id);



            foreach (IDataTable table in tables)
            {
                var lvItem = new ListViewItem(table.Name)
                {
                    Tag = table
                };

                //les seules tables qui puissent contenir des erreurs sont lieu et pays

                if (table.ID == TablesID.COUNTRY)
                    lvItem.ImageIndex = 0;
                else if (table.ID == TablesID.PLACE)
                    foreach (Places.Place row in m_impData[table.ID])
                        if (row.CountryID == 0)
                        {
                            lvItem.ImageIndex = 0;
                            break;
                        }

                m_lvTables.Items.Add(lvItem);
            }

            m_impWizard.CanAdvance = m_lvTables.Items.Count > 0;
        }

        //protected:
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if(m_boldFont != null)
            {
                m_boldFont.Dispose();
                m_boldFont = null;
            }

            base.OnHandleDestroyed(e);
        }

        //private:
        void ModifyCountryName()
        {
            ListViewItem item = m_lvData.SelectedItems[0];
            var ctry = item.Tag as Countries.Country;

            if (ctry != null)
            {
                using (var dlg = new InputDialog())
                {
                    dlg.Message = "Remplacer le nom du pays.";
                    dlg.Input = ctry.Name;

                    Opts.StringTransform_t opt = AppContext.Settings.AppSettings.ImportTransform;
                    dlg.InputCasing = opt == Opts.StringTransform_t.LowerCase ? CharacterCasing.Lower :
                        opt == Opts.StringTransform_t.UpperCase ? CharacterCasing.Upper :
                        CharacterCasing.Normal;


                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Predicate<IDataRow> compCountries = c => c == ctry;                       

                        var newCtry = new Countries.Country(ctry.ID , dlg.Input , ctry.InternalCode);

                        int ndxCtry = m_impData[TablesID.COUNTRY].FindIndex(compCountries);
                        m_impData[TablesID.COUNTRY][ndxCtry] = newCtry;

                        ndxCtry = m_modifiedCountries.FindIndex(compCountries);

                        if (ndxCtry > 0)
                            m_modifiedCountries[ndxCtry] = newCtry;
                        else
                            m_modifiedCountries.Add(newCtry);


                        const int NDX_COUNTRY_NAME_SUB_ITEM = 1;
                        item.SubItems[NDX_COUNTRY_NAME_SUB_ITEM].Text = newCtry.Name;
                        item.Tag = newCtry;
                        item.ImageIndex = -1;

                        if (m_boldFont == null)
                            m_boldFont = new Font(item.Font , FontStyle.Bold);

                        item.Font = m_boldFont;

                    }
                }
            }
        }

        void ModifyPalceCountry()
        {
            ListViewItem lvi = m_lvData.SelectedItems[0];
            var p = lvi.Tag as Places.Place;

            if (p != null)
                using (var dp = new CountryProvider(this))
                {
                    Predicate<IDataRow> compPlace = x => x == p;

                    using (var dlg = new Countries.ChooseCountryDialog(dp))
                    {
                        if(p.CountryID != 0)
                        {
                            dp.Connect();

                            foreach (Countries.Country ctry in dp.Enumerate())
                                if(ctry.ID == p.CountryID)
                                {
                                    dlg.SelectedCountry = ctry;
                                    break;
                                }
                        }

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Countries.Country newCtry = dlg.SelectedCountry;
                            var place = new Places.Place(p.ID , p.Name , newCtry.ID);

                            int ndxPlace = m_modifiedPlaces.FindIndex(compPlace);

                            if (ndxPlace >= 0)
                                m_modifiedPlaces[ndxPlace] = place;
                            else
                                m_modifiedPlaces.Add(place);


                            ndxPlace = m_impData[TablesID.PLACE].FindIndex(compPlace);
                            m_impData[TablesID.PLACE][ndxPlace] = place;

                            const int NDX_COUNTRY = 2;
                            lvi.SubItems[NDX_COUNTRY].Text = newCtry.ID.ToString();
                            lvi.Tag = place;

                            if (m_boldFont == null)
                                m_boldFont = new Font(lvi.Font , FontStyle.Bold);

                            lvi.Font = m_boldFont;
                            lvi.ImageIndex = -1;
                        }
                    }
                }
        }

        //handlers
        private void Tables_SelectedIndexChanged(object sender , EventArgs e)
        {
            m_lvData.Clear();

            var sel = m_lvTables.SelectedItems;

            if (sel.Count == 0)
                return;

            IDataTable table = sel[0].Tag as IDataTable;

            //init columns
            IDataColumn[] columns = table.Columns;
            var header = new ColumnHeader[columns.Length];

            for (int i = 0; i < columns.Length; ++i)
                header[i] = new ColumnHeader()
                {
                    Text = columns[i].Caption ,
                    TextAlign = HorizontalAlignment.Center
                };


            m_lvData.Columns.AddRange(header);
            m_lvData.ListViewItemSorter = new ListViewSorters.DefaultColumnSorter();

            //add data
            IList<IDataRow> data = m_impData[table.ID];
            var items = new ListViewItem[data.Count];

            //cas particuers: CountryTable et placeTable
            if (table.ID == TablesID.COUNTRY)
            {
                for (int i = 0; i < data.Count; ++i)
                {
                    var lvi = new ListViewItem(data[i].Content)                    
                    {
                        Tag = data[i]
                    };

                    if (m_modifiedCountries.Contains(data[i]))
                    {
                        if (m_boldFont == null)
                            m_boldFont = new Font(lvi.Font , FontStyle.Bold);

                        lvi.Font = m_boldFont;
                    }
                    else
                        lvi.ImageIndex = 0;

                    items[i] = lvi;
                }
            }
            else if (table.ID == TablesID.PLACE)
            {
                for (int i = 0; i < data.Count; ++i)
                {
                    var p = data[i] as Places.Place;
                    var lvi = new ListViewItem(data[i].Content);                    

                    if (p.CountryID == 0)
                    {
                        lvi.ImageIndex = 0;
                        lvi.Tag = p;
                    }
                    else if(m_modifiedPlaces.Contains(p))
                    {
                        if (m_boldFont == null)
                            m_boldFont = new Font(lvi.Font , FontStyle.Bold);

                        lvi.Font = m_boldFont;
                        lvi.Tag = p;
                    }

                    items[i] = lvi;
                }

            }
            else
                for (int i = 0; i < data.Count; ++i)
                    items[i] = new ListViewItem(data[i].Content);

            m_lvData.Items.AddRange(items);
            m_lvData.AdjustColumnsSize();
        }

        private void Data_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            var sorter = m_lvData.ListViewItemSorter as ListViewSorters.DefaultColumnSorter;

            if (sorter.ColumnIndex == e.Column)
                sorter.SortDescending = !sorter.SortDescending;
            else
                sorter.ColumnIndex = e.Column;

            m_lvData.Sort();
        }

        private void Data_ItemActivate(object sender , EventArgs e)
        {
            var selTable = m_lvTables.SelectedItems[0].Tag as IDataTable;

            if (selTable.ID == TablesID.PLACE)
                ModifyPalceCountry();
            else if (selTable.ID == TablesID.COUNTRY)
                try
                {
                    ModifyCountryName();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    easyLib.Log.EventLogger.Error(ex.Message);
                }
        }
    }
}
