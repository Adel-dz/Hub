using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class UnitsTable: DBTable<Unit>
    {
        public UnitsTable(string filePath) :
            base(filePath , "units" , TablesID.UNIT)
        { }
    }
}
