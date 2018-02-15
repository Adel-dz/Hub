using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Opts
{
    partial class SettingsWizard: Form
    {
        public enum SettingPage_t
        {            
            Input = 0,
            Import,
            Connection,
            None = -1
        }

        public SettingsWizard()
        {
            InitializeComponent();


            var items = new ListViewItem[]
            {
                new ListViewItem("Formulaire")
                {
                    Tag = CreatePage<InputSettingsPage>(),
                },
                new ListViewItem("Importation")
                {
                    Tag = CreatePage<ImportSettingsPage>(),
                },
                new ListViewItem("Connexion")
                {
                    Tag = CreatePage<ConnectionSettingsPage>()
                }
            };                


            m_lvSections.Items.AddRange(items);

            ActivePage = SettingPage_t.Input;
        }


        public SettingPage_t ActivePage
        {
            get
            {
                var sel = m_lvSections.SelectedIndices;

                return sel.Count == 0 ? SettingPage_t.None : (SettingPage_t)sel[0];
            }

            set
            {
                SelectPage(value);
            }
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_lvSections.Items[0].Selected = true;

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            var sel = m_lvSections.SelectedItems;

            if(sel.Count == 1)
            {
                var page = sel[0].Tag as ISettingsPage;
                page.Apply();
            }
        }

        //private:
        T CreatePage<T>() where T: UserControl, new()
        {
            var page = new T();
            m_pagePanel.Controls.Add(page);
            page.Visible = false;
            page.Dock = DockStyle.Fill;

            return page;
        }

        void SelectPage(SettingPage_t page)
        {
            if (page == SettingPage_t.None)
                m_lvSections.SelectedIndices.Clear();
            else
                m_lvSections.Items[(int)page].Selected = true;
        }


        //handlers:
        private void Sections_ItemSelectionChanged(object sender , ListViewItemSelectionChangedEventArgs e)
        {
            var page = e.Item.Tag as ISettingsPage;

            if (e.IsSelected)
                page.Show();
            else
            {
                page.Apply();
                page.Hide();
            }
        }
    }
}
