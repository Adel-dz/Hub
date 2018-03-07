using System.Collections.Generic;
using System.Linq;
using easyLib.DB;
using System.IO;
using static System.Diagnostics.Debug;

namespace DGD.HubGovernor.DB
{
    sealed class TableManager
    {
        readonly Countries.CountryTable m_countries;
        readonly Currencies.CurrencyTable m_currencies;
        readonly FilesGen.FileGenerationTable m_filesGen;
        readonly Places.PlaceTable m_places;
        readonly Products.ProductTable m_products;
        readonly Suppliers.DataSupplierTable m_suppliers;
        readonly Units.UnitTable m_units;
        readonly Spots.SpotValueTable m_spotValues;
        readonly Incoterms.IncotermTable m_incoterms;
        readonly VContext.ValueContextTable m_valuesContxt;
        readonly Updating.TransactionTable m_transactions;
        readonly Updating.UpdateIncrementTable m_dataUpdates;
        readonly TR.TRLabelTable m_trLabels;
        readonly TR.ProductMappingTable m_trProdMapping;
        readonly TR.SpotValueTable m_trSpotValues;
        readonly Strings.SharedTextTable m_sharedTexts;
        readonly TR.LabelMappingTable m_trLabelMapping;
        readonly Profiles.UserProfileTable m_profiles;
        readonly Clients.HubClientTable m_clients;
        readonly Clients.ClientStatusTable m_clientsStatus;
        readonly Profiles.ProfileManagementModeTable m_profilesMgmnt;
        readonly Updating.AppUpdateTable m_appUpdates;



        public TableManager()
        {
            m_countries = new Countries.CountryTable(Path.Combine(AppPaths.TablesFolder , "countries.dt"));            
            m_currencies = new Currencies.CurrencyTable(Path.Combine(AppPaths.TablesFolder , "currencies.dt"));
            m_filesGen = new FilesGen.FileGenerationTable(Path.Combine(AppPaths.TablesFolder , "fgen.dt"));
            m_places = new Places.PlaceTable(Path.Combine(AppPaths.TablesFolder , "places.dt"));
            m_products = new Products.ProductTable(Path.Combine(AppPaths.TablesFolder , "products.dt"));
            m_suppliers = new Suppliers.DataSupplierTable(Path.Combine(AppPaths.TablesFolder , "suppliers.dt"));
            m_units = new Units.UnitTable(Path.Combine(AppPaths.TablesFolder , "units.dt"));
            m_spotValues = new Spots.SpotValueTable(Path.Combine(AppPaths.TablesFolder , "spotvals.dt"));
            m_incoterms = new Incoterms.IncotermTable(Path.Combine(AppPaths.TablesFolder , "icterms.dt"));
            m_valuesContxt = new VContext.ValueContextTable(Path.Combine(AppPaths.TablesFolder , "vctxt.dt"));
            m_transactions = new Updating.TransactionTable(Path.Combine(AppPaths.TablesFolder , "transactions.dt"));
            m_trLabels = new TR.TRLabelTable(Path.Combine(AppPaths.TablesFolder , "trstr.dt"));
            m_trProdMapping = new TR.ProductMappingTable(Path.Combine(AppPaths.TablesFolder , "trprodmapping.dt"));
            m_dataUpdates = new Updating.UpdateIncrementTable(Path.Combine(AppPaths.TablesFolder , "incr.dt"));
            m_trSpotValues = new TR.SpotValueTable(Path.Combine(AppPaths.TablesFolder , "trspot.dt"));
            m_sharedTexts = new Strings.SharedTextTable(Path.Combine(AppPaths.TablesFolder , "shtxt.dt"));
            m_trLabelMapping = new TR.LabelMappingTable(Path.Combine(AppPaths.TablesFolder , "trlblmap.dt"));
            m_profiles = new Profiles.UserProfileTable(Path.Combine(AppPaths.TablesFolder , "profiles.dt"));
            m_clients = new Clients.HubClientTable(Path.Combine(AppPaths.TablesFolder , "clients.dt"));
            m_clientsStatus = new Clients.ClientStatusTable(Path.Combine(AppPaths.TablesFolder , "clstatus.dt"));
            m_profilesMgmnt = new Profiles.ProfileManagementModeTable(Path.Combine(AppPaths.TablesFolder , "prfmgmnt.dt"));
            m_appUpdates = new Updating.AppUpdateTable(Path.Combine(AppPaths.TablesFolder , "clupdate.dt"));
        }
        

        public IEnumerable<IDataTable> Tables => DeployableTables.Concat(InternalTables);

        public IEnumerable<IDataTable> DeployableTables
        {
            get
            {
                yield return m_countries;
                yield return m_currencies;
                yield return m_places;
                yield return m_products;
                yield return m_suppliers;
                yield return m_units;
                yield return m_spotValues;
                yield return m_incoterms;
                yield return m_valuesContxt;
                yield return m_sharedTexts;
            }
        }


        public Countries.CountryTable Countries => m_countries;
        public Currencies.CurrencyTable Currencies => m_currencies;
        public FilesGen.FileGenerationTable TablesGeneration => m_filesGen;
        public Places.PlaceTable Places => m_places;
        public Products.ProductTable Products => m_products;
        public Suppliers.DataSupplierTable Suppiers => m_suppliers;
        public Units.UnitTable Units => m_units;
        public Spots.SpotValueTable SpotValues => m_spotValues;
        public Incoterms.IncotermTable Incoterms => m_incoterms;
        public VContext.ValueContextTable ValuesContext => m_valuesContxt;
        public Updating.TransactionTable Transactions => m_transactions;
        public Updating.UpdateIncrementTable DataUpdates => m_dataUpdates;
        public TR.TRLabelTable TRLabels => m_trLabels;
        public TR.ProductMappingTable TRProductsMapping => m_trProdMapping;
        public TR.SpotValueTable TRSpotValues => m_trSpotValues;
        public Strings.SharedTextTable SharedTexts => m_sharedTexts;
        public TR.LabelMappingTable TRLabelsMapping => m_trLabelMapping;
        public Profiles.UserProfileTable Profiles => m_profiles;
        public Clients.HubClientTable HubClients => m_clients;
        public Clients.ClientStatusTable ClientsStatus => m_clientsStatus;
        public Profiles.ProfileManagementModeTable ProfileManagementMode => m_profilesMgmnt;
        public Updating.AppUpdateTable AppUpdates => m_appUpdates;


        public void Dispose()
        {
            easyLib.DebugHelper.AssertAll();

            foreach (IDataTable table in Tables)
                table.Dispose();
        }

        public IDataTable GetTable(uint idTable)
        {
            foreach (IDataTable table in Tables)
                if (table.ID == idTable)
                    return table;

            return null;
        }

        public IEnumerable<uint> GetRelatedTables(uint idTable)
        {
            Assert(Tables.Any(t => t.ID == idTable));

            return (GetTable(idTable) as ITableRelation).RelatedTables;
        }

        public uint GetTableGeneration(uint idTable)
        {
            using (IDatumProvider dp = TablesGeneration.DataProvider)
            {
                dp.Connect();

                if (dp.Count > 0)
                    foreach (FilesGen.FileGeneration fg in dp.Enumerate())
                        if (fg.ID == idTable)
                            return fg.Generation;

                return 0;
            }
        }

        public void SetTableGeneration(uint idTable, uint gen)
        {
            using (IDatumProvider dp = TablesGeneration.DataProvider)
            {
                dp.Connect();


                for (int ndx = 0; ndx < dp.Count; ++ndx)
                {
                    var fg = dp.Get(ndx) as FilesGen.FileGeneration;

                    if(fg.ID == idTable)
                    {
                        fg.Generation = gen;
                        dp.Replace(ndx , fg);
                        return;
                    }
                }


                var fileGen = new FilesGen.FileGeneration(idTable , gen);
                dp.Insert(fileGen);
            }

        }

        //private:
        IEnumerable<IDataTable> InternalTables
        {
            get
            {
                yield return m_filesGen;
                yield return m_transactions;
                yield return m_dataUpdates;
                yield return m_trLabels;
                yield return m_trProdMapping;
                yield return m_trSpotValues;
                yield return m_trLabelMapping;
                yield return m_profiles;
                yield return m_clients;
                yield return m_clientsStatus;
                yield return m_profilesMgmnt;
                yield return m_appUpdates;
            }
        }
    }
}
