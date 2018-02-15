using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Updating
{
    sealed class TransactionTable: FramedTable<Transaction>, ITableRelation
    {
        public TransactionTable(string filePath) :
            base(InternalTablesID.TRANSACTION , "Transactions" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();

        public new void Reset()
        {
            base.Reset();
        }


        //protected:
        protected override int DatumSize => Transaction.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TimeColumn("Date"),
                new IntegerColumn("Action"),
                new TextColumn("Table"),
                new IntegerColumn("Ligne ID")
            };
        }
    }
}
