using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Collections.Generic;

namespace DGD.HubGovernor.Clients
{
    sealed class ClientStatusTable: FramedTable<ClientStatus>, ITableRelation
    {
        public ClientStatusTable(string filePath) :
            base(InternalTablesID.CLIENT_STATUS , "Status Clients" , filePath)
        { }

        public IEnumerable<uint> RelatedTables => new[] { InternalTablesID.HUB_CLIENT };

        //protected:
        protected override int DatumSize => ClientStatus.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID Client"),
                new TextColumn("Status")
            };
        }
    }
}
