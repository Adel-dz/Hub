using DGD.HubGovernor.DB;
using DGD.HubGovernor.Log;
using easyLib.DB;
using System;
using System.Linq;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Suppliers
{
    sealed class DataSupplierChecker: ITableChecker
    {
        readonly IDatumProvider m_srcSuppmiers;


        public DataSupplierChecker(IDatumProvider dbSuppliers)
        {
            Assert(dbSuppliers != null);

            m_srcSuppmiers = dbSuppliers;
            m_srcSuppmiers.Connect();
        }
        


        public uint TableID => AppContext.TableManager.Suppiers.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Suppiers.Name}");
            logger.PutLine("La contrainte d’intégrité n° 6 est violée par les éléments suivants :");


            bool anyErr = false;

            using (var ndxer = new AttrIndexer<string>(m_srcSuppmiers , d => (d as DataSupplier).Name , 
                StringComparer.CurrentCultureIgnoreCase))
            {
                ndxer.Connect();

                foreach(string name in ndxer.Attributes)
                {
                    if(ndxer.Get(name).Count() != 1)
                    {
                        logger.PutLine("Les éléments suivant sont identiques:");
                        foreach (var item in ndxer.Get(name))
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
            if(!IsDisposed)
            {
                m_srcSuppmiers?.Close();

                IsDisposed = true;
            }
        }
    }
}
