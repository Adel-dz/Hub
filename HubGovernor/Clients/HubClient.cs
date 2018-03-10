using DGD.HubCore.DB;

namespace DGD.HubGovernor.Clients
{
    sealed class HubClient: ClientRow
    {
        public HubClient()
        { }

        public HubClient(uint id, uint idProfile):
            base(id, idProfile)
        { }


        public override string ToString() =>
            $"ID:{ID}, Profile:{ProfileID}, Date de création:{CreationTime},  " +
            $"Contact:{ContactName}, Tel:{ContactPhone}, eMail:{ContaclEMail}";
    }
}
