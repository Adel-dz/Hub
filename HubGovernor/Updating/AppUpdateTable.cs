using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Updating
{
    sealed class AppUpdateTable: FuzzyTable<AppUpdate>, ITableRelation
    {
        public AppUpdateTable(string filePath) :
            base(InternalTablesID.APP_UPDATE , "MAJ Hub" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new VersionColumn("Version"),
                new TextColumn("Système requis"),
                new TimeColumn("Crée le"),
                new TimeColumn("Publier le")
            };

        }
    }
}
