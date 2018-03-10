using DGD.HubGovernor.DB;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Clients
{

    sealed class HubClientEnvTable: FuzzyTable<HubClientEnvironment>, ITableRelation
    {
        public HubClientEnvTable(string filePath) :
            base(InternalTablesID.CLIENT_ENV , "Environnement Client" , filePath)
        { }

        public IEnumerable<uint> RelatedTables => new uint[] { InternalTablesID.HUB_CLIENT };

        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("ID Client"),
                new TextColumn("Utilsateur"),
                new TextColumn("Nom de la station"),
                new TextColumn("OS Version"),
                new TextColumn("OS Architecture"),
                new TextColumn("Version du Hub"),
                new TextColumn("Architecture du Hub")
            };
        }
    }
}
