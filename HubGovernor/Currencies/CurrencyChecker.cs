using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Currencies
{
    sealed class CurrencyChecker: ITableChecker
    {
        readonly IAttrIndexer<string> m_ndxerNames;


        public CurrencyChecker(IAttrIndexer<string> ndxerNames)
        {
            Assert(ndxerNames != null);

            m_ndxerNames = ndxerNames;
            m_ndxerNames.Connect();
        }


        public uint TableID => AppContext.TableManager.Currencies.ID;
        public bool IsDisposed { get; private set; }


        public bool Check()
        {
            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Currencies.Name}");
            logger.PutLine("La contrainte d’intégrité n° 3 est violée par les éléments suivants :");


            bool anyErr = false;

            foreach(string name in m_ndxerNames.Attributes)
            {
                IEnumerable<IDatum> currencies = m_ndxerNames.Get(name);

                if(currencies.Count() > 1)
                {
                    logger.PutLine($"Noms identiques ({name}):");

                    foreach (Currency c in currencies)
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
                m_ndxerNames.Close();
                IsDisposed = true;
            }
        }
    }
}
