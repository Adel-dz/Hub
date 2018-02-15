using System;

namespace DGD.HubCore.DB
{
    public interface IDBTableInfo
    {
        string FilePath { get; }
        uint Version { get; }
        DateTime CreationTime { get; }
        DateTime LasttWriteTime { get; }
        int DatumSize { get; }
    }
}
