using static System.Diagnostics.Debug;


namespace easyLib.DB
{
    public interface IDataSource
    {
        string Name { get; }
        uint ID { get; }
        bool IsConnected { get; }
        int RowCount { get; }
        IDataColumn[] Columns { get; }
        IDatumProvider DataProvider { get; }
    }



    public abstract class DataSource: IDataSource
    {
        protected DataSource(string name, uint id)
        {
            ID = id;
            Name = name;
        }


        public uint ID { get; }
        public IDatumProvider DataProvider => GetDataProvider();
        public bool IsConnected => IsOpen;
        public string Name { get; }

        public IDataColumn[] Columns
        {
            get
            {
                Assert(IsConnected);

                return GetColumns();
            }
        }        

        public int RowCount
        {
            get
            {
                Assert(IsConnected);

                return GetRowCount();
            }
        }

        //protected:
        protected abstract bool IsOpen { get; }
        protected abstract IDataColumn[] GetColumns();
        protected abstract IDatumProvider GetDataProvider();
        protected abstract int GetRowCount();
    }
}
