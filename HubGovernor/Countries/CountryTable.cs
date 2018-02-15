using easyLib.DB;
using DGD.HubCore;
using DGD.HubGovernor.DB;
using System.Collections.Generic;
using System.Linq;

namespace DGD.HubGovernor.Countries
{
    sealed class CountryTable: FuzzyDataTable<Country>, ITableRelation
    {
        public CountryTable(string filePath) 
            : base(TablesID.COUNTRY , "Pays" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Pays"),
                new TextColumn("Code pays"),    //string camparison is ok
                new TextColumn("Code ISO")
            };         
        }
    }
}
