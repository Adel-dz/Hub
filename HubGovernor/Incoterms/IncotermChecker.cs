using DGD.HubGovernor.DB;
using DGD.HubGovernor.Log;
using easyLib.DB;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Incoterms
{
    sealed class IncotermChecker: ITableChecker
    {
        IAttrIndexer<string> m_ndxerNames;


        public IncotermChecker(IAttrIndexer<string> ndxerNames)
        {
            Assert(ndxerNames != null);

            m_ndxerNames = ndxerNames;
            m_ndxerNames.Connect();
        }


        public uint TableID => AppContext.TableManager.Incoterms.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Incoterms.Name}");
            logger.PutLine("La contrainte d’intégrité n° 9 est violée par les éléments suivants :");


            bool anyErr = false;

            foreach (string name in m_ndxerNames.Attributes)
            {
                IEnumerable<IDatum> icts = m_ndxerNames.Get(name);

                if (icts.Count() > 1)
                {
                    logger.PutLine($"Noms identiques ({name}):");

                    foreach (Incoterm c in icts)
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
