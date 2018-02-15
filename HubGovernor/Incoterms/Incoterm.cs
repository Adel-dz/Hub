using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Incoterms
{
    sealed class Incoterm: IncotermRow, ITaggedRow
    {
        public Incoterm(uint id, string name):
            base(id, name)
        { }

        public Incoterm()
        { }


        public bool Disabled { get; set; }
        public override string ToString() => $"(ID:{ID}, Nom:{Name})";


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
