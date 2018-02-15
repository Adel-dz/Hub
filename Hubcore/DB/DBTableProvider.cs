namespace DGD.HubCore.DB
{
    public interface IDBTableProvider : IDBProvider
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
    }
}
