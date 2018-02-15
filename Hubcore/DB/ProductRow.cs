using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IProductRow: IDataRow
    {
        string Name { get; }
        SubHeading SubHeading { get; }
    }


    public class ProductRow: DataRow, IProductRow
    {
        private ProductRow(SubHeading subHeading, string name, uint id):
            base(id)
        {
            Name = name;
            SubHeading = subHeading;
        }

        public ProductRow():
            this(SubHeading.MaxSubHeading,"",0)
        { }

        public ProductRow(uint id , string name , SubHeading subHeading) :
            this(subHeading , name , id)
        { }


        public string Name { get; private set; }
        public SubHeading SubHeading { get; private set; }        


        //protected:
        protected override void DoRead(IReader reader)
        {
            ulong shValue = reader.ReadULong();
            Name = reader.ReadString();

            if (shValue < SubHeading.MinValue || shValue > SubHeading.MaxValue || string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();

            SubHeading = new SubHeading(shValue);
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(SubHeading != null);
            writer.Write(SubHeading.Value);

            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                SubHeading.ToString(),
                Name
            };
        }
    }
}
