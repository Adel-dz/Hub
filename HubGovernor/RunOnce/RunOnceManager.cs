using DGD.HubCore.RunOnce;
using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.RunOnce
{
    sealed partial class RunOnceManager
    {
        struct PendingMessage
        {
            public PendingMessage(uint msgID , Entry e)
            {
                MessageID = msgID;
                Entry = e;
            }


            public uint MessageID { get; }
            public Entry Entry { get; }
        }


        readonly string m_dataFilePath;
        readonly List<Entry> m_entries;
        readonly List<PendingMessage> m_sentMessages;

        public event Action<ClientAction> ActionAdded;
        public event Action<uint , RunOnceAction_t> ActionRemoved;


        public RunOnceManager(string filePath)
        {
            m_dataFilePath = filePath;
            m_entries = new List<Entry>();
            m_sentMessages = new List<PendingMessage>();

            LoadData();
        }


        public IEnumerable<ClientAction> Actions
        {
            get
            {
                lock (m_entries)
                    return m_entries.ToList();
            }
        }

        public void PostActions(IEnumerable<ClientAction> actions)
        {
            Assert(actions != null);

            foreach (ClientAction e in actions)
            {
                lock (m_entries)
                    AddEntry(e);

                if(AppContext.ClientsManager.IsClientRunning(e.ClientID))
                {
                    lock(m_sentMessages)
                    {

                    }
                }
            }

            SaveData();
        }



        //private:
        byte[] FileSignature => Encoding.UTF8.GetBytes("HUBRO1");

        void LoadData()
        {
            using (FileStream fs = File.OpenRead(m_dataFilePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = FileSignature;

                foreach (byte b in sign)
                    if (b != reader.ReadByte())
                        throw new CorruptedFileException(m_dataFilePath);


                int count = reader.ReadInt();

                for (int i = 0; i < count; ++i)
                    m_entries.Add(Entry.LoadEntry(reader));
            }
        }

        void SaveData()
        {
            using (FileStream fs = File.Create(m_dataFilePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(FileSignature);
                writer.Write(m_entries.Count);

                foreach (Entry e in m_entries)
                    e.Write(writer);
            }
        }

        void AddEntry(ClientAction e)
        {
            bool found = false;

            for (int i = 0; i < m_entries.Count; ++i)
            {
                Entry entry = m_entries[i];

                if (entry.ClientID == e.ClientID && entry.Action == e.Action)
                {
                    m_entries[i].ChangeCreationTime(e.CreationTime);
                    found = true;
                    break;
                }
            }


            if (found)
                ActionRemoved?.Invoke(e.ClientID , e.Action);
            else
                m_entries.Add(new Entry(e.ClientID , e.Action , e.CreationTime));

            ActionAdded.Invoke(e);
        }
    }
}
