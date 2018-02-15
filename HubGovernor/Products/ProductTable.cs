using DGD.HubGovernor.DB;
using easyLib.DB;
using DGD.HubCore;
using System.Linq;
using System.Collections.Generic;

namespace DGD.HubGovernor.Products
{
    sealed class ProductTable: FuzzyDataTable<Product>, ITableRelation
    {
        public ProductTable(string filePath) :
            base(TablesID.PRODUCT , "Produits" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("SPT10"),
                new TextColumn("Libellé")
            };
        }
    }
}
