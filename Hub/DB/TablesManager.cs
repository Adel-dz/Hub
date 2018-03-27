using DGD.HubCore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DGD.HubCore;
using static System.Diagnostics.Debug;
using easyLib.Log;
using easyLib;
using System.Text;

namespace DGD.Hub.DB
{

    sealed partial class TablesManager: ITablesManager, IDisposable
    {
        public enum ColumnID_t
        {
            SubHeading
        }

        readonly object m_lock = new object();
        readonly DataAccessPath m_accessPath;
        readonly TablesCollection m_tablesProxy;
        readonly DatumFactory m_dataFactory;
        readonly CountriesTable m_countries;
        readonly CurrenciesTable m_currencies;
        readonly DataSuppliersTable m_suppliers;
        readonly IncotermsTable m_incoterms;
        readonly PlacesTable m_places;
        readonly ProductsTable m_products;
        readonly SharedTextsTable m_sharedTexts;
        readonly SpotValuesTable m_spotValues;
        readonly UnitsTable m_units;
        readonly ValuesContextsTable m_valContexts;

        public event Action<uint> BeginTableProcessing;
        public event Action<uint> EndTableProcessing;

        public TablesManager()
        {
            m_accessPath = new DataAccessPath(this);
            m_tablesProxy = new TablesCollection(this);
            m_dataFactory = new DatumFactory();

            var tablesFolder = SettingsManager.TablesFolder;
            m_suppliers = new DataSuppliersTable(Path.Combine(tablesFolder , "splr"));
            m_units = new UnitsTable(Path.Combine(tablesFolder , "unit"));
            m_countries = new CountriesTable(Path.Combine(tablesFolder , "ctry"));
            m_incoterms = new IncotermsTable(Path.Combine(tablesFolder , "ict"));
            m_places = new PlacesTable(Path.Combine(tablesFolder , "place"));
            m_currencies = new CurrenciesTable(Path.Combine(tablesFolder , "cncy"));
            m_valContexts = new ValuesContextsTable(Path.Combine(tablesFolder , "vctxt"));
            m_products = new ProductsTable(Path.Combine(tablesFolder , "prod"));
            m_sharedTexts = new SharedTextsTable(Path.Combine(tablesFolder , "stxt"));
            m_spotValues = new SpotValuesTable(Path.Combine(tablesFolder , "sval"));
        }


        public IEnumerable<string> TablesFilePath => AllTables.Select(tbl => tbl.FilePath);
        public ITablesCollection Tables => m_tablesProxy;

        public string GetTablePath(uint tblID)
        {
            return GetTable(tblID , false)?.FilePath;
        }

        public IEnumerable<IDBTable> CriticalTables
        {
            get
            {
                yield return ValuesContexts;
                yield return SpotValues;
            }
        }
        public IDatumFactory DataFactory => m_dataFactory;

        public DataSuppliersTable DataSupliers => GetTable(TablesID.SUPPLIER , true) as DataSuppliersTable;
        public UnitsTable Units => GetTable(TablesID.UNIT , true) as UnitsTable;
        public CountriesTable Countries => GetTable(TablesID.COUNTRY , true) as CountriesTable;
        public IncotermsTable Incoterms => GetTable(TablesID.INCOTERM , true) as IncotermsTable;
        public PlacesTable Places => GetTable(TablesID.PLACE , true) as PlacesTable;
        public CurrenciesTable Currencies => GetTable(TablesID.CURRENCY , true) as CurrenciesTable;
        public ValuesContextsTable ValuesContexts => GetTable(TablesID.VALUE_CONTEXT , true) as ValuesContextsTable;
        public ProductsTable Products => GetTable(TablesID.PRODUCT , true) as ProductsTable;
        public SharedTextsTable SharedTexts => GetTable(TablesID.SHARED_TEXT , true) as SharedTextsTable;
        public SpotValuesTable SpotValues => GetTable(TablesID.SPOT_VALUE , true) as SpotValuesTable;

        public void ResizeTable(uint tableID , int szDatum)
        {
            Assert(Tables[tableID] != null);
            Assert(szDatum >= Tables[tableID].DatumSize);

            lock (m_lock)
            {
                IDBTable tbl = GetTable(tableID , true);

                if (tbl.DatumSize < szDatum)
                {
                    string tmpFile = Path.GetTempFileName();
                    using (new AutoReleaser(() => File.Delete(tmpFile)))
                    using (FileStream fs = File.Create(tmpFile))
                    {
                        var tmpWriter = new RawDataWriter(fs , Encoding.UTF8);
                        IDBTableProvider dp = m_accessPath.GetDataProvider(tableID);

                        using (dp.Lock())
                        {
                            foreach (IDataRow row in dp.Enumerate())
                                row.Write(tmpWriter);


                            var tmpReader = new RawDataReader(fs , Encoding.UTF8);
                            int count = dp.Count;

                            BeginTableProcessing?.Invoke(tableID);

                            using (new AutoReleaser(() => EndTableProcessing?.Invoke(tableID)))
                            {
                                uint ver = tbl.Version;
                                uint tag = tbl.Tag;

                                dp.Disconnect();
                                tbl.Disconnect();
                                File.Delete(tbl.FilePath);
                                tbl.Create(szDatum , tag);
                                dp.Connect();

                                fs.Position = 0;

                                for (int i = 0; i < count; ++i)
                                {
                                    IDataRow datum = m_dataFactory.CreateDatum(tableID);
                                    datum.Read(tmpReader);
                                    dp.Insert(datum);
                                }

                                tbl.Version = ver;
                                tbl.Flush();
                            }
                        }
                    }
                }
            }
        }

        public IDBKeyIndexer GetKeyIndexer(uint idTable)
        {
            lock (m_lock)
                return m_accessPath.GetKeyIndexer(idTable);
        }

        public IDBProvider GetDataProvider(uint idTable)
        {
            lock (m_lock)
                return m_accessPath.GetDataProvider(idTable);
        }


        public IDBColumnIndexer<SubHeading> GetSubHeadingIndexer(uint tableID , ColumnID_t columnID)
        {
            lock (m_lock)
                return m_accessPath.GetSubHeadingIndxer(tableID , columnID);
        }

        public void Dispose()
        {
            lock (m_lock)
            {
                m_accessPath.Dispose();

                foreach (IDBTable tbl in AllTables)
                    tbl.Dispose();
            }
        }


        //private:
        IEnumerable<IDBTable> AllTables
        {
            get
            {
                yield return m_suppliers;
                yield return m_units;
                yield return m_countries;
                yield return m_incoterms;
                yield return m_places;
                yield return m_currencies;
                yield return m_valContexts;
                yield return m_products;
                yield return m_sharedTexts;
                yield return m_spotValues;
            }
        }

        IDBTable GetTable(uint tableID , bool connect)
        {
            IDBTable tbl = AllTables.FirstOrDefault(t => t.ID == tableID);

            if (!connect)
                return tbl;

            Dbg.Assert(Program.Settings.ClientInfo != null);

            if (tbl != null)
                if (!tbl.IsConnected)
                    try
                    {
                        tbl.Connect();

                        if (tbl.Tag != Program.Settings.ClientInfo.ClientID)
                            throw new BadTagException(tbl.Name);
                    }
                    catch (FileNotFoundException)
                    {
                        TextLogger.Warning($"Impossible d'ouvrir la table {tbl.Name}\nLancement de la procedure de création...");

                        try
                        {
                            tbl.Create(1 , Program.Settings.ClientInfo.ClientID);
                        }
                        catch (Exception ex)
                        {
                            TextLogger.Error($"Erreur lors de la création du fichier!\n Exception: {ex.Message}");
                            throw;
                        }

                        TextLogger.Info("Création ok");
                    }

            return tbl;
        }
    }
}
