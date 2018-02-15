using System.Diagnostics;
using System.Text;

namespace easyLib.DB
{
    public interface ITableWriter: IWriter
    {
        long Position { get; set; }
        long Length { get; }
    }
    

    public sealed class TableWriter: RawDataWriter, ITableWriter
    {
        public TableWriter(System.IO.MemoryStream output) :
            base(output , new UTF8Encoding(true , true))
        { }

        public TableWriter(System.IO.FileStream output) :
            base(output , new UTF8Encoding(true , true))
        { }



        public long Length
        {
            get { return Writer.BaseStream.Length; }            
        }

        public long Position
        {
            get { return Writer.BaseStream.Position; }

            set
            {
                Debug.Assert(value <= Length);
                Writer.BaseStream.Position = value;
            }
        }    
    }
}
