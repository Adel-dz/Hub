using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IUnitRow: IDataRow
    {
        string Name { get; }
        string Description { get; }
    }


    public class UnitRow: DataRow, IUnitRow
    {
        string m_descr;

        private UnitRow(string name , string descr , uint id) :
            base(id)
        {
            Description = descr;
            Name = name;
        }

        public UnitRow(uint id , string name , string descr = "") :
            this(name , descr , id)
        { }

        public UnitRow():
            this("","",0)
        { }

        
        public string Name { get; private set; }

        public string Description
        {
            get { return m_descr; }
            protected set { m_descr = value ?? ""; }
        }



        //protected:
        protected override void DoRead(IReader reader)
        {
            Name = reader.ReadString();

            if (string.IsNullOrWhiteSpace(Name))
                throw new CorruptedStreamException();

            Description = reader.ReadString();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
            writer.Write(Description);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name,
                Description
            };
        }
    }
}
