using DGD.HubCore.DB;
using easyLib;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.TR
{
    interface ILabelMapping : IDataRow
    {
        uint TRLabelID { get; }
        uint SharedTextID { get; }
    }


    sealed class LabelMapping: DataRow, ILabelMapping
    {
        public LabelMapping()
        { }

        public LabelMapping(uint idTRLabel, uint idSharedText):
            base(idTRLabel)
        {
            SharedTextID = idSharedText;
        }


        public uint SharedTextID { get; private set; }
        public uint  TRLabelID => ID;

        public override string ToString() => $"(TRLabelID{TRLabelID}, SharedTextID:{SharedTextID})";

        public static int Size => sizeof(uint) << 1;

        //protected:
        protected override void DoRead(IReader reader)
        {
            uint id = reader.ReadUInt();

            if (id == 0)
                throw new CorruptedStreamException();

            SharedTextID = id;
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(SharedTextID > 0);

            writer.Write(SharedTextID);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                SharedTextID.ToString()
            };
        }
    }
}
