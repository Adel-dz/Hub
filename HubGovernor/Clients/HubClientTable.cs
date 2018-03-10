using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;

namespace DGD.HubGovernor.Clients
{
    sealed class HubClientTable: FuzzyTable<HubClient>, ITableRelation
    {
        public HubClientTable(string filePath) :
            base(InternalTablesID.HUB_CLIENT , "Clients" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => new [] {InternalTablesID.USER_PROFILE };


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("ID Profile"),
                new TimeColumn("Crée le"),
                new TextColumn("Contact"),
                new TextColumn("Tél."),
                new TextColumn("e-Mail")
            };
        }
    }
}
