using DGD.Hub.DB;
using DGD.HubCore;
using DGD.HubCore.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;

namespace DGD.Hub.SpotView
{
    partial class SpotView
    {
        class DataLoader
        {
            readonly SpotView m_owner;


            public DataLoader(SpotView owner)
            {
                m_owner = owner;
            }


            public void LoadAutoCompleteSource()
            {
                AutoCompleteStringCollection src = LoadHeadings();

                if (m_owner.InvokeRequired)
                    m_owner.Invoke(new Action(() => m_owner.m_tbSubHeading.AutoCompleteCustomSource = src));
                else
                    m_owner.m_tbSubHeading.AutoCompleteCustomSource = src;
            }

            public void LoadAutoCompleteSourceAsync()
            {
                var task = new Task(LoadAutoCompleteSource);
                task.OnError(OnLoadError);

                task.Start();
            }

            public void LoadIncoterms()
            {
                IEnumerable<Incoterm> icts = from Incoterm ict in Program.TablesManager.GetDataProvider(TablesID.INCOTERM).Enumerate()
                                             orderby ict.Name
                                             select ict;

                if (m_owner.InvokeRequired)
                    m_owner.Invoke(new Action<IEnumerable<Incoterm>>(FillIncorerms) , icts);
                else
                    FillIncorerms(icts);
            }

            public void LoadIncotermsAsync()
            {
                var task = new Task(LoadIncoterms);
                task.OnError(OnLoadError);

                task.Start();
            }

            public void LoadCountries()
            {
                CountryEntry.UseCountryCode = Program.Settings.UseCountryCode;

                IEnumerable<CountryEntry> countries = from Country ctry in Program.TablesManager.GetDataProvider(TablesID.COUNTRY).Enumerate()
                                                      orderby ctry.Name
                                                      select new CountryEntry(ctry);

                if (m_owner.InvokeRequired)
                    m_owner.Invoke(new Action<IEnumerable<CountryEntry>>(FillCountries) , countries);
                else
                    FillCountries(countries);
            }

            public void LoadCountriesAsync()
            {
                var task = new Task(LoadCountries);
                task.OnError(OnLoadError);

                task.Start();
            }



            //private:
            void OnLoadError(Task t) =>
                Log.LogEngin.PushFlash("Erreur lors de la lecture des données: " + t.Exception.InnerException.Message);

            AutoCompleteStringCollection LoadHeadings()
            {
                IDBColumnIndexer<SubHeading> ndxer =
                    Program.TablesManager.GetSubHeadingIndexer(TablesID.SPOT_VALUE , TablesManager.ColumnID_t.SubHeading);

                string[] seq = (from sh in ndxer.Attributes
                                select sh.ToString()).Distinct().ToArray();

                var result = new AutoCompleteStringCollection();
                result.AddRange(seq);

                return result;
            }

            void FillIncorerms(IEnumerable<Incoterm> icts)
            {
                Assert(!m_owner.InvokeRequired);


                var emptyICT = new Incoterm(0 , AppText.UNSPECIFIED);

                m_owner.m_cbIncoterm.Items.Clear();
                m_owner.m_cbIncoterm.Items.Add(emptyICT);
                m_owner.m_cbIncoterm.Items.AddRange(icts.ToArray());
                m_owner.m_cbIncoterm.DisplayMember = "Name";
                m_owner.m_cbIncoterm.SelectedIndex = 0;
            }

            void FillCountries(IEnumerable<CountryEntry> countries)
            {
                Assert(!m_owner.InvokeRequired);

                m_owner.m_cbOrigin.Items.Clear();
                m_owner.m_cbOrigin.Items.Add(new CountryEntry(null));
                m_owner.m_cbOrigin.Items.AddRange(countries.ToArray());

                m_owner.m_cbOrigin.SelectedIndex = 0;
            }
        }
    }
}
