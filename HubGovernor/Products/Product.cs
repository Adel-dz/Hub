using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Products
{
    sealed class Product: ProductRow, ITaggedRow
    {
        public Product(uint id, string name, SubHeading subHeading):
            base(id, name, subHeading)
        { }

        public Product()
        { }


        public bool Disabled { get; set; }

        public override string ToString() => $"(ID:{ID}, SPT10:{SubHeading}, Nom:{Name})";

        //protected:
        protected override void DoWrite(IWriter writer)
        {            
            base.DoWrite(writer);
            writer.Write(Disabled);
        }

        protected override void DoRead(IReader reader)
        {
            base.DoRead(reader);
            Disabled = reader.ReadBoolean();
        }
    }
}
