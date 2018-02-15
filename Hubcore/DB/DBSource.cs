namespace DGD.HubCore.DB
{
    public interface IDBSource
    {
        uint ID { get; }
        string Name { get; }
        int RowCount { get; } 
        IDBProvider DataProvider { get; }        
    }
}
