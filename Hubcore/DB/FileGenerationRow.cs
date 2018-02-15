using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IFileGenerationRow : IDataRow
    {
        uint Generation { get; set; }
    }



    public abstract class FileGenerationRow: DataRow, IFileGenerationRow
    {
        protected FileGenerationRow(uint idFile, uint gen):
            base(idFile)
        {
            Generation = gen;
        }
       

        public uint Generation { get; set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            Generation = reader.ReadUInt();

            if (Generation == 0)
                throw new CorruptedStreamException();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(Generation != 0);
            writer.Write(Generation);
        }
    }
}
