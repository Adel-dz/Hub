using System;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface ISpotValueRow : IDataRow
    {
        double Price { get; }
        DateTime SpotTime { get; }
        uint ProductID { get; }
        uint ValueContextID { get; }
        uint SupplierID { get; }
        uint DescriptionID { get; }
    }



    public class SpotValueRow: DataRow, ISpotValueRow
    {
        public SpotValueRow()
        { }

        public SpotValueRow(uint id, double price, DateTime spotTime, uint productID, 
            uint valContextID, uint supplierID, uint descrID ):
            base(id)
        {
            Price = price;
            SpotTime = spotTime;
            ProductID = productID;
            ValueContextID = valContextID;
            SupplierID = supplierID;
            DescriptionID = descrID;
        }


        public double Price { get; private set; }
        public uint ProductID { get; private set; }
        public DateTime SpotTime { get; private set; }
        public uint SupplierID { get; private set; }
        public uint ValueContextID { get; private set; }        
        public uint DescriptionID { get; private set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            Price = reader.ReadDouble();
            ProductID = reader.ReadUInt();
            SupplierID = reader.ReadUInt();
            ValueContextID = reader.ReadUInt();

            if (Price == 0 || ProductID == 0 || SupplierID == 0 || ValueContextID == 0)
                throw new CorruptedStreamException();

            SpotTime = reader.ReadTime();
            DescriptionID = reader.ReadUInt();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(Price > 0);
            writer.Write(Price);            

            Assert(ProductID != 0);
            writer.Write(ProductID);

            Assert(SupplierID != 0);
            writer.Write(SupplierID);

            Assert(ValueContextID != 0);
            writer.Write(ValueContextID);

            writer.Write(SpotTime);
            writer.Write(DescriptionID);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                SpotTime.ToShortDateString(),
                Price.ToString(),
                ValueContextID.ToString(),
                ProductID.ToString(),
                SupplierID.ToString(),
                DescriptionID.ToString()
            };
        }
    }
}
