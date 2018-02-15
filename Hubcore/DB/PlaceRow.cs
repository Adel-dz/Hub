using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IPlaceRow: IDataRow
    {
        string Name { get; }
        uint CountryID { get; }
    }




    public class PlaceRow: DataRow, IPlaceRow
    {
        private PlaceRow(string name, uint countryID, uint id):
            base(id)
        {
            Name = name;
            CountryID = countryID;
        }

        public PlaceRow():
            this("",0,0)
        { }

        public PlaceRow(uint id, string name, uint countryID = 0):
            this(name, countryID, id)
        { }
        
        

        public uint CountryID { get; private set; }
        public string Name { get; private set; }


        //public override string ToString() => Name;

        //protected:
        protected override void DoRead(IReader reader)
        {
            CountryID = reader.ReadUInt();
            Name = reader.ReadString();

            if (string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write(CountryID);

            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name,
                CountryID == 0 ? "" : CountryID.ToString()
            };
        }
    }
}
