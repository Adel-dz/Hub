using DGD.HubCore.DB;
using DGD.HubCore.DLG;

namespace DGD.HubGovernor.Clients
{
    sealed class ClientStatus : ClientStatusRow
    {
        public ClientStatus(uint idClient, ClientStatus_t status):
            base(idClient, status)
        { }

        public ClientStatus()
        { }

        public static int Size => sizeof(uint) + sizeof(byte);

        public override string ToString() =>
            $"ID:{ID}, Status:{ClientStatuses.GetStatusName(Status)}";
    }
}
