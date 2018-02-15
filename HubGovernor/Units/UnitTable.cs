using DGD.HubGovernor.DB;
using easyLib.DB;
using DGD.HubCore;
using System.Linq;
using System.Collections.Generic;

namespace DGD.HubGovernor.Units
{
    sealed class UnitTable: FuzzyDataTable<Unit>, ITableRelation
    {
        public UnitTable(string filePath) : 
            base(TablesID.UNIT , "Unités" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();

        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Nom"),
                new TextColumn("Description")
            };
        }
    }
}
