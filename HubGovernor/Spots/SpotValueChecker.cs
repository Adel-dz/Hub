using DGD.HubGovernor.DB;
using DGD.HubGovernor.Log;
using easyLib.DB;
using System;
using System.Linq;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Spots
{
    sealed class SpotValueChecker: ITableChecker
    {
        readonly IDatumProvider m_srcSpotValues;


        public SpotValueChecker(IDatumProvider srcValues)
        {
            Assert(srcValues != null);

            m_srcSpotValues = srcValues;
            m_srcSpotValues.Connect();
        }


        public uint TableID => AppContext.TableManager.SpotValues.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.SpotValues.Name}");
            logger.PutLine("La contrainte d’intégrité n° 8 est violée par les éléments suivants :");            

            Func<IDatum , IDatum, bool> comparer = (d1, d2) =>
             {
                 var sv1 = d1 as SpotValue;
                 var sv2 = d2 as SpotValue;

                 return sv1.ProductID == sv2.ProductID && sv1.SpotTime == sv2.SpotTime && 
                    sv1.SupplierID == sv2.SupplierID && sv1.ValueContextID == sv2.ValueContextID;
             };

            bool anyErr = false;

            using (var ndxer = new AttrIndexer<SpotValue>(m_srcSpotValues , d => d as SpotValue))
            {
                ndxer.Connect();

                foreach (SpotValue sv in ndxer.Attributes)
                {
                    if (ndxer.IndexOf(sv).Count() > 1)
                    {
                        logger.PutLine("Les éléments suivant sont identiques:");
                        foreach (var item in ndxer.Get(sv))
                            logger.PutLine(item);

                        logger.PutLine();

                        anyErr = true;
                    }                    
                }
            }

            return !anyErr;
        }


        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_srcSpotValues?.Close();
            }
        }
    }
}
