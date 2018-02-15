using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class IncotermsTable: DBTable<Incoterm>
    {
        public IncotermsTable(string filePath) :
            base(filePath , "incoterms" , TablesID.INCOTERM)
        { }
    }
}
