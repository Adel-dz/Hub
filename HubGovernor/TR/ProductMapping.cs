using DGD.HubCore.DB;
using static System.Diagnostics.Debug;
using easyLib;



namespace DGD.HubGovernor.TR
{
    interface IProductMapping : IDataRow
    {
        uint ProductNumber { get; }
        uint ProductID { get; }
        uint ContextID { get; }
    }



    sealed class ProductMapping: DataRow, IProductMapping
    {
        public ProductMapping(uint id, uint prodNber, uint prodID, uint ctxtID):
            base(id)
        {
            Assert(id > 0);
            Assert(prodID > 0);
            Assert(prodNber > 0);
            Assert(ctxtID > 0);

            ProductNumber = prodNber;
            ProductID = prodID;
            ContextID = ctxtID;
        }

        public ProductMapping()
        { }



        public uint ContextID { get; private set; }
        public uint ProductID { get; private set; }
        public uint ProductNumber { get; private set; }

        public override string ToString() => $"(ID:{ID}, N° Produit:{ProductNumber}, " +
            $"ID Produit:{ProductNumber}, ID Context:{ContextID})";

        public static int Size => sizeof(uint) << 2;


        //protected:
        protected override void DoRead(IReader reader)
        {
            uint prodNber = reader.ReadUInt();
            uint prodID = reader.ReadUInt();
            uint ctxtID = reader.ReadUInt();

            if (prodID == 0 || prodID == 0 || ctxtID == 0)
                throw new CorruptedStreamException();

            ProductID = prodID;
            ProductNumber = prodNber;
            ContextID = ctxtID;
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(ProductNumber != 0);
            writer.Write(ProductNumber);

            Assert(ProductID != 0);
            writer.Write(ProductID);

            Assert(ContextID != 0);
            writer.Write(ContextID);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ProductNumber.ToString(),
                ProductID.ToString(),
                ContextID.ToString()
            };
        }
    }
}
