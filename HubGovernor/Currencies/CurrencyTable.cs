using System;
using System.Collections.Generic;
using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;

namespace DGD.HubGovernor.Currencies
{
    sealed class CurrencyTable: FuzzyDataTable<Currency>, ITableRelation
    {
        public CurrencyTable(string filePath) : 
            base(TablesID.CURRENCY , "Monnaies" , filePath)
        { }


        public IEnumerable<uint> RelatedTables
        {
            get { yield return TablesID.COUNTRY; }
        }


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Nom"),
                new IntegerColumn("ID Pays"),
                new TextColumn("Description")
            };
        }
    }
}
