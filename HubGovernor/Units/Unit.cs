using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Units
{
    sealed class Unit: UnitRow, ITaggedRow
    {
        public Unit(uint id, string name, string decsr = ""):
            base(id, name,decsr)
        { }

        public Unit()
        { }


        public bool Disabled { get; set; }


        public override string ToString() => $"(ID:{ID}, Nom:{Name}, Description:{Description})";

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
