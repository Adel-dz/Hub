using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface ICurrencyRow : IDataRow
    {
        string Name { get; }
        uint CountryID { get; }
        string Description { get; }
    }


    public class CurrencyRow: DataRow, ICurrencyRow
    {
        string m_decsr;


        private CurrencyRow(string name, string descr, uint countryID, uint id):
            base(id)
        {
            Name = name;
            Description = descr;
            CountryID = countryID;
        }
        public CurrencyRow(uint id , string name , uint countryID = 0 , string descr = "") :
            this(name , descr , countryID , id)
        { }

        public CurrencyRow() :
            this("" , "" , 0 , 0)
        { }



        public uint CountryID { get; protected set; }        
        public string Name { get; private set; }

        public string Description
        {
            get { return m_decsr; }
            protected set { m_decsr = value ?? ""; }
        }
                

        //protected:
        protected override void DoRead(IReader reader)
        {
            CountryID = reader.ReadUInt();

            Name = reader.ReadString();
            if (string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();
                        
            Description = reader.ReadString();
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write(CountryID);

            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
            writer.Write(Description);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name,
                CountryID.ToString(),
                Description
            };
        }
    }
}
