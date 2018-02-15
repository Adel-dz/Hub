using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Currencies
{
    sealed class Currency: CurrencyRow, ITaggedRow
    {
        public Currency()
        { }

        public Currency(uint id , string name , uint countryID = 0 , string descr = "") :
            base(id , name , countryID , descr)
        { }


        public bool Disabled { get; set; }
        public override string ToString() => $"(ID:{ID}, Nom:{Name}, ID Pays:{CountryID}, Description:{Description})";


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
