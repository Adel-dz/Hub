using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IIncotermRow : IDataRow
    {
        string Name { get; }
    }



    public class IncotermRow: DataRow, IIncotermRow
    {
        public IncotermRow(uint id, string name):
            base(id)
        {
            Name = name;
        }

        public IncotermRow()
        {
            Name = "";
        }


        public string Name { get; private set; }        


        //protected:
        protected override void DoRead(IReader reader)
        {
            Name = reader.ReadString();

            if (string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name
            };
        }
    }
}
