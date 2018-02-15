using DGD.HubCore.DB;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.TR
{
    sealed class DataAccesPath : IDisposable
    {
        readonly object m_lock = new object();
        KeyIndexer m_ndxerValues;
        KeyIndexer m_ndxerProducts;
        //AttrIndexer<uint> m_ndxerMappingsProdID;
        KeyIndexer m_ndxerContexts;
        KeyIndexer m_ndxerCountries;
        KeyIndexer m_ndxerIncoterms;
        KeyIndexer m_ndxerPlaces;
        KeyIndexer m_ndxerCurrencies;
        KeyIndexer m_ndxerUnits;


        public KeyIndexer Products
        {
            get
            {
                if(m_ndxerProducts == null)
                    lock(m_lock)
                        if(m_ndxerProducts == null)
                        {
                            m_ndxerProducts = new KeyIndexer(AppContext.TableManager.Products.DataProvider);
                            m_ndxerProducts.Connect();
                        }

                return m_ndxerProducts;
            }
        }

        public KeyIndexer Values
        {
            get
            {
                if(m_ndxerValues == null)
                {
                    lock(m_lock)                    
                        if(m_ndxerValues == null)
                        {
                            m_ndxerValues = new KeyIndexer(AppContext.TableManager.SpotValues.DataProvider);
                            m_ndxerValues.Connect();
                        }                    
                }

                return m_ndxerValues;
            }
        }

        public AttrIndexer<uint> MappingProductsID
        {
            get
            {
                //if(m_ndxerMappingsProdID == null)
                //    lock(m_lock)
                //        if(m_ndxerMappingsProdID == null)
                //        {
                //            m_ndxerMappingsProdID = new AttrIndexer<uint>(AppContext.TableManager.TRMapping.DataProvider ,
                //                d => (d as Mappings.TRMapping).ProductID);

                //            m_ndxerMappingsProdID.Connect();
                //        }

                //return m_ndxerMappingsProdID;

                throw null;
            }
        }

        public KeyIndexer ValuesContext
        {
            get
            {
                if(m_ndxerContexts == null)
                    lock(m_lock)
                        if(m_ndxerContexts == null)
                        {
                            m_ndxerContexts = new KeyIndexer(AppContext.TableManager.ValuesContext.DataProvider);
                            m_ndxerContexts.Connect();
                        }

                return m_ndxerContexts;
            }
        }

        public KeyIndexer Countries
        {
            get
            {
                if(m_ndxerCountries == null)
                    lock(m_lock)
                        if(m_ndxerCountries == null)
                        {
                            m_ndxerCountries = new KeyIndexer(AppContext.TableManager.Countries.DataProvider);
                            m_ndxerCountries.Connect();
                        }

                return m_ndxerCountries;
            }
        }

        public KeyIndexer Incoterms
        {
            get
            {
                if(m_ndxerIncoterms == null)
                    lock(m_lock)
                        if(m_ndxerIncoterms == null)
                        {
                            m_ndxerIncoterms = new KeyIndexer(AppContext.TableManager.Incoterms.DataProvider);
                            m_ndxerIncoterms.Connect();
                        }

                return m_ndxerIncoterms;
            }
        }

        public KeyIndexer Places
        {
            get
            {
                if(m_ndxerPlaces == null)
                    lock(m_lock)
                        if(m_ndxerPlaces == null)
                        {
                            m_ndxerPlaces = new KeyIndexer(AppContext.TableManager.Places.DataProvider);
                            m_ndxerPlaces.Connect();
                        }

                return m_ndxerPlaces;
            }
        }

        public KeyIndexer Currencies
        {
            get
            {
                if(m_ndxerCurrencies == null)
                    lock(m_lock)
                        if(m_ndxerCurrencies == null)
                        {
                            m_ndxerCurrencies = new KeyIndexer(AppContext.TableManager.Currencies.DataProvider);
                            m_ndxerCurrencies.Connect();
                        }

                return m_ndxerCurrencies;                        
            }
        }

        public KeyIndexer Units
        {
            get
            {
                if(m_ndxerUnits == null)
                    lock(m_lock)
                        if(m_ndxerUnits == null)
                        {
                            m_ndxerUnits = new KeyIndexer(AppContext.TableManager.Units.DataProvider);
                            m_ndxerUnits.Connect();
                        }

                return m_ndxerUnits;
            }
        }

        public bool IsDisposed { get; private set; }


        public void Dispose()
        {
            if(!IsDisposed)
                lock (m_lock)
                    if(!IsDisposed)
                    {
                        m_ndxerValues?.Close();
                        m_ndxerProducts?.Close();
                        //m_ndxerMappingsProdID?.Close();
                        m_ndxerContexts?.Close();
                        m_ndxerCountries?.Close();
                        m_ndxerIncoterms?.Close();
                        m_ndxerPlaces?.Close();
                        m_ndxerCurrencies?.Close();
                        m_ndxerUnits?.Close();
                    }
        }
    }
}
