using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR
{
    sealed class TRLabelChecker: ITableChecker
    {
        readonly AttrIndexer<uint> m_ndxerLabelProdNbers;
        readonly AttrIndexer<uint> m_ndxerMappingProdNbers;


        public TRLabelChecker(AttrIndexer<uint> srcProdNbersLabels, AttrIndexer<uint> srcProdNbersMapping)
        {
            Assert(srcProdNbersLabels != null);
            Assert(srcProdNbersMapping != null);


            m_ndxerLabelProdNbers = srcProdNbersLabels;
            m_ndxerLabelProdNbers.Connect();

            m_ndxerMappingProdNbers = srcProdNbersMapping;
            m_ndxerMappingProdNbers.Connect();
        }



        public uint TableID => AppContext.TableManager.TRLabels.ID;
        public bool IsDisposed { get; private set; }


        public bool Check()
        {
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Incoterms.Name}");
            logger.PutLine("La contrainte d’intégrité n° 16 est violée par les éléments suivants :");

            bool err = false;

            foreach(uint prodID in m_ndxerLabelProdNbers.Attributes)            
                if(m_ndxerMappingProdNbers.Get(prodID).Any())
                {
                    logger.PutLine(m_ndxerLabelProdNbers.Get(prodID).First().ToString());

                    logger.PutLine();
                    err = true;
                }


            if (err)
                logger.Flush();

            return !err;
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_ndxerLabelProdNbers.Close();
                m_ndxerMappingProdNbers.Close();

                IsDisposed = true;
            }
        }
    }
}
