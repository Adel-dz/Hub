using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGD.HubCore;
using DGD.Hub.DB;
using DGD.HubCore.DB;
using static System.Diagnostics.Debug;
using easyLib.Extensions;
using easyLib.DB;
using DGD.Hub.WF;
using System.Drawing;

namespace DGD.Hub.SpotView
{
    sealed partial class SpotView: UserControl, IView
    {
        class CountryEntry
        {
            public CountryEntry(Country cntry)
            {
                Country = cntry;
            }

            public Country Country { get; }

            public static bool UseCountryCode { get; set; }

            public override string ToString()
            {
                if (Country == null)
                    return AppText.UNSPECIFIED;

                if (UseCountryCode)
                    return Country.InternalCode.ToString("000");

                return Country.Name;
            }

        }


        readonly Dictionary<ColumnDataType_t , IColumnSorter> m_colSorters = new Dictionary<ColumnDataType_t , IColumnSorter>();
        bool m_emptyMode;


        public SpotView()
        {
            InitializeComponent();

            SetupColumns();

            if (Program.Settings.MRUSubHeadingSize > 0)
            {
                var autoCompleteSrc = new AutoCompleteStringCollection();
                string[] strs = Program.Settings.MRUSubHeading.Select(sh => sh.ToString()).ToArray();
                autoCompleteSrc.AddRange(strs);

                m_tbSubHeading.AutoCompleteCustomSource = autoCompleteSrc;
            }
        }

        public void Activate(Control parent)
        {
            Assert(parent != null);

            bool useCountryCode = Program.Settings.UseCountryCode;

            if (m_cbOrigin.Items.Count == 0 || CountryEntry.UseCountryCode != useCountryCode)
            {
                CountryEntry.UseCountryCode = useCountryCode;

                if (useCountryCode)
                {
                    m_cbOrigin.Size = new Size(77 , 21);
                    m_lblCountryInfo.Location = new Point(505 , 109);
                }
                else
                {
                    m_cbOrigin.Size = new Size(161 , 21);
                    m_lblCountryInfo.Location = new Point(581 , 109);
                }


                LoadCountries();
            }

            parent.Controls.Add(this);
            RegisterHandlers();
            Dock = DockStyle.Fill;
            Show();
        }

        public void Deactivate(Control parent)
        {
            Assert(parent != null);

            Hide();
            UnregisterHandlers();
            parent.Controls.Remove(this);
        }

        //protected:

        protected override void OnLoad(EventArgs e)
        {
            LoadIncoterms();
            base.OnLoad(e);
        }

        protected override bool ProcessCmdKey(ref Message msg , Keys keyData)
        {
            if (keyData == Keys.Enter && ActiveControl == m_tbSubHeading)
            {
                m_tsbSearch.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg , keyData);
        }

        //private:
        bool EmptyMode
        {
            get { return m_emptyMode; }

            set
            {
                m_lvSearchResult.Items.Clear();

                if (value)
                {
                    m_lvSearchResult.View = View.Tile;
                    m_lvSearchResult.Items.Add(new ListViewItem(new string[] { " " , "Aucun élement à afficher" }));
                    m_tsbToggleDetails.Enabled = m_tsbToggleView.Enabled = false;
                }
                else
                {
                    m_tsbToggleView.Enabled = true;
                    m_lvSearchResult.View = m_tsbToggleView.Checked ? View.Tile : View.Details;
                    m_tsbToggleDetails.Enabled = m_lvSearchResult.View == View.Details;

                    SetupColumns();
                }

                m_emptyMode = value;
                m_lvSearchResult.Enabled = !value;
            }
        }

        IColumnSorter GetColumnSorter(ColumnDataType_t dataType)
        {
            IColumnSorter sorter;

            if (!m_colSorters.TryGetValue(dataType , out sorter))
            {
                switch (dataType)
                {
                    case ColumnDataType_t.Text:
                    sorter = new TextColumnSorter();
                    break;

                    case ColumnDataType_t.Integer:
                    sorter = new IntegerColumnSorter();
                    break;

                    case ColumnDataType_t.Float:
                    sorter = new FloatColumnSorter();
                    break;

                    case ColumnDataType_t.Time:
                    sorter = new TimeColumnSorter();
                    break;

                    default:
                    Assert(false);
                    break;
                }

                m_colSorters[dataType] = sorter;
            }


            return sorter;
        }

        void RegisterHandlers()
        {
            TablesManager tablesManager = Program.TablesManager;

            tablesManager.BeginTableProcessing += BeginTableProcessing_Handler;
            tablesManager.EndTableProcessing += EndTableProcessing_Handler;
            tablesManager.GetDataProvider(TablesID.COUNTRY).SourceCleared += Countries_SourceCleared;
            tablesManager.GetDataProvider(TablesID.INCOTERM).SourceCleared += Incoterms_SourceCleared;

            AutoUpdater.BeginTableUpdate += BeginTableProcessing_Handler;
            AutoUpdater.EndTableUpdate += EndTableProcessing_Handler;
        }

        void UnregisterHandlers()
        {
            TablesManager tablesManager = Program.TablesManager;

            tablesManager.BeginTableProcessing -= BeginTableProcessing_Handler;
            tablesManager.EndTableProcessing -= EndTableProcessing_Handler;
            tablesManager.GetDataProvider(TablesID.COUNTRY).SourceCleared -= Countries_SourceCleared;
            tablesManager.GetDataProvider(TablesID.INCOTERM).SourceCleared -= Incoterms_SourceCleared;

            AutoUpdater.BeginTableUpdate -= BeginTableProcessing_Handler;
            AutoUpdater.EndTableUpdate -= EndTableProcessing_Handler;
        }

        void LoadCountries()
        {
            IEnumerable<CountryEntry> countries = from Country ctry in Program.TablesManager.GetDataProvider(TablesID.COUNTRY).Enumerate()
                                                  orderby ctry.Name
                                                  select new CountryEntry(ctry);

            m_cbOrigin.Items.Clear();
            m_cbOrigin.Items.Add(new CountryEntry(null));
            m_cbOrigin.Items.AddRange(countries.ToArray());

            m_cbOrigin.SelectedIndex = 0;
        }

        void LoadIncoterms()
        {
            IEnumerable<Incoterm> icts = from Incoterm ict in Program.TablesManager.GetDataProvider(TablesID.INCOTERM).Enumerate()
                                         orderby ict.Name
                                         select ict;

            var emptyICT = new Incoterm(0 , AppText.UNSPECIFIED);

            m_cbIncoterm.Items.Clear();
            m_cbIncoterm.Items.Add(emptyICT);
            m_cbIncoterm.Items.AddRange(icts.ToArray());
            m_cbIncoterm.DisplayMember = "Name";
            m_cbIncoterm.SelectedIndex = 0;
        }

        void SearchAsyncSubHeading(SubHeading subHeading , DateTime date , Incoterm ict , Country origin)
        {
            IDisposable releaser = Log.LogEngin.PushMessage("Recherche en cours...");
            m_tsbSearch.Enabled = false;

            Func<SpotValue , SpotValue , bool> isSameVC = (sv1 , sv2) =>
                {
                    if (sv1.ValueContextID == sv2.ValueContextID)
                        return true;

                    ValueContext vc1 = sv1.ValueContext, vc2 = sv2.ValueContext;

                    return vc1.IncotermID == vc2.IncotermID && vc1.OriginID == vc2.OriginID;
                };


            Func<SpotViewItem[]> search = () =>
            {
                IDBColumnIndexer<SubHeading> ndxer =
                    Program.TablesManager.GetSubHeadingIndexer(TablesID.SPOT_VALUE , TablesManager.ColumnID_t.SubHeading);

                IEnumerable<SpotValue> seq = from SpotValue sv in ndxer.Get(subHeading)
                                             select sv;

                var candidatesValue = new List<SpotValue>();

                foreach (SpotValue sv in seq)
                {
                    if (sv.SpotTime > date)
                        continue;

                    int ndx = candidatesValue.FindIndex(v => v.Product.ID == sv.Product.ID && isSameVC(v , sv));

                    if (ndx == -1)
                        candidatesValue.Add(sv);
                    else if (candidatesValue[ndx].SpotTime < sv.SpotTime)
                        candidatesValue[ndx] = sv;
                }

                seq = candidatesValue;

                if (ict.ID != 0)
                    seq = from SpotValue sv in seq
                          where sv.ValueContext.IncotermID == ict.ID
                          select sv;


                if (origin != null)
                    seq = from SpotValue sv in seq
                          where sv.ValueContext.OriginID == origin.ID
                          select sv;


                IEnumerable<SpotViewItem> items = from SpotValue sv in seq
                                                  select new SpotViewItem(sv);

                return items.ToArray();
            };

            Action<Task<SpotViewItem[]>> onSuccess = (t) =>
            {
                EmptyMode = t.Result.Length == 0;

                if (!EmptyMode)
                {
                    m_lvSearchResult.BeginUpdate();
                    m_lvSearchResult.Items.AddRange(t.Result);

                    if (m_lvSearchResult.View == View.Details)
                        m_lvSearchResult.AdjustColumnsSize();

                    m_lvSearchResult.EndUpdate();
                }



                string txtLog = $"Module valeurs boursières: recherche de la SPT {subHeading}, date: {date.ToShortDateString()}, " +
                    $"incoterm: {(ict.ID == 0 ? AppText.UNSPECIFIED : ict.Name)}, origine: {(origin == null ? AppText.UNSPECIFIED : origin.Name)}, " +
                    $"Nbre d'enregistrements trouvés: {t.Result.Length}";

                Program.DialogManager.PostLog(txtLog , false);


                m_tsbSearch.Enabled = true;
                releaser.Dispose();
                Log.LogEngin.PushFlash($"{t.Result.Length} élément(s) trouvé(s).");
            };

            Action<Task> onErr = (t) =>
            {
                m_tsbSearch.Enabled = true;
                Log.LogEngin.PushFlash(t.Exception.InnerException.Message);

                string errLog = "Module valeurs boursiére: Erreur lors de la recherche, " + t.Exception.InnerException.Message;
                Program.DialogManager.PostLog(errLog , true);
            };

            var task = new Task<SpotViewItem[]>(search , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);
            task.Start();
        }

        void SetupColumns()
        {
            m_lvSearchResult.Columns.Clear();

            var columns = new ColumnHeader[]
            {
                new ColumnHeader{ Text = "Produit", Tag = ColumnDataType_t.Text },
                new ColumnHeader { Text = "Date de cotation", Tag = ColumnDataType_t.Time },
                new ColumnHeader { Text = "Prix", Tag = ColumnDataType_t.Text },
                new ColumnHeader { Text = "Origine", Tag = ColumnDataType_t.Text }
            };

            m_lvSearchResult.Columns.AddRange(columns);

            if (m_tsbToggleDetails.Checked)
            {
                var columnsEx = new ColumnHeader[]
                {
                    new ColumnHeader{Text = "Incoterm", Tag = ColumnDataType_t.Text },
                    new ColumnHeader { Text = "Lieu", Tag = ColumnDataType_t.Text},
                    new ColumnHeader { Text = "Description", Tag = ColumnDataType_t.Text },
                    new ColumnHeader { Text = "Source", Tag = ColumnDataType_t.Text }
                };

                m_lvSearchResult.Columns.AddRange(columnsEx);
            }
        }

        //handlers:
        private void Search_Click(object sender , EventArgs e)
        {
            var subHeading = SubHeading.Parse(m_tbSubHeading.Text);

            if (subHeading == null)
            {
                MessageBox.Show(Parent , "Contenu ou format de la sous-position tarifaire incorrect." , null , MessageBoxButtons.OK ,
                    MessageBoxIcon.Error);
                return;
            }

            if (Program.Settings.MRUSubHeading.Add(subHeading))
                m_tbSubHeading.AutoCompleteCustomSource.Add(subHeading.ToString());


            DateTime date = m_dtpSpotDate.Value;
            var ict = m_cbIncoterm.SelectedItem as Incoterm;
            var ctry = (m_cbOrigin.SelectedItem as CountryEntry).Country;

            SearchAsyncSubHeading(subHeading , date , ict , ctry);
        }

        private void EndTableProcessing_Handler(uint tableID)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<uint>(EndTableProcessing_Handler) , tableID);
            else
            {
                try
                {
                    if (tableID == TablesID.COUNTRY)
                    {
                        m_cbOrigin.Enabled = true;
                        LoadCountries();
                    }
                    else if (tableID == TablesID.INCOTERM)
                    {
                        m_cbIncoterm.Enabled = true;
                        LoadIncoterms();
                    }
                }
                catch (Exception ex)
                {
                    Program.DialogManager.PostLog($"Erreur fin de traitement de la table {tableID}: " +
                        ex.Message, true);

                    MessageBox.Show(this , ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }

                m_tsbSearch.Enabled = true;
            }
        }

        private void BeginTableProcessing_Handler(uint tableID)
        {
            if (InvokeRequired)
                Invoke(new Action<uint>(BeginTableProcessing_Handler) , tableID);
            else
            {
                if (tableID == TablesID.COUNTRY)
                    m_cbOrigin.Enabled = false;
                else if (tableID == TablesID.INCOTERM)
                    m_cbIncoterm.Enabled = false;

                m_tsbSearch.Enabled = false;
            }
        }

        private void Origin_SelectedIndexChanged(object sender , EventArgs e)
        {
            int ndx = m_cbOrigin.SelectedIndex;

            string txt;

            if (ndx == 0)
                txt = "";
            else if (CountryEntry.UseCountryCode)
                txt = $"( {(m_cbOrigin.SelectedItem as CountryEntry).Country.Name} )";
            else
                txt = $"( Code pays: {(m_cbOrigin.SelectedItem as CountryEntry).Country.InternalCode.ToString()} )";

            m_lblCountryInfo.Text = txt;
        }

        private void ToggleView_Click(object sender , EventArgs e)
        {
            const string tiledViewText = "Mosaïque";
            const string listViewText = "Liste";

            if (m_lvSearchResult.View == View.Details)
            {
                m_lvSearchResult.View = View.Tile;
                m_tsbToggleDetails.Enabled = false;
                m_tsbToggleView.Text = listViewText;
            }
            else
            {
                m_lvSearchResult.View = View.Details;
                m_tsbToggleDetails.Enabled = true;
                m_tsbToggleView.Text = tiledViewText;

                m_lvSearchResult.AdjustColumnsSize();
            }

            m_tsbToggleView.Checked = m_lvSearchResult.View == View.Tile;
        }

        private void ToggleDetails_Click(object sender , EventArgs e)
        {
            const string showMoreText = "Afficher plus détails";
            const string showLessText = "Masquer les détails";

            if (m_tsbToggleDetails.Checked)
            {
                m_tsbToggleDetails.Checked = false;
                m_tsbToggleDetails.Text = showMoreText;
            }
            else
            {
                m_tsbToggleDetails.Checked = true;
                m_tsbToggleDetails.Text = showLessText;
            }

            SetupColumns();
            m_lvSearchResult.AdjustColumnsSize();
        }

        private void TogglePanel_Click(object sender , EventArgs e)
        {
            const string collapseText = "Masquer le formulaire";
            const string ExpandText = "Afficher le formulaire";

            m_tsbSearch.Enabled = m_searchPanel.Visible = !m_searchPanel.Visible;

            if (m_searchPanel.Visible)
            {
                m_tsbTogglePanel.Image = Properties.Resources.Collapse_32;
                m_tsbTogglePanel.Text = collapseText;
            }
            else
            {
                m_tsbTogglePanel.Image = Properties.Resources.Expand_32;
                m_tsbTogglePanel.Text = ExpandText;
            }
        }

        private void SearchResult_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            var sorter = m_lvSearchResult.ListViewItemSorter as IColumnSorter;
            ColumnDataType_t dataType = (ColumnDataType_t)m_lvSearchResult.Columns[e.Column].Tag;

            if (sorter == null)
            {
                sorter = GetColumnSorter(dataType);
                sorter.ColumnIndex = e.Column;
                m_lvSearchResult.ListViewItemSorter = sorter;
            }
            else if (sorter.ColumnIndex != e.Column)
            {
                m_lvSearchResult.SetColumnHeaderSortIcon(sorter.ColumnIndex , SortOrder.None);
                sorter = GetColumnSorter(dataType);
                sorter.ColumnIndex = e.Column;
                sorter.SortDescending = false;
                m_lvSearchResult.ListViewItemSorter = sorter;
            }
            else
                sorter.SortDescending = !sorter.SortDescending;


            m_lvSearchResult.Sort();
            m_lvSearchResult.SetColumnHeaderSortIcon(e.Column , sorter.SortDescending ? SortOrder.Descending :
                SortOrder.Ascending);
        }

        private void Incoterms_SourceCleared()
        {
            if (InvokeRequired)
                Invoke(new Action(LoadIncoterms));
            else
                LoadIncoterms();
        }

        private void Countries_SourceCleared()
        {
            if (InvokeRequired)
                Invoke(new Action(LoadCountries));
            else
                LoadCountries();
        }

    }
}
