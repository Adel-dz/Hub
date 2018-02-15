namespace DGD.Hub.DB
{
    sealed class DataSupplier : HubCore.DB.DataSupplierRow
    {
        public DataSupplier()
        { }


        public DataSupplier(uint id , string name): 
            base(id, name)
        { }
    }
}
