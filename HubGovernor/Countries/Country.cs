using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Countries
{
    public class Country: CountryRow, ITaggedRow
    {
        public Country(uint id , string name , ushort internalCode , string isoCode = "") : 
            base(id , name , internalCode , isoCode)
        { }

        public Country()
        { }
        

        public bool Disabled { get; set; }

        public override string ToString() => $"(ID:{ID}, Pays:{Name}, Code Pays:{InternalCode}, Code Iso:{IsoCode})";

        //protected:
        protected override void DoRead(IReader reader)
        {
            base.DoRead(reader);
            Disabled = reader.ReadBoolean();
        }

        protected override void DoWrite(IWriter writer)
        {
            base.DoWrite(writer);
            writer.Write(Disabled);
        }
    }
}
