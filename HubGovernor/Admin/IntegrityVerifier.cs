using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using DGD.HubGovernor.Extensions;
using DGD.HubGovernor.Jobs;
using easyLib.DB;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Admin
{
    class IntegrityVerifier: IDisposable
    {
        class AccessPath: IDisposable
        {
            readonly object m_lock = new object();            
            AttrIndexer<string> m_ndxerCtryName;
            AttrIndexer<ushort> m_ndxerCtryCode;
            AttrIndexer<string> m_ndxerCtryIsoCode;
            AttrIndexer<string> m_ndxerCncyName;
            AttrIndexer<string> m_ndxerIctName;
            AttrIndexer<uint> m_ndxerLabelProdNber;
            AttrIndexer<uint> m_ndxerMapProdNber;
            KeyIndexer m_ndxerProducts;
            KeyIndexer m_ndxerValContext;
            KeyIndexer m_ndxerTrLabels;
            KeyIndexer m_ndxerTrProdMappings;
            IDatumProvider m_srcTRProMappings;
            IDatumProvider m_srcPlaces;
            IDatumProvider m_srcProducts;
            IDatumProvider m_srcValues;
            IDatumProvider m_srcSuppliers;
            IDatumProvider m_srcTRSpotValues;
            IDatumProvider m_srcUnits;
            IDatumProvider m_srcValContext;
            IDatumProvider m_srcCountries;



            public IDatumProvider CountriesProvider
            {
                get
                {
                    Assert(!IsDisposed);


                    if (m_srcCountries == null)
                        lock (m_lock)
                            if (m_srcCountries == null)
                            {
                                m_srcCountries = AppContext.TableManager.Countries.RowProvider;
                                m_srcCountries.Connect();
                            }

                    return m_srcCountries;
                }
            }

            public AttrIndexer<uint> TRLabelsProdNber
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_ndxerLabelProdNber == null)
                        lock (m_lock)
                            if (m_ndxerLabelProdNber == null)
                            {
                                m_ndxerLabelProdNber = 
                                    new AttrIndexer<uint>(AppContext.TableManager.TRLabels.DataProvider ,
                                    d => (d as TR.ITRLabel).ProductNumber);

                                m_ndxerLabelProdNber.Connect();
                            }

                    return m_ndxerLabelProdNber;
                }
            }

            public AttrIndexer<uint> TRProdMappingProductNber
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_ndxerMapProdNber == null)
                        lock (m_lock)
                            if (m_ndxerMapProdNber == null)
                            {
                                m_ndxerMapProdNber = new AttrIndexer<uint>(TRProductMappingsProvider ,
                                    d => (d as TR.ProductMapping).ProductNumber);

                                m_ndxerMapProdNber.Connect();
                            }

                    return m_ndxerMapProdNber;
                }
            }

            public AttrIndexer<string> CountriesName
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_ndxerCtryName == null)
                        lock (m_lock)
                            if (m_ndxerCtryName == null)
                            {
                                m_ndxerCtryName = new AttrIndexer<string>(CountriesProvider ,
                                    d => (d as Countries.Country).Name.ToUpper());

                                m_ndxerCtryName.Connect();
                            }

                    return m_ndxerCtryName;
                }
            }

            public AttrIndexer<ushort> CountriesInternalCode
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_ndxerCtryCode == null)
                        lock (m_lock)
                            if (m_ndxerCtryCode == null)
                            {
                                m_ndxerCtryCode = new AttrIndexer<ushort>(m_srcCountries ,
                                    d => (d as Countries.Country).InternalCode);
                                m_ndxerCtryCode.Connect();
                            }

                    return m_ndxerCtryCode;
                }
            }

            public AttrIndexer<string> CountriesIsoCode
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_ndxerCtryIsoCode == null)
                        lock (m_lock)
                            if (m_ndxerCtryIsoCode == null)
                            {
                                m_ndxerCtryIsoCode = new AttrIndexer<string>(CountriesProvider ,
                                    d => (d as Countries.Country).IsoCode.ToUpper());

                                m_ndxerCtryIsoCode.Connect();
                            }

                    return m_ndxerCtryIsoCode;
                }
            }

            public AttrIndexer<string> CurrenciesName
            {
                get
                {
                    if (m_ndxerCncyName == null)
                        lock (m_lock)
                            if (m_ndxerCncyName == null)
                            {
                                m_ndxerCncyName = new AttrIndexer<string>(AppContext.TableManager.Currencies.RowProvider ,
                                    d => (d as Currencies.Currency).Name.ToUpper());

                                m_ndxerCncyName.Connect();
                            }

                    return m_ndxerCncyName;
                }
            }

            public AttrIndexer<string> IncotermsName
            {
                get
                {
                    if (m_ndxerIctName == null)
                        lock (m_lock)
                            if (m_ndxerIctName == null)
                            {
                                m_ndxerIctName = new AttrIndexer<string>(AppContext.TableManager.Incoterms.RowProvider ,
                                    d => (d as Incoterms.Incoterm).Name.ToUpper());

                                m_ndxerIctName.Connect();
                            }

                    return m_ndxerIctName;
                }
            }

            public KeyIndexer ProductsIndexer
            {
                get
                {
                    if (m_ndxerProducts == null)
                        lock (m_lock)
                            if (m_ndxerProducts == null)
                            {
                                m_ndxerProducts = new KeyIndexer(ProductsProvider);
                                m_ndxerProducts.Connect();
                            }

                    return m_ndxerProducts;
                }
            }

            public KeyIndexer ValueContextsIndexer
            {
                get
                {
                    if (m_ndxerValContext == null)
                        lock (m_lock)
                            if (m_ndxerValContext == null)
                            {
                                m_ndxerValContext = new KeyIndexer(ValueContextsProvider);
                                m_ndxerValContext.Connect();
                            }

                    return m_ndxerValContext;
                }
            }

            public KeyIndexer TRLabelsIndexer
            {
                get
                {
                    if (m_ndxerTrLabels == null)
                        lock (m_lock)
                            if (m_ndxerTrLabels == null)
                            {
                                m_ndxerTrLabels = new KeyIndexer(AppContext.TableManager.TRLabels.DataProvider);
                                m_ndxerTrLabels.Connect();
                            }

                    return m_ndxerTrLabels;
                }
            }

            public KeyIndexer TRProductMappingsIndexer
            {
                get
                {
                    if (m_ndxerTrProdMappings == null)
                        lock (m_lock)
                            if (m_ndxerTrProdMappings == null)
                            {
                                m_ndxerTrProdMappings = new KeyIndexer(TRProductMappingsProvider);
                                m_ndxerTrProdMappings.Connect();
                            }

                    return m_ndxerTrProdMappings;
                }
            }
            
            public IDatumProvider TRProductMappingsProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcTRProMappings == null)
                        lock (m_lock)
                            if (m_srcTRProMappings == null)
                            {
                                m_srcTRProMappings = AppContext.TableManager.TRProductsMapping.DataProvider;
                                m_srcTRProMappings.Connect();
                            }

                    return m_srcTRProMappings;
                }
            }

            public IDatumProvider TRSpotValuesProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcTRSpotValues == null)
                        lock (m_lock)
                            if (m_srcTRSpotValues == null)
                            {
                                m_srcTRSpotValues = AppContext.TableManager.TRSpotValues.DataProvider;
                                m_srcTRSpotValues.Connect();
                            }

                    return m_srcTRSpotValues;
                }
            }

            public IDatumProvider PlacesProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcPlaces == null)
                        lock (m_lock)
                            if (m_srcPlaces == null)
                            {
                                m_srcPlaces = AppContext.TableManager.Places.RowProvider;
                                m_srcPlaces.Connect();
                            }

                    return m_srcPlaces;
                }
            }

            public IDatumProvider ProductsProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcProducts == null)
                        lock (m_lock)
                            if (m_srcProducts == null)
                            {
                                m_srcProducts = AppContext.TableManager.Products.RowProvider;
                                m_srcProducts.Connect();
                            }

                    return m_srcProducts;
                }
            }

            public IDatumProvider ValuesProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcValues == null)
                        lock (m_lock)
                            if (m_srcValues == null)
                            {
                                m_srcValues = AppContext.TableManager.SpotValues.DataProvider;
                                m_srcValues.Connect();
                            }

                    return m_srcValues;
                }
            }

            public IDatumProvider SuppliersProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcSuppliers == null)
                        lock (m_lock)
                            if (m_srcSuppliers == null)
                            {
                                m_srcSuppliers = AppContext.TableManager.Suppiers.DataProvider;
                                m_srcSuppliers.Connect();
                            }

                    return m_srcSuppliers;
                }
            }

            public IDatumProvider UnitsProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcUnits == null)
                        lock (m_lock)
                            if (m_srcUnits == null)
                            {
                                m_srcUnits = AppContext.TableManager.Units.RowProvider;
                                m_srcUnits.Connect();
                            }

                    return m_srcUnits;
                }
            }

            public IDatumProvider ValueContextsProvider
            {
                get
                {
                    Assert(!IsDisposed);

                    if (m_srcValContext == null)
                        lock (m_lock)
                            if (m_srcValContext == null)
                            {
                                m_srcValContext = AppContext.TableManager.ValuesContext.DataProvider;
                                m_srcValContext.Connect();
                            }

                    return m_srcValContext;
                }
            }


            public bool IsDisposed { get; private set; }
            
            public void Dispose()
            {
                if (!IsDisposed)
                {
                    lock (m_lock)
                    {
                        m_srcCountries?.Close();
                        m_ndxerCtryName?.Close();
                        m_ndxerCtryCode?.Close();
                        m_ndxerCtryIsoCode?.Close();
                        m_ndxerCncyName?.Close();
                        m_ndxerIctName?.Close();
                        m_ndxerLabelProdNber?.Close();
                        m_ndxerMapProdNber?.Close();
                        m_ndxerProducts?.Close();
                        m_ndxerValContext?.Close();
                        m_ndxerTrLabels?.Close();
                        m_ndxerTrProdMappings?.Close();
                        m_srcTRProMappings?.Close();
                        m_srcPlaces?.Close();
                        m_srcProducts?.Close();
                        m_srcValues?.Close();
                        m_srcSuppliers?.Close();
                        m_srcUnits?.Close();
                        m_srcValContext?.Close();
                        m_srcTRSpotValues?.Close();

                        IsDisposed = true;
                    }
                }
            }
        }

        readonly List<ITableChecker> m_checkers = new List<ITableChecker>();
        readonly AccessPath m_accessPath = new AccessPath();


        public IntegrityVerifier(IEnumerable<IDataTable> tables)
        {
            var checkers = new Dictionary<IDataTable , Func<ITableChecker>>
            {
                {
                    AppContext.TableManager.Countries,() =>
                    new Countries.CountryChecker(m_accessPath.CountriesName,m_accessPath.CountriesInternalCode,
                    m_accessPath.CountriesIsoCode)
                } ,
                { AppContext.TableManager.Currencies, () => new Currencies.CurrencyChecker(m_accessPath.CurrenciesName) },
                { AppContext.TableManager.TablesGeneration, () => new FilesGen.FileGenerationChecker() },
                { AppContext.TableManager.Incoterms, () => new Incoterms.IncotermChecker(m_accessPath.IncotermsName) },
                { AppContext.TableManager.Places, () => new Places.PlaceChecker(m_accessPath.PlacesProvider) },
                { AppContext.TableManager.Products, () => new Products.ProductChecker(m_accessPath.ProductsProvider) },
                { AppContext.TableManager.SpotValues, () => new Spots.SpotValueChecker(m_accessPath.ValuesProvider) },
                { AppContext.TableManager.Suppiers, () => new Suppliers.DataSupplierChecker(m_accessPath.SuppliersProvider) },
                { AppContext.TableManager.Units, () => new Units.UnitChecker(m_accessPath.UnitsProvider) },
                { AppContext.TableManager.ValuesContext, () => new VContext.ValueContextChecker(m_accessPath.ValueContextsProvider) },
                { AppContext.TableManager.TRLabels, () => new TR.TRLabelChecker(m_accessPath.TRLabelsProdNber,
                    m_accessPath.TRProdMappingProductNber) },
                { AppContext.TableManager.TRProductsMapping, () => new TR.ProductMappingChecker(
                    m_accessPath.TRProdMappingProductNber, m_accessPath.ProductsIndexer, m_accessPath.ValueContextsIndexer)},
                { AppContext.TableManager.TRSpotValues, () => new TR.SpotValueChecker(
                    m_accessPath.TRSpotValuesProvider, m_accessPath.TRLabelsIndexer, m_accessPath.TRProductMappingsIndexer)}
            };


            foreach (IDataTable table in tables)
                m_checkers.Add(checkers[table].Invoke());
        }


        public bool IsDisposed { get; private set; }


        public void Run()
        {
            ProcessingDialog dlg = null;

            Action verify = () =>
            {
                foreach (ITableChecker checker in m_checkers)
                {
                    dlg.Message = $"Vérification de la table {AppContext.TableManager.GetTable(checker.TableID).Name}";
                    checker.Check();
                    checker.Dispose();
                }
            };

            Action<Task> onErr = t =>
            {
                dlg.ShowError(t.Exception.InnerException.Message);
                //Dispose();
                dlg.Close();
            };

            Action onSuccess = () =>
            {
                //Dispose();
                dlg.Close();
            };

            using (dlg = new ProcessingDialog())
            {
                var task = new Task(verify);
                task.OnError(onErr);
                task.OnSuccess(onSuccess);

                task.Start();
                dlg.ShowDialog();
            }
        }


        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_accessPath.Dispose();
                IsDisposed = true;
            }
        }


        //private:

    }
}
