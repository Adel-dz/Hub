using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubGovernor.Countries;
using DGD.HubGovernor.Currencies;
using DGD.HubGovernor.Incoterms;
using DGD.HubGovernor.Places;
using DGD.HubGovernor.Products;
using DGD.HubGovernor.Suppliers;
using DGD.HubGovernor.Units;
using DGD.HubGovernor.VContext;
using easyLib.DB;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR.Imp
{
    sealed class ImportEngin: IDisposable
    {
        static readonly Country EmptyCountry = new Country();
        static readonly Incoterm EmptyIncoterm = new Incoterm();
        static readonly Place EmptyPlace = new Place();
        static readonly string[] m_ictNames = { "EXW" , "FCA" , "FOB" , "FAS" };
        readonly string[][] m_srcData;
        readonly IDictionary<ColumnKey_t , int> m_colMapping;
        readonly List<int> m_ignoredRows = new List<int>();
        readonly Dictionary<uint , List<IDataRow>> m_importedData = new Dictionary<uint, List<IDataRow>>();
        readonly Dictionary<uint , IDataRow> m_pendingData = new Dictionary<uint, IDataRow>();
        AttrIndexer<uint> m_ndxerContextOriginID;
        AttrIndexer<ushort> m_ndxerCountryInternalCode;
        AttrIndexer<string> m_ndxCurrencyName;
        AttrIndexer<string> m_ndxerIncotermName;
        AttrIndexer<uint> m_ndxerMappingProdID;
        AttrIndexer<uint> m_ndxerMappingProdNber;
        AttrIndexer<string> m_ndxerPlaceName;
        AttrIndexer<SubHeading> m_ndxerProductSubHeading;
        AttrIndexer<uint> m_ndxerSpotValueMappingID;
        AttrIndexer<uint> m_ndxerTRLabelProdNber;
        AttrIndexer<string> m_ndxerUnitName;
        IDatumProvider m_dpContext;
        IDatumProvider m_dpCountries;
        IDatumProvider m_dpCurrencies;
        IDatumProvider m_dpIncoterms;
        IDatumProvider m_dpProdMapping;
        IDatumProvider m_dpPlaces;
        IDatumProvider m_dpProducts;
        IDatumProvider m_dpSpotValues;
        IDatumProvider m_dpTRLabels;
        IDatumProvider m_dpUnits;       


        public ImportEngin(string[][] rows , IDictionary<ColumnKey_t , int> colMapping)
        {
            Assert(rows != null);
            Assert(colMapping != null);
            Assert(Enum.GetValues(typeof(ColumnKey_t)).AsEnumerable<ColumnKey_t>().All(e =>
                colMapping.ContainsKey(e)));

            m_srcData = rows;
            m_colMapping = colMapping;

            foreach (IDataTable table in AppContext.TableManager.Tables)
                m_importedData[table.ID] = new List<IDataRow>();
        }

        
        public bool IsDisposed { get; private set; }

        public IDictionary<uint , List<IDataRow>> ImportedData => m_importedData;
        public List<int> IgnoredRows => m_ignoredRows;
        

        public void Run()
        {
            Assert(!IsDisposed);


            m_pendingData.Clear();
            m_ignoredRows.Clear();

            foreach (List<IDataRow> lst in m_importedData.Values)
                lst.Clear();
            
            SetupSupplier();

            var logger = new EventLogger(LogSeverity.Error);

            for(int i = 0; i < m_srcData.Length;++i)
            {
                if (ParseSpotValue(i) == null)
                {
                    m_ignoredRows.Add(i);
                    logger.PutLine($"La ligne N° {i + 1} a été ignorée.");
                }
                else
                    foreach (uint idTable in m_pendingData.Keys)
                        m_importedData[idTable].Add(m_pendingData[idTable]);

                m_pendingData.Clear();
            }
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_ndxerContextOriginID?.Close();
                m_ndxerCountryInternalCode?.Close();
                m_ndxCurrencyName?.Close();
                m_ndxerIncotermName?.Close();
                m_ndxerMappingProdID?.Close();
                m_ndxerMappingProdNber?.Close();
                m_ndxerPlaceName?.Close();
                m_ndxerProductSubHeading?.Close();
                m_ndxerSpotValueMappingID?.Close();
                m_ndxerTRLabelProdNber?.Close();
                m_ndxerUnitName?.Close();
                m_dpContext?.Close();
                m_dpCountries?.Close();
                m_dpCurrencies?.Close();
                m_dpIncoterms?.Close();
                m_dpProdMapping?.Close();
                m_dpPlaces?.Close();
                m_dpProducts?.Close();
                m_dpSpotValues?.Close();
                m_dpTRLabels?.Close();
                m_dpUnits?.Close();

                IsDisposed = true;
            }
        }


        //private:
        string AdjustInput(string input)
        {
            Opts.StringTransform_t opt = AppContext.Settings.AppSettings.ImportTransform;

            input = input.Trim();

            return opt == Opts.StringTransform_t.LowerCase ? input.ToLower() :
                opt == Opts.StringTransform_t.UpperCase ? input.ToUpper() : input;
        }

        IDatumProvider SpotValuesProvider
        {
            get
            {
                if (m_dpSpotValues == null)
                {
                    m_dpSpotValues = AppContext.TableManager.TRSpotValues.DataProvider;
                    m_dpSpotValues.Connect();
                }

                return m_dpSpotValues;
            }
        }

        IDatumProvider TRLabelsProvider
        {
            get
            {
                if (m_dpTRLabels == null)
                {
                    m_dpTRLabels = AppContext.TableManager.TRLabels.DataProvider;
                    m_dpTRLabels.Connect();
                }

                return m_dpTRLabels;
            }
        }

        IDatumProvider ProductsMappingProvider
        {
            get
            {
                if (m_dpProdMapping == null)
                {
                    m_dpProdMapping = AppContext.TableManager.TRProductsMapping.DataProvider;
                    m_dpProdMapping.Connect();
                }

                return m_dpProdMapping;
            }
        }

        IDatumProvider ValuesContextProvider
        {
            get
            {
                if (m_dpContext == null)
                {
                    m_dpContext = AppContext.TableManager.ValuesContext.DataProvider;
                    m_dpContext.Connect();
                }

                return m_dpContext;
            }
        }

        IDatumProvider ProductsProvider
        {
            get
            {
                if (m_dpProducts == null)
                {
                    m_dpProducts = AppContext.TableManager.Products.RowProvider;
                    m_dpProducts.Connect();
                }

                return m_dpProducts;
            }
        }

        IDatumProvider PlacesProvider
        {
            get
            {
                if (m_dpPlaces == null)
                {
                    m_dpPlaces = AppContext.TableManager.Places.RowProvider;
                    m_dpPlaces.Connect();
                }

                return m_dpPlaces;
            }
        }

        IDatumProvider IncotermsProvider
        {
            get
            {
                if (m_dpIncoterms == null)
                {
                    m_dpIncoterms = AppContext.TableManager.Incoterms.RowProvider;
                    m_dpIncoterms.Connect();
                }

                return m_dpIncoterms;
            }
        }

        IDatumProvider UnitsProvider
        {
            get
            {
                if (m_dpUnits == null)
                {
                    m_dpUnits = AppContext.TableManager.Units.RowProvider;
                    m_dpUnits.Connect();
                }

                return m_dpUnits;
            }
        }

        IDatumProvider CurrenciesProvider
        {
            get
            {
                if (m_dpCurrencies == null)
                {
                    m_dpCurrencies = AppContext.TableManager.Currencies.RowProvider;
                    m_dpCurrencies.Connect();
                }

                return m_dpCurrencies;
            }
        }

        IDatumProvider CountriesProvider
        {
            get
            {
                if (m_dpCountries == null)
                {
                    m_dpCountries = AppContext.TableManager.Countries.RowProvider;
                    m_dpCountries.Connect();
                }

                return m_dpCountries;
            }
        }

        AttrIndexer<uint> SpotValueMappingIDIndexer
        {
            get
            {
                if (m_ndxerSpotValueMappingID == null)
                {
                    m_ndxerSpotValueMappingID = new AttrIndexer<uint>(SpotValuesProvider , d => (d as SpotValue).ProductMappingID);
                    m_ndxerSpotValueMappingID.Connect();
                }

                return m_ndxerSpotValueMappingID;
            }
        }

        AttrIndexer<uint> TRLabelProdNberIndexer
        {
            get
            {
                if (m_ndxerTRLabelProdNber == null)
                {
                    m_ndxerTRLabelProdNber = new AttrIndexer<uint>(TRLabelsProvider , d => (d as TRLabel).ProductNumber);
                    m_ndxerTRLabelProdNber.Connect();
                }

                return m_ndxerTRLabelProdNber;
            }
        }

        AttrIndexer<uint> ValueContextOriginIDIndexer
        {
            get
            {
                if (m_ndxerContextOriginID == null)
                {
                    m_ndxerContextOriginID = new AttrIndexer<uint>(ValuesContextProvider , d => (d as ValueContext).OriginID);
                    m_ndxerContextOriginID.Connect();
                }

                return m_ndxerContextOriginID;
            }
        }

        AttrIndexer<uint> ProdMappingProductIDIndexer
        {
            get
            {
                if (m_ndxerMappingProdID == null)
                {
                    m_ndxerMappingProdID = new AttrIndexer<uint>(ProductsMappingProvider , d => (d as ProductMapping).ProductID);
                    m_ndxerMappingProdID.Connect();
                }

                return m_ndxerMappingProdID;
            }
        }

        AttrIndexer<uint> ProdMappingProdNberIndexer
        {
            get
            {
                if (m_ndxerMappingProdNber == null)
                {
                    m_ndxerMappingProdNber = new AttrIndexer<uint>(ProductsMappingProvider , d => (d as ProductMapping).ProductNumber);
                    m_ndxerMappingProdNber.Connect();
                }

                return m_ndxerMappingProdNber;
            }
        }
        
        AttrIndexer<SubHeading> ProductSubHeadingIndexer
        {
            get
            {
                if (m_ndxerProductSubHeading == null)
                {
                    m_ndxerProductSubHeading = new AttrIndexer<SubHeading>(ProductsProvider , d => (d as Product).SubHeading);
                    m_ndxerProductSubHeading.Connect();
                }

                return m_ndxerProductSubHeading;
            }
        }

        AttrIndexer<string> PlaceNameIndexer
        {
            get
            {
                if (m_ndxerPlaceName == null)
                {
                    m_ndxerPlaceName = new AttrIndexer<string>(PlacesProvider , d => (d as Place).Name ,
                        StringComparer.CurrentCultureIgnoreCase);

                    m_ndxerPlaceName.Connect();
                }

                return m_ndxerPlaceName;
            }
        }

        AttrIndexer<string> UnitNameIndexer
        {
            get
            {
                if (m_ndxerUnitName == null)
                {
                    m_ndxerUnitName = new AttrIndexer<string>(UnitsProvider ,
                        d => (d as Unit).Name ,
                        StringComparer.CurrentCultureIgnoreCase);

                    m_ndxerUnitName.Connect();
                }

                return m_ndxerUnitName;
            }
        }

        AttrIndexer<string> IncotermNameIndexer
        {
            get
            {
                if (m_ndxerIncotermName == null)
                {
                    m_ndxerIncotermName = new AttrIndexer<string>(IncotermsProvider ,
                        d => (d as Incoterm).Name ,
                        StringComparer.CurrentCultureIgnoreCase);

                    m_ndxerIncotermName.Connect();
                }

                return m_ndxerIncotermName;
            }
        }

        AttrIndexer<ushort> CountryInternalCodeIndexer
        {
            get
            {
                if (m_ndxerCountryInternalCode == null)
                {
                    m_ndxerCountryInternalCode = new AttrIndexer<ushort>(CountriesProvider , d => (d as Country).InternalCode);

                    m_ndxerCountryInternalCode.Connect();
                }

                return m_ndxerCountryInternalCode;
            }
        }

        AttrIndexer<string> CurrencyNameIndexer
        {
            get
            {
                if (m_ndxCurrencyName == null)
                {
                    m_ndxCurrencyName = new AttrIndexer<string>(CurrenciesProvider ,
                        d => (d as Currency).Name ,
                        StringComparer.CurrentCultureIgnoreCase);

                    m_ndxCurrencyName.Connect();
                }

                return m_ndxCurrencyName;
            }
        }
        
        SpotValue FindSpotValue(uint mappingID , DateTime tm)
        {
            SpotValue sv = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.TRSpotValues.ID , out data))
                sv = data.Cast<SpotValue>().SingleOrDefault(d => d.ProductMappingID == mappingID && d.Time == tm);

            if (sv == null)
                sv = SpotValueMappingIDIndexer.Get(mappingID).Cast<SpotValue>().SingleOrDefault(d => d.Time == tm);

            return sv;
        }

        TRLabel FindLabel(uint prodNber , string label)
        {
            TRLabel lbl = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.TRLabels.ID , out data))
                lbl = data.Cast<TRLabel>().SingleOrDefault(l => prodNber == l.ProductNumber &&
                    string.Compare(l.Label , label , true) == 0);

            if (lbl == null)
                lbl = TRLabelProdNberIndexer.Get(prodNber).Cast<TRLabel>().SingleOrDefault(l =>
                        string.Compare(l.Label , label , true) == 0);

            return lbl;
        }

        ProductMapping FindProductMapping(uint prodNber)
        {
            ProductMapping pm = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.TRProductsMapping.ID , out data))
                pm = data.Cast<ProductMapping>().SingleOrDefault(p => p.ProductNumber == prodNber);

            if (pm == null)
                pm = ProdMappingProdNberIndexer.Get(prodNber).Cast<ProductMapping>().SingleOrDefault();

            return pm;
        }

        ProductMapping FindProductMapping(uint prodID , uint ctxtID)
        {
            ProductMapping pm = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.TRProductsMapping.ID , out data))
                pm = data.Cast<ProductMapping>().SingleOrDefault(p => p.ProductID == prodID && p.ContextID == ctxtID);

            if (pm == null)
                pm = ProdMappingProductIDIndexer.Get(prodID).Cast<ProductMapping>().SingleOrDefault(p => p.ContextID == ctxtID);

            return pm;
        }

        ValueContext FindValueContext(uint cncyID , uint unitID , uint origID , uint ictID , uint placeID)
        {
            ValueContext vc = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.ValuesContext.ID , out data))
                vc = (from ValueContext d in data
                      where d.CurrencyID == cncyID &&
                        d.UnitID == unitID &&
                        d.OriginID == origID &&
                        d.IncotermID == ictID &&
                        d.PlaceID == placeID
                      select d).SingleOrDefault();

            if (vc == null)
                vc = (from ValueContext d in ValueContextOriginIDIndexer.Get(origID)
                      where d.CurrencyID == cncyID &&
                         d.UnitID == unitID &&
                         d.IncotermID == ictID &&
                         d.PlaceID == placeID
                      select d).SingleOrDefault();


            return vc;
        }

        Product FindProduct(SubHeading sh , string label)
        {
            Product prod = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Products.ID , out data))
                prod = data.Cast<Product>().SingleOrDefault(p =>
                    p.SubHeading.Equals(sh) && string.Compare(p.Name , label , true) == 0);

            if (prod == null)
                prod = ProductSubHeadingIndexer.Get(sh).Cast<Product>().SingleOrDefault(p =>
                    string.Compare(p.Name , label , true) == 0);

            return prod;
        }

        Unit FindUnit(string name)
        {
            Unit u = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Units.ID , out data))
                u = data.Cast<Unit>().SingleOrDefault(d => string.Compare(d.Name , name , true) == 0);

            if (u == null)
                u = UnitNameIndexer.Get(name).SingleOrDefault() as Unit;

            return u;
        }

        Currency FindCurrency(string name)
        {
            Currency cncy = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Currencies.ID , out data))
                cncy = data.Cast<Currency>().SingleOrDefault(c => string.Compare(c.Name , name , true) == 0);

            if (cncy == null)
                cncy = CurrencyNameIndexer.Get(name).SingleOrDefault() as Currency;

            return cncy;
        }

        Incoterm FindIncoterm(string name)
        {
            Incoterm ict = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Incoterms.ID , out data))
                ict = data.Cast<Incoterm>().SingleOrDefault(d => string.Compare(d.Name , name , true) == 0);

            if (ict == null)
                ict = IncotermNameIndexer.Get(name).SingleOrDefault() as Incoterm;

            return ict;
        }

        Country FindCountry(ushort code)
        {
            //search in imported data + db

            Country cntry = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Countries.ID , out data))
                cntry = data.Cast<Country>().SingleOrDefault(c => c.InternalCode == code);

            if (cntry == null)
                cntry = CountryInternalCodeIndexer.Get(code).Cast<Country>().SingleOrDefault();

            return cntry;
        }

        IEnumerable<Place> FindPlace(string name)
        {
            IEnumerable<Place> places = null;
            List<IDataRow> data;

            if (m_importedData.TryGetValue(AppContext.TableManager.Places.ID , out data))
                places = data.Cast<Place>().Where(p => string.Compare(p.Name , name , true) == 0);

            if (!places.Any())
                places = PlaceNameIndexer.Get(name).Cast<Place>();

            return places;
        }

        Product ParseProduct(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colName = m_colMapping[ColumnKey_t.LabelFr];
            int colSubHeading = m_colMapping[ColumnKey_t.SubHeading];

            //parsing
            string name = AdjustInput(row[colName]);
            SubHeading sh = SubHeading.Parse(row[colSubHeading].Trim());

            if (string.IsNullOrWhiteSpace(name) || sh == null)
                return null;

            Product prod = FindProduct(sh , name);

            if (prod == null)
            {
                prod = new Product(AppContext.TableManager.Products.CreateUniqID() , name , sh);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.Products.ID));
                m_pendingData[AppContext.TableManager.Products.ID] = prod;
            }

            return prod;
        }

        Country ParseOrigin(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colCode = m_colMapping[ColumnKey_t.Origin];
            string strCode = row[colCode].Trim();

            if (string.IsNullOrWhiteSpace(strCode))
                return EmptyCountry;

            ushort code;

            if (!ushort.TryParse(strCode , out code) || code == 0)
                return null;


            Country cntry = FindCountry(code);

            if (cntry == null)
            {
                cntry = new Country(AppContext.TableManager.Countries.CreateUniqID() , $"PAYS_{code}" , code);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.Countries.ID));
                m_pendingData[AppContext.TableManager.Countries.ID] = cntry;
            }

            return cntry;
        }

        Currency ParseCurrency(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colName = m_colMapping[ColumnKey_t.Currency];
            string name = AdjustInput(row[colName]);

            if (string.IsNullOrWhiteSpace(name))
                return null;

            Currency cncy = FindCurrency(name);

            if (cncy == null)
            {
                cncy = new Currency(AppContext.TableManager.Currencies.CreateUniqID() , name);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.Currencies.ID));
                m_pendingData[AppContext.TableManager.Currencies.ID] = cncy;
            }

            return cncy;
        }

        Unit ParseUnit(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colName = m_colMapping[ColumnKey_t.Unit];
            string name = AdjustInput(row[colName]);

            if (string.IsNullOrWhiteSpace(name))
                return null;

            Unit u = FindUnit(name);

            if (u == null)
            {
                u = new Unit(AppContext.TableManager.Units.CreateUniqID() , name);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.Units.ID));
                m_pendingData[AppContext.TableManager.Units.ID] = u;
            }

            return u;
        }

        Incoterm ParseIncoterm(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colName = m_colMapping[ColumnKey_t.Incoterm];
            string name = AdjustInput(row[colName]);

            if (string.IsNullOrWhiteSpace(name))
                return EmptyIncoterm;

            Incoterm ict = FindIncoterm(name);

            if (ict == null)
            {
                ict = new Incoterm(AppContext.TableManager.Incoterms.CreateUniqID() , name);

                Assert(m_pendingData.ContainsKey(AppContext.TableManager.Incoterms.ID) == false);
                m_pendingData[AppContext.TableManager.Incoterms.ID] = ict;
            }

            return ict;
        }

        Place ParsePlace(int ndxRow , Incoterm curIncoterm , Country curOrigin)
        {
            string[] row = m_srcData[ndxRow];
            int colName = m_colMapping[ColumnKey_t.Place];
            string name = AdjustInput(row[colName]);

            if (string.IsNullOrWhiteSpace(name))
                return EmptyPlace;

            IEnumerable<Place> places = FindPlace(name);
            Place result;

            if (places.Count() == 1)
                result = places.Single();
            else 
            {
                uint ctryID = 0;

                if (curOrigin != null && curIncoterm != null)
                    foreach (string s in m_ictNames)
                        if (string.Compare(curIncoterm.Name , s , true) == 0)
                        {
                            ctryID = curOrigin.ID;
                            break;
                        }


                result = new Place(AppContext.TableManager.Places.CreateUniqID() , name , ctryID);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.Places.ID));
                m_pendingData[AppContext.TableManager.Places.ID] = result;
            }

            return result;
        }

        ValueContext ParseValueContext(int ndxRow)
        {
            Currency cncy;
            Unit u;
            Country orig;
            Incoterm ict;
            Place p;

            if ((orig = ParseOrigin(ndxRow)) == null ||
                    (cncy = ParseCurrency(ndxRow)) == null ||
                    (u = ParseUnit(ndxRow)) == null ||
                    (ict = ParseIncoterm(ndxRow)) == null ||
                    (p = ParsePlace(ndxRow , ict , orig)) == null)
                return null;


            ValueContext vc = FindValueContext(cncy.ID , u.ID , orig.ID , ict.ID , p.ID);

            if (vc == null)
            {
                vc = new ValueContext(AppContext.TableManager.ValuesContext.CreateUniqID() , cncy.ID , u.ID , orig.ID , ict.ID , p.ID);

                Assert(m_pendingData.ContainsKey(AppContext.TableManager.ValuesContext.ID) == false);
                m_pendingData[AppContext.TableManager.ValuesContext.ID] = vc;
            }

            return vc;
        }

        ProductMapping ParseProductMapping(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colProdNber = m_colMapping[ColumnKey_t.ProductNber];
            string strProdNber = row[colProdNber].Trim();
            uint prodNber;
            Product prod;
            ValueContext vc;


            if (!uint.TryParse(strProdNber , out prodNber) ||
                (prod = ParseProduct(ndxRow)) == null ||
                (vc = ParseValueContext(ndxRow)) == null)
                return null;

            ProductMapping pm = FindProductMapping(prodNber);

            if (pm == null)
            {
                if (FindProductMapping(prod.ID , vc.ID) == null)
                {
                    pm = new ProductMapping(AppContext.TableManager.TRProductsMapping.CreateUniqID() , prodNber , prod.ID , vc.ID);

                    Assert(m_pendingData.ContainsKey(AppContext.TableManager.TRProductsMapping.ID) == false);
                    m_pendingData[AppContext.TableManager.TRProductsMapping.ID] = pm;
                }
            }
            else if (pm.ContextID != vc.ID || pm.ProductID != prod.ID)
                return null;

            return pm;
        }

        TRLabel ParseLabel(int ndxRow , uint prodNber)
        {
            string[] row = m_srcData[ndxRow];
            int colLabel = m_colMapping[ColumnKey_t.LabelUs];
            string str = AdjustInput(row[colLabel]);


            if (string.IsNullOrWhiteSpace(str))
                return null;

            TRLabel lbl = FindLabel(prodNber , str);

            if (lbl == null)
            {
                lbl = new TRLabel(AppContext.TableManager.TRLabels.CreateUniqID() , prodNber , str);

                Assert(!m_pendingData.ContainsKey(AppContext.TableManager.TRLabels.ID));
                m_pendingData[AppContext.TableManager.TRLabels.ID] = lbl;
            }

            return lbl;
        }

        SpotValue ParseSpotValue(int ndxRow)
        {
            string[] row = m_srcData[ndxRow];
            int colPrice = m_colMapping[ColumnKey_t.Price];
            int colTime = m_colMapping[ColumnKey_t.Date];
            int colSession = m_colMapping[ColumnKey_t.SessionNber];
            double price;
            DateTime tm;
            uint sessionNber;
            TRLabel lbl;
            ProductMapping mapping;

            if (!double.TryParse(row[colPrice] , out price) || price <= 0.0 ||
                    !DateTime.TryParse(row[colTime] , out tm) ||
                    !uint.TryParse(row[colSession] , out sessionNber) ||
                    (mapping = ParseProductMapping(ndxRow)) == null ||
                    (lbl = ParseLabel(ndxRow , mapping.ProductNumber)) == null)
                return null;

            SpotValue sp = null;

            if(FindSpotValue(mapping.ID , tm) == null)
            {
                sp = new SpotValue(AppContext.TableManager.TRSpotValues.CreateUniqID() , mapping.ID ,
                    lbl.ID , sessionNber , price , tm);

                Assert(m_pendingData.ContainsKey(AppContext.TableManager.TRSpotValues.ID) == false);
                m_pendingData[AppContext.TableManager.TRSpotValues.ID] = sp;
            }

            return sp;
        }

        void SetupSupplier()
        {
            using (IDatumProvider dp = AppContext.TableManager.Suppiers.DataProvider)
            {
                dp.Connect();

                DataSupplier tr = dp.Count == 0 ? null :
                    dp.Enumerate().Where(d => (d as IDataRow).ID == SuppliersID.TR).SingleOrDefault() as DataSupplier;

                if (tr == null)
                {
                    tr = new DataSupplier(SuppliersID.TR , "THOMSON REUTERS");
                    dp.Insert(tr);
                }
            }
        }
    }
}
