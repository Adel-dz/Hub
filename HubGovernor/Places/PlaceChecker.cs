using DGD.HubGovernor.DB;
using easyLib;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Places
{
    sealed class PlaceChecker: ITableChecker
    {
        readonly IDatumProvider m_srcPlaces;


        public PlaceChecker(IDatumProvider srcPlaces)
        {
            Assert(srcPlaces != null);

            m_srcPlaces = srcPlaces;
            m_srcPlaces.Connect();
        }

        public uint TableID => AppContext.TableManager.Places.ID;
        public bool IsDisposed { get; private set; }

        public bool Check()
        {
            //contrainte 4

            var logger = new TextLogger(LogSeverity.Warning);
            logger.PutLine("*** Control d’intégrité ***");
            logger.PutLine($"Table: {AppContext.TableManager.Places.Name}");
            logger.PutLine("La contrainte d’intégrité n° 4 est violée par les éléments suivants :");

            Func<IDatum , Pair<string , uint>> filter = d =>
              {
                  var p = d as Place;

                  return new Pair<string , uint>(p.Name , p.CountryID);
              };


            bool anyErr = false;

            using (AttrIndexer<Pair<string , uint>> ndxer = new AttrIndexer<Pair<string , uint>>(m_srcPlaces , filter))
            {
                ndxer.Connect();

                foreach(Pair<string,uint> item in ndxer.Attributes)
                {
                    IEnumerable<IDatum> data = ndxer.Get(item);

                    if (data.Count() > 1)
                    {
                        logger.PutLine("Eléments suivant sont identiques:");

                        foreach (Place p in data)
                            logger.PutLine($"(Lieu:{p.Name}, ID Pays:{p.CountryID})");

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
                m_srcPlaces?.Close();
                IsDisposed = true;
            }
        }
    }
}
