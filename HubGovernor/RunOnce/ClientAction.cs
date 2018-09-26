using DGD.HubCore.RunOnce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.RunOnce
{
    class ClientAction
    {
        protected ClientAction()
        { }
        
        public ClientAction(uint idClient , RunOnceAction_t action, DateTime tmCreation)
        {
            ClientID = idClient;
            Action = action;
            CreationTime = tmCreation;
        }


        public uint ClientID { get; protected set; }
        public RunOnceAction_t Action { get; protected set; }
        public DateTime CreationTime { get; protected set; }
    }

}
