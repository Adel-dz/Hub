using DGD.HubCore.DB;
using System;



namespace DGD.HubGovernor.Clients
{
    sealed class HubClientEnvironment : ClientEnvironmentRow
    {
        public HubClientEnvironment()
        { }

        public HubClientEnvironment(uint id, uint idClient):
            base(id, idClient)
        { }

        public HubClientEnvironment(uint id, uint idClient, DateTime tmCreation):
            base(id, idClient, tmCreation)
        { }
    }
}
