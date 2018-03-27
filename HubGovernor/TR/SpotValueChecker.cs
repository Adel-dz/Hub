using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Linq;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.TR
{
    public class SpotValueChecker: ITableChecker
    {
        readonly KeyIndexer m_labels;
        readonly KeyIndexer m_ProductMappings;
        readonly IDatumProvider m_srcSpotValues;


        public SpotValueChecker(IDatumProvider spotValues, KeyIndexer labels, KeyIndexer prodMappings)
        {
            Assert(spotValues != null);
            Assert(labels != null);
            Assert(prodMappings != null);

            m_srcSpotValues = spotValues;
            m_srcSpotValues.Connect();

            m_ProductMappings = prodMappings;
            m_ProductMappings.Connect();

            m_labels = labels;
            m_labels.Connect();
        }

        public uint TableID => AppContext.TableManager.TRSpotValues.ID;
        public bool IsDisposed { get; private set; }



        public bool Check()
        {
            return CheckConstraint18() & CheckConstraint19() & CheckConstraint20();
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_labels?.Close();
                m_ProductMappings?.Close();
                m_srcSpotValues?.Close();
            }
        }


        //private:
        bool CheckConstraint18()
        {
            //∀v ∈ SpotValueTable  ∃l ∈ TRLabelTable : v.LabelID = l.ID

            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRSpotValues.Name}");

            bool err = false;

            using (var ndxer = new AttrIndexer<uint>(m_srcSpotValues , d => (d as SpotValue).LabelID))
            {
                ndxer.Connect();

                foreach (uint idLabel in ndxer.Attributes)
                    if (m_labels.Get(idLabel) == null)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 18 est violée par l'élément suivant :");
                        logger.PutLine(ndxer.Get(idLabel));

                        logger.PutLine();

                        err = true;
                    }
            }


            if (err)
                logger.Flush();

            return !err;
        }

        bool CheckConstraint19()
        {
            // ∀v ∈ TRSpotValueTable ∃m ∈ TRProductMappingTable: v.ProductMappingID = m.ID;

            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRSpotValues.Name}");

            bool err = false;
            using (var ndxer = new AttrIndexer<uint>(m_srcSpotValues , d => (d as SpotValue).ProductMappingID))
            {
                ndxer.Connect();

                foreach (uint idMapping in ndxer.Attributes)
                    if (m_ProductMappings.Get(idMapping) == null)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 19 est violée par l'élément suivant :");
                        logger.PutLine(ndxer.Get(idMapping));

                        logger.PutLine();

                        err = true;
                    }
            }


            if (err)
                logger.Flush();

            return !err;

        }

        bool CheckConstraint20()
        {
            //∀v1, v2 ∈ TRSpotValueTable (v1.ProductMappingId, v1.Time) = (v2.ProductMappingId, v2.Time) <=> v1=v2

            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRSpotValues.Name}");


            Func<IDatum , Pair<uint , DateTime>> selector = d =>
              {
                  var sv = d as SpotValue;
                  return Pair.Create(sv.ProductMappingID , sv.Time);
              };

            bool err = false;
            using (var ndxer = new AttrIndexer<Pair<uint , DateTime>>(m_srcSpotValues , selector))
            {
                ndxer.Connect();

                foreach (var pair in ndxer.Attributes)
                {
                    if(ndxer.Get(pair).Count() != 1)
                    {
                        {
                            logger.PutLine("La contrainte d’intégrité n° 20 est violée par les éléments suivants :");

                            foreach (var datum in ndxer.Get(pair))
                                logger.PutLine(datum);

                            logger.PutLine();

                            err = true;
                        }
                    }
                }
            }


            if (err)
                logger.Flush();

            return !err;
        }
    }
}
