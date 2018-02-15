using DGD.HubCore.DB;
using System;
using easyLib;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.TR
{
    interface ISpotValue: IDataRow
    {
        double Price { get; }
        DateTime Time { get; }
        uint ProductMappingID { get; }
        uint LabelID { get; }
        uint SessionNumber { get; }
        uint PublishedValueID { get; }
    }



    sealed class SpotValue: DataRow, ISpotValue
    {
        public SpotValue(uint id , uint prodMappingID , uint labelID , uint sessionNber , double price ,
                DateTime time , uint pubValID = 0) :
            base(id)
        {
            Assert(id > 0);
            Assert(prodMappingID > 0);
            Assert(labelID > 0);
            Assert(sessionNber > 0);
            Assert(price > 0.0);

            ProductMappingID = prodMappingID;
            LabelID = labelID;
            SessionNumber = sessionNber;
            Price = price;
            Time = time;
            PublishedValueID = pubValID;
        }

        public SpotValue(uint id , uint prodMappingID , uint labelID , uint sessionNber , double price) :
            this(id , prodMappingID , labelID , sessionNber , price , DateTime.Now , 0)
        { }

        public SpotValue()
        {
            Time = DateTime.Now;
        }



        public double Price { get; private set; }
        public uint ProductMappingID { get; private set; }
        public uint SessionNumber { get; private set; }
        public DateTime Time { get; private set; }
        public uint LabelID { get; private set; }
        public uint PublishedValueID { get; set; }

        public override string ToString()
        {
            return $"(ID:{ID}, ID Mapping Produit:{ProductMappingID}, ID Libelle TR:{LabelID}, " +
                $"N° Session{SessionNumber}, Prix{Price}, Temps:{Time}, ID Valeur Publiée:{PublishedValueID})";
        }

        public static int Size => sizeof(uint) * 5 + sizeof(long) + sizeof(double);


        //protected:
        protected override void DoRead(IReader reader)
        {
            uint idProdMap = reader.ReadUInt();
            uint idLabel = reader.ReadUInt();
            uint sessionNber = reader.ReadUInt();
            double price = reader.ReadDouble();

            if (idProdMap == 0 || idLabel == 0 || sessionNber == 0 || price == 0.0)
                throw new CorruptedStreamException();

            Time = reader.ReadTime();
            PublishedValueID = reader.ReadUInt();

            ProductMappingID = idProdMap;
            LabelID = idLabel;
            SessionNumber = sessionNber;
            Price = price;
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(ProductMappingID != 0);
            writer.Write(ProductMappingID);

            Assert(LabelID != 0);
            writer.Write(LabelID);

            Assert(SessionNumber != 0);
            writer.Write(SessionNumber);

            Assert(Price > 0.0);
            writer.Write(Price);

            writer.Write(Time);
            writer.Write(PublishedValueID);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                SessionNumber.ToString(),
                Price.ToString(),
                Time.ToString(),
                ProductMappingID.ToString(),
                LabelID.ToString(),
                PublishedValueID == 0 ? "" : PublishedValueID.ToString()
            };
        }
    }
}
