using System;
using DGD.HubCore.DB;
using DGD.HubGovernor.DB;
using easyLib;

namespace DGD.HubGovernor.Suppliers
{
    sealed class DataSupplier: DataSupplierRow, ITaggedRow
    {
        public DataSupplier()
        { }

        public DataSupplier(uint id, string name):
            base(id, name)
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
