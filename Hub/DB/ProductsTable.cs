namespace DGD.Hub.DB
{
    sealed class ProductsTable: HubCore.DB.DBTable<Product>
    {
        public ProductsTable(string filePath) :
            base(filePath , "Prodcuts" , HubCore.TablesID.PRODUCT)
        { }
    }
}
