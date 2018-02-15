using DGD.HubCore;
using System;
using static System.Diagnostics.Debug;



namespace DGD.Hub.DB
{
    sealed class SpotValue: HubCore.DB.SpotValueRow
    {
        public SpotValue()
        { }

        public SpotValue(uint id , double price , DateTime spotTime , uint productID , uint valContextID ,
                uint supplierID , uint descrID) :
            base(id , price , spotTime , productID , valContextID , supplierID , descrID)
        { }


        public Product Product
        {
            get
            {
                Assert(ProductID > 0);

                return Program.TablesManager.GetKeyIndexer(TablesID.PRODUCT).Get(ProductID) as Product;
            }
        }

        public ValueContext ValueContext
        {
            get
            {
                Assert(ValueContextID > 0);

                return Program.TablesManager.GetKeyIndexer(TablesID.VALUE_CONTEXT).Get(ValueContextID) as ValueContext;
            }
        }

        public DataSupplier DataSupplier
        {
            get
            {
                Assert(SupplierID > 0);

                return Program.TablesManager.GetKeyIndexer(TablesID.SUPPLIER).Get(SupplierID) as DataSupplier;
            }
        }

        public SharedText Description => DescriptionID == 0 ? null :
            Program.TablesManager.GetKeyIndexer(TablesID.SHARED_TEXT).Get(DescriptionID) as SharedText;
    }
}
