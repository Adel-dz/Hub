using DGD.HubGovernor.Log;
using easyLib;
using System;
using System.Collections.Generic;
using System.Threading;

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

            void AddLog(IEventLog log);
        }


        class ClientData: IClientData
        {          
            public ClientData(DateTime cxnTime)
            {
                LogList = new List<IEventLog>();
                ConnectionTime = cxnTime;
                TimeToLive = InitTimeToLive;
            }


            public List<IEventLog> LogList { get; }
            public DateTime ConnectionTime { get; }
            public int TimeToLive { get; set; }
            public uint LastClientMessageID { get; set; }
            public uint LastSrvMessageID { get; set; }
            public IEnumerable<IEventLog> ClientLogs => LogList;

            public void AddLog(IEventLog log)
            {
                LogList.Add(log);
            }
        }



        readonly Dictionary<uint , ClientData> m_clients = new Dictionary<uint, ClientData>(); //the lock


        public static int InitTimeToLive { get; set; } = 10;

        public IClientData[] ClientsData
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

        public uint[] ClientsID
        {
            get
            {
                lock(m_clients)
                {
                    var ids = new uint[m_clients.Count];
                    m_clients.Keys.CopyTo(ids , 0);

                    return ids;
                }
            }
        }

        public IClientData Add(uint clID, DateTime cxnTime)
        {
            Dbg.Assert(Contains(clID) == false);

            ClientData clData = null;

            lock (m_clients)
                if (!m_clients.ContainsKey(clID))
                {
                    clData = new ClientData(cxnTime);
                    m_clients[clID] = clData;                    
                }

            return clData;
        }

        public IClientData Add(uint clID) => Add(clID , DateTime.Now);

        public void Remove(uint clID)
        {
            lock (m_clients)
                m_clients.Remove(clID);
        }

        public bool Contains(uint clID)
        {
            lock (m_clients)
                return m_clients.ContainsKey(clID);
        }

        public IClientData Get(uint clID)
        {
            ClientData clData;

            lock (m_clients)
                m_clients.TryGetValue(clID , out clData);

            return clData;
        }

        public IDisposable Lock()
        {
            Monitor.Enter(m_clients);

            return new AutoReleaser(() => Monitor.Exit(m_clients));
        }
    }
}
