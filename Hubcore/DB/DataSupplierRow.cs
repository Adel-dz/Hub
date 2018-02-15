using easyLib;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.DB
{
    public interface IDataSupplierRow : IDataRow
    {
        string Name { get; }
    }
    


    public class DataSupplierRow: DataRow, IDataSupplierRow
    {

        private DataSupplierRow(string name, uint id):
            base(id)
        {
            Name = name;
        }

        public DataSupplierRow(uint id , string name) :
            this(name , id)
        { }

        public DataSupplierRow():
            this("",0)
        { }


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
