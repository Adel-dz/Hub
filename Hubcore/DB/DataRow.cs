using easyLib;
using easyLib.DB;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{
    public interface IDataRow : IDatum, IStorable
    {
        uint ID { get; }        
    }
    


    public abstract class DataRow: IDataRow
    {
        protected DataRow(uint id = 0)
        {
            ID = id;
        }


        public string[] Content => GetContent();
        public uint ID { get; private set; }
        

        public void Read(IReader reader)
        {
            Assert(reader != null);

            uint id = reader.ReadUInt();

            if(id == 0)
            {
                var ex = new CorruptedStreamException();
                System.Diagnostics.Debug.WriteLine(ex.Message);

                throw ex;
            }

            DoRead(reader);
            ID = id;
        }        

        public void Write(IWriter writer)
        {
            Assert(writer != null);
            Assert(ID > 0);

            writer.Write(ID);
            DoWrite(writer);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        //protected:
        protected abstract string[] GetContent();
        protected abstract void DoRead(IReader reader);
        protected abstract void DoWrite(IWriter writer);
    }
}
