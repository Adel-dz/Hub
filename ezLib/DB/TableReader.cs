using System.Text;
using static System.Diagnostics.Debug;



namespace easyLib.DB
{
    public interface ITableReader: IReader
    {
        long Position { get; set; }
        long Length { get; }
    }


    public sealed class TableReader: RawDataReader, ITableReader
    {
        public TableReader(System.IO.FileStream input):
            base(input, new UTF8Encoding(true,true))
        { }

        public TableReader(System.IO.MemoryStream input):
            base(input, new UTF8Encoding(true , true))
        { }



        public long Length
        {
            get { return Reader.BaseStream.Length; }
        }

        public long Position
        {
            get { return Reader.BaseStream.Position; }

            set
            {
                Assert(value <= Length);

                Reader.BaseStream.Position = value;
            }
        }
    }
}
