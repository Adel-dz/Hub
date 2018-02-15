using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using System.IO;
using easyLib.DSV;
using easyLib.Extensions;

namespace DGD.HubGovernor.TR.Imp
{
    sealed partial class ConfigPage: UserControl
    {
        readonly IImportWizard m_impWizard;
        readonly string m_filePath;
        int m_ndxSelectedColumn = -1;


        public ConfigPage(string filePath, IImportWizard importWiz)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));
            Assert(importWiz != null);

            InitializeComponent();

            m_filePath = filePath;
            m_impWizard = importWiz;

            InitColumns();

            //load settings
            var opt = AppContext.Settings.UserSettings.DSVImportSettings;
            m_nudLinesToIgnore.Value = opt.LineOffset;
            m_nudLinesToShow.Value = opt.DisplayCount;
            m_tbSeparator.Text = opt.ColumnsSeparator.ToString();

            IDictionary<ColumnKey_t, int> mapping = opt.ColumnsMapping;
            if (mapping != null)
                InitMapping(mapping);
            

            if (m_tbSeparator.TextLength > 0)
                LoadData();

            UpdateUI();

            m_tbSeparator.TextChanged += ParamChanged_EventHandler;
            m_nudLinesToIgnore.ValueChanged += ParamChanged_EventHandler;
            m_nudLinesToShow.ValueChanged += ParamChanged_EventHandler;
            m_lbColumns.SelectedIndexChanged += delegate { UpdateUI(); };

            ImportWizardDialog.EndUpdate += ImportWizardDialog_EndUpdate;
        }


        public bool IsInputComplete => m_impWizard.CanAdvance;

        public ImportInfo ImportData
        {
            get
            {
                Assert(IsInputComplete);


                var mapping = new Dictionary<ColumnKey_t , int>();

                foreach (ColumnMapping ci in m_lbColumns.Items)
                    mapping[ci.Key] = ci.DSVIndex;


                List<string[]> data = new List<string[]>();

                using (var sr = File.OpenText(m_filePath))
                {
                    int offset = (int)m_nudLinesToIgnore.Value;

                    for (int i = 0; i < offset; ++i)
                        sr.ReadLine();


                    var dsvReader = new DSVReader(sr , m_tbSeparator.Text[0]);

                    foreach (IList<string> str in dsvReader.ReadAllRows())
                    {
                        string[] row = str.ToArray();

                        if (row.Length < ColumnMapping.COLUMNS_COUNT)
                            throw new InvalidDataException();

                        data.Add(row);
                    }
                }


                return new ImportInfo(data.NoDup(RowsComparer).ToArray() , mapping);
            }
        }

        //protected:
        protected override void OnHandleDestroyed(EventArgs e)
        {
            ImportWizardDialog.EndUpdate -= ImportWizardDialog_EndUpdate;

            base.OnHandleDestroyed(e);
        }

        //private:
        void InitMapping(IDictionary<ColumnKey_t , int> mapping)
        {
            Assert(!InvokeRequired);

            if (mapping.Count != ColumnMapping.COLUMNS_COUNT ||
                    mapping.Values.ToArray().Distinct().Count() != ColumnMapping.COLUMNS_COUNT)
                return;


            foreach (ColumnMapping colInfo in m_lbColumns.Items)
                if (mapping.ContainsKey(colInfo.Key))
                    colInfo.DSVIndex = mapping[colInfo.Key];
        }

        void InitColumns()
        {
            Assert(!InvokeRequired);

            //init lb data
            var colInfos = new ColumnMapping[]
            {
                new ColumnMapping(ColumnKey_t.SessionNber, "N° Session"),
                new ColumnMapping(ColumnKey_t.SubHeading, "SPT"),
                new ColumnMapping(ColumnKey_t.LabelUs,"Libellé US"),
                new ColumnMapping(ColumnKey_t.LabelFr, "Libellé FR"),
                new ColumnMapping(ColumnKey_t.Price, "Prix"),
                new ColumnMapping(ColumnKey_t.Currency, "Monnaie"),
                new ColumnMapping(ColumnKey_t.Incoterm, "Incoterm"),
                new ColumnMapping(ColumnKey_t.Place, "Lieu"),
                new ColumnMapping(ColumnKey_t.Date,"Date de cotation"),
                new ColumnMapping(ColumnKey_t.Unit,"Unité"),
                new ColumnMapping(ColumnKey_t.Origin,"Origine"),
                new ColumnMapping(ColumnKey_t.ProductNber,"N° produit")
            };

            m_lbColumns.Items.AddRange(colInfos);
        }

        List<IList<string>> LoadRows(char sep , int offset , int maxRow)
        {
            var data = new List<IList<string>>();

            using (StreamReader sr = File.OpenText(m_filePath))
            {

                for (int i = 0; i < offset; ++i)
                    sr.ReadLine();

                var reader = new DSVReader(sr , sep);


                for (int i = 0; i < maxRow; ++i)
                {
                    IList<string> row = reader.ReadNextRow();

                    if (row == null)
                        break;

                    if (row.Count < ColumnMapping.COLUMNS_COUNT)
                        throw new InvalidDataException();
                    
                    data.Add(row);
                }
            }


            return data;
        }

        void LoadData()
        {
            Assert(!InvokeRequired);

            char sep = m_tbSeparator.Text[0];
            int nbLineToIgnore = (int)m_nudLinesToIgnore.Value;


            List<IList<string>> data = LoadRows(sep , (int)nbLineToIgnore , (int)m_nudLinesToShow.Value);

            int nbColumns = int.MaxValue;
            foreach (IList<string> row in data)
            {
                if (row.Count < nbColumns)
                    nbColumns = row.Count;
            }

            if (nbColumns != m_lvData.Columns.Count)
            {
                var columns = new ColumnHeader[nbColumns];

                for (int i = 0; i < nbColumns; ++i)
                    columns[i] = new ColumnHeader
                    {
                        Text = "?"
                    };

                foreach (ColumnMapping ci in m_lbColumns.Items)
                    if (ci.DSVIndex != ColumnMapping.NULL_NDX)
                        if (ci.DSVIndex >= nbColumns)
                            ci.DSVIndex = ColumnMapping.NULL_NDX;
                        else
                            columns[ci.DSVIndex].Text = ci.Text;

                m_lvData.Columns.AddRange(columns);
            }

            m_lvData.Items.Clear();
            AddItems(data);
        }

        void AddItems(List<IList<string>> rows)
        {
            Assert(!InvokeRequired);

            var items = new ListViewItem[rows.Count];

            for (int i = 0; i < rows.Count; ++i)
            {
                items[i] = new ListViewItem(rows[i].ToArray());
                items[i].UseItemStyleForSubItems = false;
            }

            m_lvData.Items.AddRange(items);
        }

        void UpdateUI()
        {
            Assert(!InvokeRequired);

            m_btnMapColumn.Enabled = m_ndxSelectedColumn >= 0 && m_lbColumns.SelectedIndex >= 0;

            bool canAdvance = true;

            foreach (ColumnMapping ci in m_lbColumns.Items)
                if (ci.DSVIndex == ColumnMapping.NULL_NDX)
                {
                    canAdvance = false;
                    break;
                }

            m_impWizard.CanAdvance = canAdvance;
        }

        bool RowsComparer(string[] row0 , string[] row1)
        {
            if (row0.Length != row1.Length)
                return false;

            for (int i = 0; i < row0.Length; ++i)
                if (string.Compare(row0[i] , row1[i] , true) != 0)
                    return false;

            return true;
        }

        //handlers
        void ParamChanged_EventHandler(object sender , EventArgs e)
        {
            if (m_tbSeparator == sender)
            {
                foreach (ColumnMapping col in m_lbColumns.Items)
                    col.DSVIndex = ColumnMapping.NULL_NDX;

                m_lvData.Clear();
            }
            else
                m_lvData.Items.Clear();

            if (m_tbSeparator.TextLength > 0)
                try
                {
                    LoadData();
                }
                catch
                {
                    m_lvData.Clear();
                }

            UpdateUI();
        }

        private void Data_ColumnClick(object sender , ColumnClickEventArgs e)
        {
            if (m_ndxSelectedColumn != e.Column)
            {
                if (m_ndxSelectedColumn >= 0 && m_ndxSelectedColumn < m_lvData.Columns.Count)
                    foreach (ListViewItem item in m_lvData.Items)
                    {
                        item.SubItems[m_ndxSelectedColumn].BackColor = SystemColors.Window;
                        item.SubItems[m_ndxSelectedColumn].ForeColor = SystemColors.WindowText;
                    }

                foreach (ListViewItem item in m_lvData.Items)
                {
                    item.SubItems[e.Column].BackColor = SystemColors.Highlight;
                    item.SubItems[e.Column].ForeColor = SystemColors.HighlightText;
                }

                m_ndxSelectedColumn = e.Column;

                UpdateUI();
            }
        }

        private void MapColumn_Click(object sender , EventArgs e)
        {
            //clear old links
            var colInfo = m_lbColumns.SelectedItem as ColumnMapping;

            if (colInfo.DSVIndex >= 0)
            {
                Assert(colInfo.DSVIndex < m_lvData.Columns.Count);

                m_lvData.Columns[colInfo.DSVIndex].Tag = null;
                m_lvData.Columns[colInfo.DSVIndex].Text = "?";
            }

            Assert(m_ndxSelectedColumn < m_lvData.Columns.Count);

            ColumnHeader lvColumn = m_lvData.Columns[m_ndxSelectedColumn];
            var oldInfo = lvColumn.Tag as ColumnMapping;

            if (oldInfo != null)
                oldInfo.DSVIndex = ColumnMapping.NULL_NDX;

            //set new links
            lvColumn.Tag = colInfo;
            lvColumn.Text = colInfo.Text;
            colInfo.DSVIndex = m_ndxSelectedColumn;

            UpdateUI();
        }

        private void ImportWizardDialog_EndUpdate(uint[] tablesID)
        {
            if (tablesID.Length > 0)
            {
                var dict = AppContext.Settings.UserSettings.DSVImportSettings.ColumnsMapping;
                dict.Clear();

                //foreach (ColumnHeader col in m_lvData.Columns)
                //{
                //    var colMapping = col.Tag as ColumnMapping;

                //    if (colMapping != null)
                //        dict.Add(colMapping.Key , colMapping.DSVIndex);
                //}

                foreach (ColumnMapping colInfo in m_lbColumns.Items)
                    if (colInfo.DSVIndex != ColumnMapping.NULL_NDX)
                        dict[colInfo.Key] = colInfo.DSVIndex;

            }
        }

        private void ColumnsMapping_DoubleClick(object sender , EventArgs e)
        {
            Point pt = m_lbColumns.PointToClient(Cursor.Position);

            int ndx = m_lbColumns.IndexFromPoint(pt);

            if (ndx != ListBox.NoMatches && m_ndxSelectedColumn >= 0)
                MapColumn_Click(sender , e);
        }
    }
}
