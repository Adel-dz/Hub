using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;

namespace DGD.HubGovernor.Products
{
    sealed class ProductChecker: ITableChecker
    {
        readonly IDatumProvider m_srcProducts;


        public ProductChecker(IDatumProvider srcProducts)
        {
            Assert(srcProducts != null);

            m_srcProducts = srcProducts;
            m_srcProducts.Connect();
        }

        public uint TableID => AppContext.TableManager.Products.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            //contrainte 1

            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Products.Name}");
            logger.PutLine("La contrainte d’intégrité n° 1 est violée par les éléments suivants :");

            Func<IDatum , Pair<string , SubHeading>> selector = (d) =>
            {
                var p = d as Product;
                return Pair.Create(p.Name , p.SubHeading);
            };
            
            bool anyErr = false;

            using (var ndxer = new AttrIndexer<Pair<string, SubHeading>>(m_srcProducts , selector))
            {
                ndxer.Connect();

                foreach (var pair in ndxer.Attributes)
                {
                    IEnumerable<IDatum> products = ndxer.Get(pair);

                    if (products.Count() > 1)
                    {
                        logger.PutLine("Eléments suivant sont identiques:");

                        foreach (Product prod in products)
                            logger.PutLine(prod);

                        logger.PutLine();
                        anyErr = true;
                    }
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
                m_srcProducts.Close();
                IsDisposed = true;
            }
        }
    }
}
