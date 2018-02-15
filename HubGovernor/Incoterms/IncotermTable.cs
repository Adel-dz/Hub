using DGD.HubGovernor.DB;
using easyLib.DB;
using DGD.HubCore;
using System.Linq;
using System.Collections.Generic;

namespace DGD.HubGovernor.Incoterms
{
    sealed class IncotermTable: FuzzyDataTable<Incoterm>, ITableRelation
    {
        public IncotermTable(string filePath) : 
            base(TablesID.INCOTERM , "Incoterms" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();

        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Nom")
            };
        }
    }
}
