using DGD.HubGovernor.DB;
using easyLib.DB;
using DGD.HubCore;
using System.Linq;
using System.Collections.Generic;

namespace DGD.HubGovernor.Suppliers
{
    sealed class DataSupplierTable: FuzzyTable<DataSupplier>, ITableRelation
    {
        public DataSupplierTable(string filePath) : 
            base(TablesID.SUPPLIER , "Sources de données" , filePath)
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
