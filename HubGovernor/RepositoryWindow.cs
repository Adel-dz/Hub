using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;
using easyLib.Extensions;
using DGD.HubGovernor.Extensions;

namespace DGD.HubGovernor
{
    public partial class RepositoryWindow: Form
    {
        const string OPT_KEY = "REPOSITORY";
        const string ASK_DELETE = "Vous êtes sur le point de supprimer les éléments sélectionnés, Poursuivre ?";
        readonly Dictionary<uint , IDatumProvider> m_dpCache = new Dictionary<uint , IDatumProvider>();
        KeyIndexer m_ndxerTableGen;


        public RepositoryWindow()
        {
            InitializeComponent();
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_ndxerTableGen = new KeyIndexer(AppContext.TableManager.TablesGeneration.DataProvider);
            m_ndxerTableGen.Connect();

            var tm = AppContext.TableManager.Tables;
            foreach (IDataTable table in AppContext.TableManager.Tables)
            {
                var lvi = new ListViewItem(table.Name , 0)
                {
                    Tag = table
                };

                m_lvTables.Items.Add(lvi);
            }


            m_lvTables.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            var wp = AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY];

            if (wp != null)
                Bounds = wp;

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            foreach (uint key in m_dpCache.Keys)
                m_dpCache[key].Close();

            m_ndxerTableGen.Close();

            var wp = new Opts.WindowPlacement(Bounds);
            AppContext.Settings.UserSettings.WindowPlacement[OPT_KEY] = wp;
        }


        //private:
        void ConnectTableAsync(IDataTable table)
        {
            var waitClue = new Waits.WaitClue(this);


            Action connect = () =>
            {
                if (!table.IsConnected)
                    table.Connect();

                var fuzzyTable = table as IFuzzyDataTable;

                IDatumProvider dp = (fuzzyTable?.RowProvider) ?? table.DataProvider;
                dp.Connect();

                lock (m_dpCache)
                    m_dpCache[table.ID] = dp;

            };

            Action onSuccess = () =>
            {
                waitClue.LeaveWaitMode();
                SetInfo(table);
            };

            Action<Task> onError = t =>
            {
                waitClue.LeaveWaitMode();

                Exception ex = t.Exception.InnerException;
                MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);

                m_lvTables.SelectedItems.Clear();
            };



            waitClue.EnterWaitMode();

            var connecting = new Task(connect , TaskCreationOptions.LongRunning);
            connecting.OnError(onError);
            connecting.OnSuccess(onSuccess);

            connecting.Start();
        }

        void ClearInfo()
        {
            Assert(!InvokeRequired);

            m_lblWriteTime.Text = m_lblRowCount.Text = m_lblGeneration.Text = m_lblFileSize.Text =
                m_lblFileName.Text = m_lblCreateTime.Text = m_lblAccessTime.Text = "-";
        }

        void SetInfo(IDataTable table)
        {
            Assert(!InvokeRequired);

            m_lblAccessTime.Text = table.LastAccessTime.ToString();
            m_lblCreateTime.Text = table.CreationTime.ToString();
            m_lblFileName.Text = Path.GetFileName(table.FilePath);
            m_lblFileSize.Text = FileSizeToString(new FileInfo(table.FilePath).Length);
            m_lblGeneration.Text = GetTableGeneration(table).ToString();
            m_lblWriteTime.Text = table.LastWriteTime.ToString();

            lock (m_dpCache)
                m_lblRowCount.Text = m_dpCache[table.ID].Count.ToString();
        }

        uint GetTableGeneration(IDataTable table)
        {
            return (m_ndxerTableGen.Get(table.ID) as IFileGenerationRow)?.Generation ?? 0;
        }

        static string FileSizeToString(long len)
        {
            string[] units = { "Octets" , "Ko" , "Mo" , "Go" };
            int ndx = 0;

            while ((len / 1024) != 0 && ndx < 3)
            {
                len /= 1024;
                ++ndx;
            }

            return $"{len} {units[ndx]}";
        }

        static void DeleteFuzzyData(IDatumProvider dp , int[] indices)
        {
            if (MessageBox.Show(ASK_DELETE , "Confirmation requise" ,
                    MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int nbDeleted = 0;

                try
                {
                    for (int i = indices.Length - 1; i >= 0; --i)
                    {
                        int ndx = indices[i];
                        IDatum datum = dp.Get(ndx);
                        var item = datum as ITaggedRow;
                        item.Disabled = true;
                        dp.Replace(ndx , datum);
                        ++nbDeleted;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }

                EventLogger.Info(string.Format("{0} élément(s) supprimé(s)." , nbDeleted));
            }
        }

        static void DeleteFarmedData(IDatumProvider dp , int[] indices)
        {
            if (MessageBox.Show(ASK_DELETE , "Confirmation requise" ,
                    MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int nbDeleted = 0;

                try
                {
                    for (int i = indices.Length - 1; i >= 0; --i)
                    {
                        int ndx = indices[i];
                        dp.Delete(ndx);
                        ++nbDeleted;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }

                EventLogger.Info(string.Format("{0} élément(s) supprimé(s)." , nbDeleted));
            }
        }


        //handlers
        private void Tables_SelectedIndexChanged(object sender , EventArgs e)
        {
            var sel = m_lvTables.SelectedItems;

            if (sel.Count == 1)
            {
                var table = sel[0].Tag as IDataTable;
                IDatumProvider dataProvider;

                lock (m_dpCache)
                    m_dpCache.TryGetValue(table.ID , out dataProvider);

                if (dataProvider != null)
                    try
                    {
                        SetInfo(table);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    }
                else
                    ConnectTableAsync(table);
            }
            else
                ClearInfo();
        }

        private void Tables_ItemActivate(object sender , EventArgs e)
        {
            var sel = m_lvTables.SelectedItems;

            if (sel.Count != 1)
                return;

            var table = sel[0].Tag as IDataTable;
            var fuzzyTable = table as IFuzzyDataTable;
            IDatumProvider dataProvider;

            if (fuzzyTable != null)
                dataProvider = fuzzyTable.RowProvider;
            else
                dataProvider = table.DataProvider;

            uint idTable = table.ID;

            Action<IDatumProvider , IDatum> editHandler = (dp , d) =>
            {
                Form frm = TableFormFactory.CreateFrom(idTable , dp , d);
                frm.Show(Owner);
            };

            Action<IDatumProvider> addHandler = dp =>
            {
                Form form = TableFormFactory.CreateFrom(idTable , dp);
                form.Show(Owner);
            };


            try
            {
                var view = new DatumView(dataProvider , table.Name);

                if (TableFormFactory.IsEditingEnabled(idTable))
                    view.EditClicked += editHandler;

                if (TableFormFactory.IsAddingEnabled(idTable))
                    view.AddClicked += addHandler;

                if (TableFormFactory.IsDeletingEnabled(idTable))
                    if (fuzzyTable == null)
                        view.DeleteClicked += DeleteFarmedData;
                    else
                        view.DeleteClicked += DeleteFuzzyData;

                view.Show(Owner);
            }
            catch (Exception ex)
            {
                Owner.ShowError(ex.Message);
            }
        }
    }
}
