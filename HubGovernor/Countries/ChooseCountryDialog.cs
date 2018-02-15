using easyLib.DB;
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


namespace DGD.HubGovernor.Countries
{
    sealed partial class ChooseCountryDialog: Form
    {
        readonly IDatumProvider m_dpCountries;


        public ChooseCountryDialog(IDatumProvider provider)
        {
            Assert(provider != null);

            InitializeComponent();

            m_dpCountries = provider;
            m_dpCountries.Connect();

            m_cbCountries.DisplayMember = "Name";
            m_cbCountries.Items.AddRange(m_dpCountries.Enumerate().ToArray());
        }


        public Country SelectedCountry
        {
            get
            {
                return m_cbCountries.SelectedItem as Country;
            }

            set
            {
                foreach (Country ctry in m_cbCountries.Items)
                    if (ctry.ID == value.ID)
                    {
                        m_cbCountries.SelectedItem = ctry;
                        break;
                    }
            }
        }


        //protected:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            m_dpCountries.Close();
        }


        //private:


        //hadlers:
        private void Add_Click(object sender , EventArgs e)
        {
            using (var ctryForm = new CountryForm(m_dpCountries))
            {
                ctryForm.ShowDialog(this);

                object selItem = m_cbCountries.SelectedItem;
                m_cbCountries.Items.Clear();

                m_cbCountries.Items.AddRange(m_dpCountries.Enumerate().ToArray());

                if (selItem != null)
                    m_cbCountries.SelectedItem = selItem;
            }
        }

        private void Countries_SelectedIndexChanged(object sender , EventArgs e)
        {
            m_btnOK.Enabled = m_cbCountries.SelectedItem != null;
        }
    }
}
