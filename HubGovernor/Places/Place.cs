using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Places
{
    sealed class Place: PlaceRow, ITaggedRow
    {
        public Place()
        { }

        public Place(uint id , string name , uint ctryID = 0) :
            base(id , name , ctryID)
        { }


        public bool Disabled { get; set; }

        public override string ToString() => $"(ID:{ID}, Nom:{Name}, ID Pays:{CountryID})";


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
