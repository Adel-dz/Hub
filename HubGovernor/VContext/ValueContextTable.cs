using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;

namespace DGD.HubGovernor.VContext
{
    sealed class ValueContextTable: FramedTable<ValueContext>, ITableRelation
    {
        public ValueContextTable(string filePath) :
            base(TablesID.VALUE_CONTEXT , "Contextes de valeurs" , filePath)
        { }


        public IEnumerable<uint> RelatedTables
        {
            get
            {
                yield return TablesID.UNIT;
                yield return TablesID.CURRENCY;
                yield return TablesID.COUNTRY;
                yield return TablesID.INCOTERM;
                yield return TablesID.PLACE;
            }
        }

        //protected:
        protected override int DatumSize => ValueContext.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("ID Monnaie"),
                new IntegerColumn("ID Unité"),
                new IntegerColumn("ID Origine"),
                new IntegerColumn("ID Incoterm"),
                new IntegerColumn("ID Lieu")
            };
        }
    }
}
