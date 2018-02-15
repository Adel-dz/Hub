//using DGD.HubCore;
//using DGD.HubCore.DB;
//using DGD.HubGovernor.Countries;
//using DGD.HubGovernor.Currencies;
//using DGD.HubGovernor.Incoterms;
//using DGD.HubGovernor.Places;
//using DGD.HubGovernor.Products;
//using DGD.HubGovernor.Spots;
//using DGD.HubGovernor.Suppliers;
//using DGD.HubGovernor.TR.Sessions;
//using DGD.HubGovernor.Units;
//using DGD.HubGovernor.VContext;
//using easyLib;
//using easyLib.DB;
//using easyLib.Log;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using static System.Diagnostics.Debug;




//namespace DGD.HubGovernor.TR.Imp
//{


//    class ImportDriver
//    {
//        ImportInfo m_impInfo;
//        ImportAccessPath m_accessPath;
//        List<SessionImportData> m_impResult;
//        readonly Dictionary<IDataTable , IDatum> m_pendingData = new Dictionary<IDataTable , IDatum>();
//        List<int> m_ignoredRows;
//        EventLogger m_wrnLogger = new EventLogger(LogSeverity.Warning , true);
//        readonly string[] m_ictNames = { "EXW" , "FCA" , "FOB" , "FAS" };
//        Session m_curSession;
//        Product m_curProduct;
//        TRMapping m_curMapping;
//        Country m_curOrigin;
//        Incoterm m_curIncoterm;
//        Place m_curPlace;
//        Currency m_curCurrency;
//        Unit m_curUnit;
//        ValueContext m_curValueContext;
//        SpotValue m_curValue;


//        public IList<int> IgnoredRows => m_ignoredRows;
//        public IList<SessionImportData> ImportResult => m_impResult;

//        public void Run(ImportInfo impInfo , ImportAccessPath accessPath)
//        {
//            Assert(impInfo != null);
//            Assert(accessPath != null);

//            m_impInfo = impInfo;
//            m_accessPath = accessPath;

//            /*
//             *  - pour chaque line de impInfo.Data:
//             *      -- importer session
//             *      -- importer valeur spot
//             *          --- importer produit
//             *          --- importer contexte de valeur
//             *              ---- importer origine
//             *              ---- importer incoterm
//             *              ---- importer lieu             *          
//             *              ---- importer monnaie
//             *              ---- importer unite
//             *          --- importer date
//             *          --- importer prix
//             *      -- importer mapping         
//             */

//            m_impResult = new List<SessionImportData>();
//            m_ignoredRows = new List<int>();



//            //verfier que DataSuppliers contients TR
//            CheckSupplier();

//            for (int i = 0; i < m_impInfo.Data.Length; ++i)
//            {
//                Reset();

//                ImportSession(i);
//                ImportProduct(i);
//                ImportOrigin(i);
//                ImportIncoterm(i);
//                ImportPlace(i);
//                ImportCurrency(i);
//                ImportUnit(i);
//                ImportValueContext();
//                ImportMapping(i);
//                ImportSpotValue(i);

//                if (!ValidateProduct(i) || !ValidateOrigin() || !ValidateIncoterm() || !ValidatePlace() || !ValidateCurrency() ||
//                    !ValidateUnit() || !ValidateValueContext() || !ValidateMapping(i) || !ValidateSpotValue())
//                {
//                    m_ignoredRows.Add(i);

//                    m_wrnLogger.PutLine($"Importation de la ligne {i + 1} ignorée.\n");
//                    continue;
//                }

//                SessionImportData sid = m_impResult.Where(s => s.Session == m_curSession).SingleOrDefault();

//                if (sid == null)
//                {
//                    sid = new SessionImportData(m_curSession);
//                    m_impResult.Add(sid);
//                }

//                foreach (IDataTable table in m_pendingData.Keys)
//                    sid.AddDatum(table , m_pendingData[table]);
//            }

//            m_wrnLogger.Flush();
//        }


//        private:
//        string AdjustInput(string input)
//        {
//            Opts.StringTransform_t opt = AppContext.AppSettings.ImportTransform;

//            input = input.Trim();

//            return opt == Opts.StringTransform_t.LowerCase ? input.ToLower() :
//                opt == Opts.StringTransform_t.UpperCase ? input.ToUpper() : input;
//        }

//        void ImportSession(int ndxRow)
//        {
//            int ndxSessionNber = m_impInfo.ColumnsMapping[ColumnKey_t.SessionNber];
//            string[] row = m_impInfo.Data[ndxRow];
//            uint sessionNber;

//            if (!uint.TryParse(row[ndxSessionNber].Trim() , out sessionNber))
//                throw new ImportException("N° de Session" , ndxRow);

//            si la session est deja creee => il exite une cle tq Session.Number = sessionNber.
//            le contraire n'est pas forcement vrai (si la cle existe elle peut etre due a l'ajout
//            d'une ancienne session car la session se comporte comme un namespace)

//            m_curSession = (from s in m_impResult
//                            where s.Session.Number == sessionNber
//                            select s.Session).SingleOrDefault();

//            session pas encore crée. verfier d'abord l'existance d'une ancienne session de meme n0
//            if (m_curSession == null)
//                m_curSession = m_accessPath.SessionNumbers.Get(sessionNber).Cast<Session>().SingleOrDefault();

//            nouvelle session?
//            if (m_curSession == null)
//            {
//                m_curSession = new Session(AppContext.TableManager.TRSessions.CreateUniqID() , sessionNber);
//                m_pendingData.Add(AppContext.TableManager.TRSessions , m_curSession);
//            }
//        }

//        TRMapping FindMapping(uint productNber)
//        {
//            //1 rechercher dans l'imporation en cours

//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.TRMapping , out data))
//                    foreach (TRMapping mapping in data)
//                        if (mapping.ProductNumber == productNber)
//                            return mapping;

//            //2. rechercher dans la base de donnees
//            return (from TRMapping m in m_accessPath.TRMappingProductsNumber.Get(productNber)
//                    where m.ProductNumber == productNber
//                    select m).SingleOrDefault();
//        }

//        TRMapping FindMapping(uint prodID , uint valContextID)
//        {
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.TRMapping , out data))
//                    foreach (TRMapping m in data)
//                        if (m.ProductID == prodID && m.ValueContextID == valContextID)
//                            return m;

//            return (from TRMapping m in m_accessPath.TRMappingProductsID.Get(prodID)
//                    where m.ValueContextID == valContextID
//                    select m).SingleOrDefault();
//        }

//        Product FindProduct(string name , SubHeading sh)
//        {
//            1 rechercher dans l'importation en cours
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Products , out data))
//                    foreach (Product prod in data)
//                        if (prod.SubHeading.Equals(sh) && string.Compare(prod.Name , name , true) == 0)
//                            return prod;

//            return (from Product prod in m_accessPath.ProductsSubHeading.Get(sh)
//                    where string.Compare(prod.Name , name , true) == 0
//                    select prod).SingleOrDefault();
//        }

//        Product FindProduct(uint prodID)
//        {
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Products , out data))
//                    foreach (Product p in data)
//                        if (p.ID == prodID)
//                            return p;

//            return m_accessPath.Products.Get(prodID) as Product;
//        }

//        void ImportProduct(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];

//            int ndxProductNber = m_impInfo.ColumnsMapping[ColumnKey_t.ProductNber];
//            uint productNber;

//            if (!uint.TryParse(row[ndxProductNber].Trim() , out productNber))
//                throw new ImportException("N° de produit" , ndxRow);



//            si un mapping existe alors le produit du mapping est le produit recherche(apres validation)
//            sinon verifier que le produit n'a pas ete importe par un autre referentiel
//             sinon creer

//            m_curMapping = FindMapping(productNber);


//            if (m_curMapping != null)
//            {
//                le mapping existe => le produit existe (contrainte 13)
//                m_curProduct = FindProduct(m_curMapping.ProductID);

//                if (m_curProduct == null)
//                    throw new CorruptedStreamException($"La table {AppContext.TableManager.Products.Name} est corrompue.\n" +
//                        "Contrainte d'intégrité n° 13 non-respectée.");
//            }
//            else
//            {
//                verifier si le produit n'a pas ete importe par un autre referentiel

//                int ndxSubHeading = m_impInfo.ColumnsMapping[ColumnKey_t.SubHeading];
//                SubHeading sh = SubHeading.Parse(row[ndxSubHeading].Trim());

//                if (sh == null)
//                    throw new ImportException("SPT" , ndxRow);


//                int ndxLabelFr = m_impInfo.ColumnsMapping[ColumnKey_t.LabelFr];
//                string labelFr = AdjustInput(row[ndxLabelFr]);

//                if (labelFr == "")
//                    throw new ImportException("Libellé Fr" , ndxRow);


//                m_curProduct = FindProduct(labelFr , sh);

//                if (m_curProduct == null)
//                {
//                    ok nouveau produit
//                    m_curProduct = new Product(AppContext.TableManager.Products.CreateUniqID() , labelFr , sh);
//                    m_pendingData[AppContext.TableManager.Products] = m_curProduct;
//                }
//            }
//        }

//        bool ValidateProduct(int ndxRow)
//        {
//            validation des contraintes:
//            -1


//            Assert(m_curProduct != null);

//            if (m_pendingData.ContainsKey(AppContext.TableManager.Products))
//            {
//                if (FindProduct(m_curProduct.Name , m_curProduct.SubHeading) != null)
//                {
//                    m_wrnLogger.PutLine("Contrainte n° 1 non-respectée!");
//                    return false;
//                }
//            }
//            else //produit non nouveau
//            {
//                string[] row = m_impInfo.Data[ndxRow];

//                int ndxSubHeading = m_impInfo.ColumnsMapping[ColumnKey_t.SubHeading];
//                SubHeading sh = SubHeading.Parse(row[ndxSubHeading].Trim());

//                if (sh == null)
//                    throw new ImportException("SPT" , ndxRow);


//                int ndxLabelFr = m_impInfo.ColumnsMapping[ColumnKey_t.LabelFr];
//                string labelFr = AdjustInput(row[ndxLabelFr]);

//                if (labelFr == "")
//                    throw new ImportException("Libellé Fr" , ndxRow);

//                if (!m_curProduct.SubHeading.Equals(sh) || string.Compare(m_curProduct.Name , labelFr , true) != 0)
//                {
//                    m_wrnLogger.PutLine($"Conflit trouvé pour le produit de n° {m_curMapping.ProductNumber} :");
//                    m_wrnLogger.PutLine($"SPT10: Ancienne valeur: {m_curProduct.SubHeading}, nouvelle valeur: {sh}.");
//                    m_wrnLogger.PutLine($"Libellé: Ancienne valeur: {m_curProduct.Name}, nouvelle valeur {labelFr}.");

//                    return false;
//                }
//            }



//            return true;
//        }

//        Country FindCountry(ushort code)
//        {
//            /*
//             *  # rechercher dans:
//             *      # importaion en cours
//             *      # toute la base de donnees
//             */



//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Countries , out data))
//                    foreach (Country ctry in data)
//                        if (ctry.InternalCode == code)
//                            return ctry;

//            return m_accessPath.CountriesInternalCode.Get(code).Cast<Country>().SingleOrDefault();
//        }

//        Country FindCountry(string name)
//        {
//            /*
//             *  # rechercher dans:
//             *      # importaion en cours
//             *      # toute la base de donnees
//             */


//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Countries , out data))
//                    foreach (Country ctry in data)
//                        if (string.Compare(ctry.Name , name , true) == 0)
//                            return ctry;

//            return m_accessPath.CountriesName.Get(m_curOrigin.Name.ToUpper()).SingleOrDefault() as Country;
//        }

//        void ImportOrigin(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxOrigin = m_impInfo.ColumnsMapping[ColumnKey_t.Origin];
//            ushort countryCode;

//            if (string.IsNullOrWhiteSpace(row[ndxOrigin]))
//                return; //ok origine vide

//            if (!ushort.TryParse(row[ndxOrigin] , out countryCode) || countryCode == 0)
//                throw new ImportException("Origine" , ndxRow);


//            rechercher pays p existant tq p.InternalCode = countryCode
//            m_curOrigin = FindCountry(countryCode);

//            if (m_curOrigin == null)
//            {
//                uint id = AppContext.TableManager.Countries.CreateUniqID();
//                m_curOrigin = new Country(id , $"PAYS_{countryCode}" , countryCode);
//                m_pendingData[AppContext.TableManager.Countries] = m_curOrigin;
//            }
//        }

//        bool ValidateOrigin()
//        {
//            /*
//             * validation des contraintes:
//             *  2.1
//             *  2.2
//             *  2.3
//             */


//            if (!m_pendingData.ContainsKey(AppContext.TableManager.Countries))
//                return true;    //pas de nouveau pays

//            Assert(m_curOrigin != null);


//            if (FindCountry(m_curOrigin.InternalCode) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte 2.1 non-respectée!");
//                return false;
//            }


//            if (FindCountry(m_curOrigin.Name) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte 2.2 non-respectée!");
//                return false;
//            }

//            pour les nouveau pays IsoCode = "" => valid

//            return true;
//        }

//        Incoterm FindIncoterm(string name)
//        {
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Incoterms , out data))
//                    foreach (Incoterm ict in data)
//                        if (string.Compare(ict.Name , name , true) == 0)
//                            return ict;

//            return (from Incoterm ict in m_accessPath.IncotermsName.Get(name.ToUpper())
//                    select ict).SingleOrDefault();
//        }

//        void ImportIncoterm(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxIncoterm = m_impInfo.ColumnsMapping[ColumnKey_t.Incoterm];

//            string ictName = AdjustInput(row[ndxIncoterm]);

//            if (string.IsNullOrWhiteSpace(ictName))
//                return; // ok incoterm vide

//            m_curIncoterm = FindIncoterm(ictName);


//            if (m_curIncoterm == null)
//            {
//                m_curIncoterm = new Incoterm(AppContext.TableManager.Incoterms.CreateUniqID() ,
//                    ictName);

//                m_pendingData[AppContext.TableManager.Incoterms] = m_curIncoterm;
//            }
//        }

//        bool ValidateIncoterm()
//        {
//            /*
//             * Validation de la contrainte 9
//             */

//            if (!m_pendingData.ContainsKey(AppContext.TableManager.Incoterms))
//                return true;

//            Assert(m_curIncoterm != null);


//            if (FindIncoterm(m_curIncoterm.Name) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 9 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        IEnumerable<Place> FindPlaces(string name)
//        {
//            IEnumerable<Place> places = Enumerable.Empty<Place>();
//            IList<IDatum> data;

//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Places , out data))
//                {
//                    IEnumerable<Place> seq = from Place p in data
//                                             where string.Compare(p.Name , name , true) == 0
//                                             select p;

//                    places = places.Union(seq);
//                }

//            return (from Place p in m_accessPath.PlacesName.Get(name.ToUpper())
//                    select p).Union(places);
//        }

//        Country CountryFromIncoterm()
//        {
//            if (m_curIncoterm == null || m_curOrigin == null)
//                return null;

//            foreach (string s in m_ictNames)
//                if (string.Compare(m_curIncoterm.Name , s , true) == 0)
//                    return m_curOrigin;

//            return null;
//        }

//        Place CreatePlace(string name)
//        {
//            Country ctry = CountryFromIncoterm();

//            if (ctry == null)
//                ctry = FindCountry(Country.UNKNOWN_COUNTRY_INTERNAL_CODE);

//            if (ctry == null)
//            {
//                ctry = new Country(AppContext.TableManager.Countries.CreateUniqID() ,
//                    "PAYS_INCONNU" , Country.UNKNOWN_COUNTRY_INTERNAL_CODE);

//                Assert(m_curSession != null);

//                SessionImportData sid = (from x in m_impResult
//                                         where x.Session == m_curSession
//                                         select x).SingleOrDefault();

//                if (sid == null)
//                {
//                    sid = new SessionImportData(m_curSession);
//                    m_impResult.Add(sid);
//                }

//                sid.AddDatum(AppContext.TableManager.Countries , ctry);
//            }

//            return new Place(AppContext.TableManager.Places.CreateUniqID() , name , ctry.ID);
//        }

//        void ImportPlace(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxPlace = m_impInfo.ColumnsMapping[ColumnKey_t.Place];

//            string placeName = AdjustInput(row[ndxPlace]);

//            if (string.IsNullOrWhiteSpace(placeName))
//                return; // ok lieu vide


//            IEnumerable<Place> places = FindPlaces(placeName);

//            if (!places.Any())
//            {
//                m_curPlace = CreatePlace(placeName);
//                m_pendingData[AppContext.TableManager.Places] = m_curPlace;
//            }
//            else if (places.Count() == 1)
//                m_curPlace = places.Single();
//            else
//            {
//                choisir le deriner lieu ajoute
//                m_curPlace = places.First();

//                foreach (Place p in places.Skip(1))
//                    if (p.ID > m_curPlace.ID)
//                        m_curPlace = p;
//            }
//        }

//        bool ValidatePlace()
//        {
//            /*
//             * validation de la contrainte 4
//             */

//            if (!m_pendingData.ContainsKey(AppContext.TableManager.Places))
//                return true;

//            Assert(m_curPlace != null);


//            if ((from Place p in FindPlaces(m_curPlace.Name)
//                 where p.CountryID == m_curPlace.CountryID
//                 select p).Any())
//            {
//                m_wrnLogger.PutLine("Contrainte n° 4 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        Currency FindCurrency(string name)
//        {
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Currencies , out data))
//                    foreach (Currency c in data)
//                        if (string.Compare(name , c.Name , true) == 0)
//                            return c;

//            return (from Currency c in m_accessPath.CurrenciesName.Get(name.ToUpper())
//                    select c).SingleOrDefault();
//        }

//        void ImportCurrency(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxCurrency = m_impInfo.ColumnsMapping[ColumnKey_t.Currency];

//            string cncyName = AdjustInput(row[ndxCurrency]);

//            if (string.IsNullOrWhiteSpace(cncyName))
//                throw new ImportException("Monnaie" , ndxRow);

//            m_curCurrency = FindCurrency(cncyName);

//            if (m_curCurrency == null)
//            {
//                m_curCurrency = new Currency(AppContext.TableManager.Currencies.CreateUniqID() , cncyName);
//                m_pendingData[AppContext.TableManager.Currencies] = m_curCurrency;
//            }
//        }

//        bool ValidateCurrency()
//        {
//            /*
//             * validation de la contrainte 3
//             */

//            if (!m_pendingData.ContainsKey(AppContext.TableManager.Currencies))
//                return true;

//            Assert(m_curCurrency != null);


//            if (FindCurrency(m_curCurrency.Name) != null)
//            {
//                m_wrnLogger.PutLine("Containte n° 3 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        Unit FindUnit(string name)
//        {
//            IList<IDatum> data;
//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.Units , out data))
//                    foreach (Unit u in data)
//                        if (string.Compare(u.Name , name , true) == 0)
//                            return u;

//            return (from Unit u in m_accessPath.UnitsName.Get(name.ToUpper())
//                    select u).SingleOrDefault();
//        }

//        void ImportUnit(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxUnit = m_impInfo.ColumnsMapping[ColumnKey_t.Unit];

//            string unitName = AdjustInput(row[ndxUnit]);

//            if (string.IsNullOrWhiteSpace(unitName))
//                throw new ImportException("Unité" , ndxRow);

//            m_curUnit = FindUnit(unitName);

//            if (m_curUnit == null)
//            {
//                m_curUnit = new Unit(AppContext.TableManager.Units.CreateUniqID() , unitName);
//                m_pendingData[AppContext.TableManager.Units] = m_curUnit;
//            }
//        }

//        bool ValidateUnit()
//        {
//            /*
//             * Validation de la contrainte 10
//             */


//            if (!m_pendingData.ContainsKey(AppContext.TableManager.Units))
//                return true;

//            Assert(m_curUnit != null);


//            if (FindUnit(m_curUnit.Name) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 10 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        ValueContext FindValueContext(uint cncyID , uint unitID , uint origID , uint ictID , uint placeID)
//        {
//            IList<IDatum> data;

//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.ValuesContext , out data))
//                    foreach (ValueContext vc in data)
//                        if (vc.CurrencyID == cncyID && vc.UnitID == unitID && vc.OriginID == origID &&
//                                vc.IncotermID == ictID && vc.PlaceID == placeID)
//                            return vc;


//            IEnumerable<ValueContext> seq = from ValueContext vc in m_accessPath.ValuesContextPlaceID.Get(placeID)
//                                            where vc.CurrencyID == cncyID && vc.IncotermID == ictID &&
//                                            vc.OriginID == origID && vc.UnitID == unitID
//                                            select vc;

//            return seq.SingleOrDefault();
//        }

//        void ImportValueContext()
//        {
//            Assert(m_curUnit != null);
//            Assert(m_curCurrency != null);

//            uint origID = (m_curOrigin?.ID) ?? 0;
//            uint ictID = (m_curIncoterm?.ID) ?? 0;
//            uint placeID = (m_curPlace?.ID) ?? 0;

//            m_curValueContext = FindValueContext(m_curCurrency.ID , m_curUnit.ID , origID , ictID , placeID);

//            if (m_curValueContext == null)
//            {
//                m_curValueContext = new ValueContext(AppContext.TableManager.ValuesContext.CreateUniqID() ,
//                    m_curCurrency.ID , m_curUnit.ID , origID , ictID , placeID);

//                m_pendingData[AppContext.TableManager.ValuesContext] = m_curValueContext;
//            }
//        }

//        bool ValidateValueContext()
//        {
//            /*
//             * validation de la contrainte 14
//             */

//            if (!m_pendingData.ContainsKey(AppContext.TableManager.ValuesContext))
//                return true;
//            Assert(m_curValueContext != null);

//            if (FindValueContext(m_curValueContext.CurrencyID , m_curValueContext.UnitID , m_curValueContext.OriginID ,
//                m_curValueContext.IncotermID , m_curValueContext.PlaceID) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 14 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        void ImportSpotValue(int ndxRow)
//        {
//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxDate = m_impInfo.ColumnsMapping[ColumnKey_t.Date];
//            int ndxPrice = m_impInfo.ColumnsMapping[ColumnKey_t.Price];

//            DateTime spotDate;
//            if (!DateTime.TryParse(row[ndxDate] , out spotDate))
//                throw new ImportException("Date spot" , ndxRow);

//            double price;
//            if (!double.TryParse(row[ndxPrice] , out price) || price == 0.0)
//                throw new ImportException("Prix" , ndxRow);

//            Assert(m_curValueContext != null);
//            Assert(m_curProduct != null);

//            if (!AppContext.TableManager.SpotValues.IsConnected)
//                AppContext.TableManager.SpotValues.Connect();

//            m_curValue = new SpotValue(AppContext.TableManager.SpotValues.CreateUniqID() ,
//                price , spotDate , m_curProduct.ID , m_curValueContext.ID , SuppliersID.IDSUPPLIER_TR);
//            m_pendingData[AppContext.TableManager.SpotValues] = m_curValue;
//        }

//        SpotValue FindTRValue(uint prodID , uint valContetxID , DateTime spotTime)
//        {
//            IList<IDatum> data;

//            foreach (SessionImportData sid in m_impResult)
//                if (sid.ImportData.TryGetValue(AppContext.TableManager.SpotValues , out data))
//                    foreach (SpotValue sv in data)
//                        if (sv.SupplierID == SuppliersID.IDSUPPLIER_TR && sv.ProductID == prodID &&
//                                sv.ValueContextID == valContetxID && sv.SpotTime == spotTime)
//                            return sv;

//            IEnumerable<SpotValue> seq = from SpotValue sv in m_accessPath.SpotValuesProductID.Get(prodID)
//                                         where sv.SupplierID == SuppliersID.IDSUPPLIER_TR && sv.ValueContextID == valContetxID &&
//                                             sv.SpotTime == spotTime
//                                         select sv;

//            return seq.SingleOrDefault();
//        }

//        bool ValidateSpotValue()
//        {
//            /*
//             * validation de la conrainte 8 et 12
//             */

//            Assert(m_curValue != null);

//            if (FindTRValue(m_curValue.ProductID , m_curValue.ValueContextID , m_curValue.SpotTime) != null)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 8 non-respectée!");
//                return false;
//            }


//            if (m_curMapping.ProductID != m_curValue.ProductID || m_curMapping.ValueContextID != m_curValue.ValueContextID)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 12 non-respectée!");
//                return false;
//            }

//            return true;
//        }

//        void ImportMapping(int ndxRow)
//        {
//            if (m_curMapping != null)
//                return;

//            Assert(m_curProduct != null);


//            string[] row = m_impInfo.Data[ndxRow];
//            int ndxProdNber = m_impInfo.ColumnsMapping[ColumnKey_t.ProductNber];
//            uint prodNber = uint.Parse(row[ndxProdNber]);

//            int ndxLabelUs = m_impInfo.ColumnsMapping[ColumnKey_t.LabelUs];
//            string labelUs = AdjustInput(row[ndxLabelUs]);

//            if (labelUs == "")
//                throw new ImportException("Libellé Us" , ndxRow);

//            Assert(m_curValueContext != null);

//            m_curMapping = new TRMapping(AppContext.TableManager.TRMapping.CreateUniqID() , prodNber ,
//                m_curProduct.ID , m_curValueContext.ID , labelUs);
//            m_pendingData[AppContext.TableManager.TRMapping] = m_curMapping;
//        }

//        bool ValidateMapping(int ndxRow)
//        {
//            /*
//             * validation des contraintes:
//             *  5
//             *  11             
//             *  13
//             */

//            Assert(m_curMapping != null);

//            if (m_pendingData.ContainsKey(AppContext.TableManager.TRMapping)) //nouveau mapping??
//            {
//                if (FindMapping(m_curMapping.ProductNumber) != null)
//                {
//                    m_wrnLogger.PutLine("Contrainte n° 5 non-respectée!");
//                    return false;
//                }

//                TRMapping m = FindMapping(m_curMapping.ProductID , m_curMapping.ValueContextID);

//                if (m != null)
//                {
//                    m_wrnLogger.PutLine("Contrainte n° 11 non-respectée!");
//                    return false;
//                }
//            }
//            else
//            {

//                mapping pas nouveau => valider labelUs
//                string[] row = m_impInfo.Data[ndxRow];

//                int ndxLabelUs = m_impInfo.ColumnsMapping[ColumnKey_t.LabelUs];
//                string labelUs = AdjustInput(row[ndxLabelUs]);

//                if (labelUs == "")
//                    throw new ImportException("Libellé Us" , ndxRow);

//                if (string.Compare(labelUs , m_curMapping.Description , true) != 0)
//                {
//                    m_wrnLogger.PutLine($"Libellé TR du produit n° {m_curMapping.ProductNumber} non-conforme à l'ancienne donnée:");
//                    m_wrnLogger.PutLine($"Ancienne valeur: {m_curMapping.Description},");
//                    m_wrnLogger.PutLine($"Nouvelle valeur: {labelUs}.");

//                    return false;
//                }
//            }


//            if (m_pendingData.ContainsKey(AppContext.TableManager.Products))
//            {
//                if (m_curProduct.ID != m_curMapping.ProductID)
//                {
//                    m_wrnLogger.PutLine("Contrainte n° 13 non-respectée!");
//                    return false;
//                }
//            }
//            else if (FindProduct(m_curMapping.ProductID) == null)
//            {
//                m_wrnLogger.PutLine("Contrainte n° 13 non-respectée!");
//                return false;
//            }


//            return true;
//        }

//        void CheckSupplier()
//        {
//            using (IDatumProvider dp = AppContext.TableManager.Suppiers.DataProvider)
//            {
//                dp.Connect();

//                DataSupplier tr = dp.Count == 0 ? null :
//                    dp.Enumerate().Where(d => (d as IDataRow).ID == SuppliersID.IDSUPPLIER_TR).SingleOrDefault() as DataSupplier;

//                if (tr == null)
//                {
//                    tr = new DataSupplier(SuppliersID.IDSUPPLIER_TR , "THOMSON REUTERS");
//                    dp.Insert(tr);
//                }
//            }
//        }

//        void Reset()
//        {
//            m_pendingData.Clear();

//            m_curSession = null;
//            m_curProduct = null;
//            m_curMapping = null;
//            m_curOrigin = null;
//            m_curIncoterm = null;
//            m_curPlace = null;
//            m_curCurrency = null;
//            m_curUnit = null;
//            m_curValueContext = null;
//            m_curValue = null;
//        }
//    }
//}