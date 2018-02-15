using System.Collections.Generic;
using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Linq;

namespace DGD.HubGovernor.Strings
{
    sealed class SharedTextTable: FuzzyTable<SharedText>, ITableRelation
    {
        public SharedTextTable(string filePath) :
            base(TablesID.SHARED_TEXT , "Texte partagé" , filePath)
        { }

        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();



        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Texte")
            };
        }
    }
}
