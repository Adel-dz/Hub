using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using System;

namespace DGD.HubGovernor.Clients
{
    sealed class ClientStatus : ClientStatusRow
    {
        public ClientStatus(uint idClient, ClientStatus_t status, DateTime lastSeen):
            base(idClient, status, lastSeen)
        { }

        public ClientStatus(uint idClient , ClientStatus_t status) : 
            this(idClient , status , DateTime.Now)
        { }


        public ClientStatus()
        { }

        public static int Size => sizeof(uint) + sizeof(byte) + sizeof(long);

        public override string ToString() =>
            $"(ID:{ID}, Status:{ClientStatuses.GetStatusName(Status)}, LastSeen:{LastSeen}";
    }
}
