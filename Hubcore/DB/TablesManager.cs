using System;

namespace DGD.HubCore.DB
{
    public interface ITablesManager
    {
        event Action<uint> BeginTableProcessing;
        event Action<uint> EndTableProcessing;

        ITablesCollection Tables { get; }

        IDBKeyIndexer GetKeyIndexer(uint idTable);
        IDBProvider GetDataProvider(uint idTable);

    }
}
