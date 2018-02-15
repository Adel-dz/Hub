using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.VContext
{
    sealed class ValueContextChecker: ITableChecker
    {
        readonly IDatumProvider m_srcContext;

        public ValueContextChecker(IDatumProvider dp)
        {
            Assert(dp != null);

            m_srcContext = dp;
            m_srcContext.Connect();
        }


        public uint TableID => AppContext.TableManager.ValuesContext.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            //contrainte 14

            var logger = new EventLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.ValuesContext.Name}");
            

            Func<IDatum , Tuple<uint , uint , uint , uint , uint>> selector = delegate (IDatum d)
                      {
                          var vc = d as ValueContext;
                          return Tuple.Create(vc.CurrencyID , vc.IncotermID , vc.OriginID , vc.PlaceID , vc.UnitID);
                      };

            bool err = false;

            using (var ndxer = new AttrIndexer<Tuple<uint , uint , uint , uint , uint>>(m_srcContext , selector))
            {
                ndxer.Connect();

                foreach(var tpl in ndxer.Attributes)
                    if(ndxer.Get(tpl).Count() > 1)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 14 est violée par les éléments suivants :");
                        foreach (IDatum d in ndxer.Get(tpl))
                            logger.PutLine(d);

                        logger.PutLine();
                        err = true;
                    }

                if (err)
                    logger.Flush();

                return !err;
            }
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_srcContext.Close();
                IsDisposed = true;
            }
        }
    }
}
