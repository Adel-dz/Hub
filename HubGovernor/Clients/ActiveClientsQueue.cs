using DGD.HubGovernor.Log;
using System;
using System.Collections.Generic;

namespace DGD.HubGovernor.Clients
{
    sealed class ActiveClientsQueue
    {
        public interface IClientData
        {
            DateTime ConnectionTime { get; }
            int TimeToLive { get; set; }
            uint LastClientMessageID { get; set; }
            uint LastSrvMessageID { get; set; }
            IEnumerable<IEventLog> ClientLogs { get; }
        }


        class ClientData: IClientData
        {          
            public ClientData(DateTime cxnTime, int TTL = 20)
            {
                LogList = new List<IEventLog>();
                ConnectionTime = cxnTime;
                TimeToLive = TTL;
            }


            public List<IEventLog> LogList { get; }
            public DateTime ConnectionTime { get; }
            public int TimeToLive { get; set; }
            public uint LastClientMessageID { get; set; }
            public uint LastSrvMessageID { get; set; }
            public IEnumerable<IEventLog> ClientLogs => LogList;
        }



        readonly Dictionary<uint , ClientData> m_clients = new Dictionary<uint, ClientData>(); //the lock


        public IClientData[] Clients
        {
            get
            {
                lock(m_clients)
                {
                    var cls = new ClientData[m_clients.Count];
                    m_clients.Values.CopyTo(cls , 0);

                    return cls;
                }
            }
        }


        public IClientData AddClient(uint clID, DateTime cxnTime)
        {
            Dbg.Assert(ContainsClient(clID) == false);

            ClientData clData = null;

            lock (m_clients)
                if (!m_clients.ContainsKey(clID))
                {
                    clData = new ClientData(cxnTime);
                    m_clients[clID] = clData;                    
                }

            return clData;
        }

        public IClientData AddClient(uint clID) => AddClient(clID , DateTime.Now);

        public void RemoveClient(uint clID)
        {
            lock (m_clients)
                m_clients.Remove(clID);
        }

        public bool ContainsClient(uint clID)
        {
            lock (m_clients)
                return m_clients.ContainsKey(clID);
        }
    }
}
