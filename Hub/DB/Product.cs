namespace DGD.Hub.DB
{
    sealed class Product: HubCore.DB.ProductRow
    {
        public Product()
        { }

        public Product(uint id , string name , HubCore.SubHeading subHeading):
            base(id, name, subHeading)
        { }
    }
}
