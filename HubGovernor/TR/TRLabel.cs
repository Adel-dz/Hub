using DGD.HubCore.DB;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR
{
    interface ITRLabel: IDataRow
    {
        uint ProductNumber { get; }
        string Label { get; }
    }



    sealed class TRLabel: DataRow, ITRLabel
    {
        public TRLabel()
        { }

        public TRLabel(uint id, uint prodNber, string label):
            base(id)
        {
            ProductNumber = prodNber;
            Label = label ?? "";
        }


        public string Label { get; private set; }
        public uint ProductNumber { get; private set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            uint prodNber = reader.ReadUInt();
            string txt = reader.ReadString();

            if (prodNber == 0 || string.IsNullOrWhiteSpace(txt))
                throw new CorruptedStreamException();

            Label = txt;
            ProductNumber = prodNber;            
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(ProductNumber > 0);
            Assert(!string.IsNullOrWhiteSpace(Label));

            writer.Write(ProductNumber);
            writer.Write(Label);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ProductNumber.ToString(),
                Label
            };
        }
    }
}
