using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;



namespace DGD.HubGovernor.TR
{
    sealed class TRLabelTable: FuzzyTable<TRLabel>, ITableRelation
    {
        public TRLabelTable(string filePath) :
            base(InternalTablesID.TR_LABEL , "Libellé TR" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => new[] { InternalTablesID.TR_PRODUCT_MAPPING };

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("N° Produit"),
                new TextColumn("Libellé TR")
            };
        }
    }
}
