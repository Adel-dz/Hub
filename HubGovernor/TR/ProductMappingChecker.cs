using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Linq;




namespace DGD.HubGovernor.TR
{
    sealed class ProductMappingChecker: ITableChecker
    {
        readonly AttrIndexer<uint> m_ndxerMapProdNber;
        readonly KeyIndexer m_ndxerProduct;
        readonly KeyIndexer m_ndxerContext;


        public ProductMappingChecker(AttrIndexer<uint> ndxerMapProdNber, KeyIndexer ndxerProduct, KeyIndexer ndxerContext)
        {
            m_ndxerMapProdNber = ndxerMapProdNber;
            m_ndxerMapProdNber.Connect();

            m_ndxerProduct = ndxerProduct;
            m_ndxerProduct.Connect();

            m_ndxerContext = ndxerContext;
            m_ndxerContext.Connect();            
        }


        public uint TableID => AppContext.TableManager.TRProductsMapping.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            return CheckConstraint5() & CheckConstraint11() & CheckConstraint13() & CheckConstraint17();
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_ndxerContext?.Close();
                m_ndxerMapProdNber?.Close();
                m_ndxerProduct?.Close();

                IsDisposed = true;
            }
        }


        //private:
        bool CheckConstraint5()
        {
            //∀ p1, P2 ∈ TRProductMapping, p1.ProductNumber = p2.ProductNumber <=> p1 = p2;

            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRProductsMapping.Name}");

            bool err = false;
            foreach (uint prodNber in m_ndxerMapProdNber.Attributes)
                if (m_ndxerMapProdNber.Get(prodNber).Count() != 1)
                {
                    logger.PutLine("La contrainte d’intégrité n° 5 est violée par les éléments suivants :");

                    foreach (IDatum d in m_ndxerMapProdNber.Get(prodNber))
                        logger.PutLine(d);

                    logger.PutLine();

                    err = true;
                }


            if (err)
                logger.Flush();


            return !err;
        }

        bool CheckConstraint11()
        {
            //∀ p1, p2 ∈ TRProductMapping (p1.ProductID, p1.ContextID) = (p2.ProductID, p2.ContextID) <=> p1 = p2

            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRProductsMapping.Name}");

            Func<IDatum , Pair<uint , uint>> selctor = d =>
               {
                   var p = d as ProductMapping;
                   return Pair.Create(p.ProductID , p.ContextID);
               };


            bool err = false;
            using (var ndxer = new AttrIndexer<Pair<uint , uint>>(m_ndxerMapProdNber.Source , selctor))
            {
                ndxer.Connect();

                foreach (Pair<uint , uint> item in ndxer.Attributes)
                {
                    var seq = ndxer.Get(item);

                    if (seq.Count() != 1)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 11 est violée par les éléments suivants :");
                        foreach (ProductMapping mapping in seq)
                            logger.PutLine(mapping);

                        logger.PutLine();
                        err = true;
                    }
                }
            }


            if (err)
                logger.Flush();

            return !err;
        }

        bool CheckConstraint13()
        {
            //∀m ∈ ProductMapping  ∃p ∈ Product : p.ID = m.ProducID

            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRProductsMapping.Name}");

            bool err = false;

            if (m_ndxerMapProdNber.Source.Count > 0)
                foreach (ProductMapping pm in m_ndxerMapProdNber.Source.Enumerate())
                    if (m_ndxerProduct.Get(pm.ProductID) == null)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 13 est violée par l'élément suivant :");
                        logger.PutLine(pm);
                        logger.PutLine();

                        err = true;
                    }
            if (err)
                logger.Flush();

            return !err;
        }

        bool CheckConstraint17()
        {
            //∀m ∈ ProductMapping  ∃c ∈ ValueContext : c.ID = m.ContextID

            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.TRProductsMapping.Name}");

            bool err = false;

            if (m_ndxerMapProdNber.Source.Count > 0)
                foreach (ProductMapping pm in m_ndxerMapProdNber.Source.Enumerate())
                    if (m_ndxerContext.Get(pm.ContextID) == null)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 17 est violée par l'élément suivant :");
                        logger.PutLine(pm);
                        logger.PutLine();

                        err = true;
                    }
            if (err)
                logger.Flush();

            return !err;
        }
    }
}
