using DGD.HubGovernor.DB;
using easyLib.DB;
using DGD.HubCore;
using System.Collections.Generic;

namespace DGD.HubGovernor.Places
{
    sealed class PlaceTable: FuzzyDataTable<Place>, ITableRelation
    {
        public PlaceTable(string filePath) :
            base(TablesID.PLACE , "Lieux" , filePath)
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
                new IntegerColumn("ID Pays")
            };
        }
    }
}
