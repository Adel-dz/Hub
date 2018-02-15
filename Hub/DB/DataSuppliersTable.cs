using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class DataSuppliersTable: DBTable<DataSupplier>
    {
        public DataSuppliersTable(string filePath) :
            base(filePath , "suppliers" , TablesID.SUPPLIER)
        { }

    }
}
