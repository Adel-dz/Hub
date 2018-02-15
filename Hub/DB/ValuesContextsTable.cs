using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class ValuesContextsTable: DBTable<ValueContext>
    {
        public ValuesContextsTable(string filePath) :
            base(filePath , "VContexts" , TablesID.VALUE_CONTEXT)
        { }
    }
}
