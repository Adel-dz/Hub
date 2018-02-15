using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface ICountryRow: IDataRow
    {
        string Name { get; }
        ushort InternalCode { get; }
        string IsoCode { get; }
    }



    public class CountryRow: DataRow, ICountryRow
    {
        public const ushort MAX_INTERNAL_CODE = 999;
        string m_isoCode;
        

        private CountryRow(string name , string isoCode , uint id , ushort internalCode) :
            base(id)
        {
            Name = name;
            InternalCode = internalCode;
            IsoCode = isoCode;
        }

        public CountryRow(uint id , string name , ushort internalCode , string isoCode = "") :
            this(name , isoCode ?? "" , id , internalCode)
        { }

        public CountryRow():
            this("","",0,0)
        { }


        public ushort InternalCode { get; private set; }
        public string Name { get; private set; }

        public string IsoCode
        {
            get { return m_isoCode; }
            set { m_isoCode = value ?? ""; }
        }

        //protected:
        protected override void DoRead(IReader reader)
        {
            InternalCode = reader.ReadUShort();
            Name = reader.ReadString();
            IsoCode = reader.ReadString();

            if (InternalCode == 0 || InternalCode > MAX_INTERNAL_CODE || string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(InternalCode > 0);
            Assert(InternalCode <= MAX_INTERNAL_CODE);
            Assert(!string.IsNullOrWhiteSpace(Name));

            writer.Write(InternalCode);
            writer.Write(Name);
            writer.Write(IsoCode);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name,
                InternalCode.ToString("000"),
                IsoCode
            };
        }
    }

}


