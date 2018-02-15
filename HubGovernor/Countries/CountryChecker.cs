using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Countries
{
    sealed class CountryChecker: ITableChecker
    {
        readonly IAttrIndexer<string> m_ndxerName;
        readonly IAttrIndexer<ushort> m_ndxerInternalCode;
        readonly IAttrIndexer<string> m_ndxerIsoCode;


        public CountryChecker(IAttrIndexer<string> ndxerNames, IAttrIndexer<ushort> ndxerCtryCodes, 
            IAttrIndexer<string> ndxerIsoCodes)
        {
            Assert(ndxerNames != null);
            Assert(ndxerCtryCodes != null);
            Assert(ndxerIsoCodes != null);

            m_ndxerInternalCode = ndxerCtryCodes;
            m_ndxerInternalCode.Connect();

            m_ndxerIsoCode = ndxerIsoCodes;
            m_ndxerIsoCode.Connect();

            m_ndxerName = ndxerNames;
            m_ndxerName.Connect();
        }


        public uint TableID => TablesID.COUNTRY;
        public bool IsDisposed { get; private set; }


        public bool Check()
        {
            //contrainte 2
            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Countries.Name}");
            logger.PutLine("La contrainte d’intégrité n° 2 est violée par les éléments suivants :");

            bool anyErr = false;

            //nom unique
            foreach(string name in m_ndxerName.Attributes)
            {
                IEnumerable<IDatum> countries = m_ndxerName.Get(name);

                if (countries.Count() > 1)
                {
                    logger.PutLine($"Noms identiques ({name}):");

                    foreach (Country c in countries)
                        logger.PutLine(c.ToString());

                    logger.PutLine();
                    anyErr = true;
                }
            }


            //code pays unique
            foreach(ushort code in m_ndxerInternalCode.Attributes)
            {
                IEnumerable<IDatum> countries = m_ndxerInternalCode.Get(code);

                if(countries.Count() > 1)
                {
                    logger.PutLine($"Codes pays identiques ({code}):");

                    foreach (Country c in countries)
                        logger.PutLine(c.ToString());

                    logger.PutLine();
                    anyErr = true;
                }
            }


            //iso code est nul ou unique
            foreach(string isoCode in  m_ndxerIsoCode.Attributes.Where(s=>!string.IsNullOrWhiteSpace(s)))
            {
                IEnumerable<IDatum> countries = m_ndxerIsoCode.Get(isoCode);

                if (countries.Count() > 1)
                {
                    logger.PutLine($"Iso Codes identiques ({isoCode}):");

                    foreach (Country c in countries)
                        logger.PutLine(c.ToString());

                    logger.PutLine();
                    anyErr = true; 
                }
            }


            if (anyErr)
                logger.Flush();

            return !anyErr;
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_ndxerInternalCode.Close();
                m_ndxerIsoCode.Close();
                m_ndxerName.Close();

                IsDisposed = true;
            }
        }
    }
}
