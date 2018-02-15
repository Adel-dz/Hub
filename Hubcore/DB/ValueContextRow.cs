using easyLib;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{
    public interface IValueContextRow: IDataRow
    {
        uint CurrencyID { get; }
        uint UnitID { get; }
        uint OriginID { get; }
        uint IncotermID { get; }
        uint PlaceID { get; }
    }




    public class ValueContextRow: DataRow, IValueContextRow
    {
        public ValueContextRow()
        { }

        public ValueContextRow(uint id, uint currencyID, uint unitID, uint originID = 0, 
                uint incotermID = 0, uint placeID = 0):
            base(id)
        {
          
            CurrencyID = currencyID;
            UnitID = unitID;
            OriginID = originID;
            IncotermID = incotermID;
            PlaceID = placeID;
        }


        public uint CurrencyID { get; private set; }
        public uint UnitID { get; private set; }
        public uint OriginID { get; private set; }
        public uint IncotermID { get; private set; }
        public uint PlaceID { get; private set; }



        //protected:
        protected override void DoRead(IReader reader)
        {
            CurrencyID = reader.ReadUInt();
            UnitID = reader.ReadUInt();

            if (CurrencyID == 0 || UnitID == 0)
                throw new CorruptedStreamException();

            OriginID = reader.ReadUInt();
            IncotermID = reader.ReadUInt();
            PlaceID = reader.ReadUInt();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(CurrencyID != 0);
            writer.Write(CurrencyID);

            Assert(UnitID != 0);
            writer.Write(UnitID);

            writer.Write(OriginID);
            writer.Write(IncotermID);
            writer.Write(PlaceID);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                CurrencyID.ToString(),
                UnitID.ToString(),
                OriginID.ToString(),
                IncotermID.ToString(),
                PlaceID.ToString()
            };
        }
    }
}
