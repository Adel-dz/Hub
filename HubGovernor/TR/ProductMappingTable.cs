using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;

namespace DGD.HubGovernor.TR
{


    sealed class ProductMappingTable: FramedTable<ProductMapping>, ITableRelation
    {
        public ProductMappingTable(string filePath) :
            base(InternalTablesID.TR_PRODUCT_MAPPING , "Produits correspondance (TR)" , filePath)
        { }



        public IEnumerable<uint> RelatedTables => new[]
        {
            TablesID.VALUE_CONTEXT ,
            TablesID.PRODUCT
        };


        //protected:
        protected override int DatumSize => ProductMapping.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("N° Produit"),
                new IntegerColumn("ID Produit"),
                new IntegerColumn("ID Context"),
            };
        }
    }
}
