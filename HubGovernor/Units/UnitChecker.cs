using DGD.HubGovernor.DB;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Units
{
    sealed class UnitChecker: ITableChecker
    {
        readonly IDatumProvider m_srcUnits;

        
        public UnitChecker(IDatumProvider dp)
        {
            Assert(dp != null);

            m_srcUnits = dp;
            m_srcUnits.Connect();
        }


        public uint TableID => AppContext.TableManager.Units.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            //contrainte no 10 = Name et unique
            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Suppiers.Name}");
            

            bool err = false;

            using (var ndxer = new AttrIndexer<string>(m_srcUnits , d => (d as Unit).Name , StringComparer.CurrentCultureIgnoreCase))
            {
                ndxer.Connect();

                foreach(string name in ndxer.Attributes)
                    if(ndxer.Get(name).Count() > 1)
                    {
                        logger.PutLine("La contrainte d’intégrité n° 10 est violée par les éléments suivants :");
                        foreach (IDatum d in ndxer.Get(name))
                            logger.PutLine(d);

                        logger.PutLine();
                        err = true;
                    }
            }

            if (err)
                logger.Flush();

            return !err;
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_srcUnits.Close();
                IsDisposed = true;
            }
        }
    }
}
