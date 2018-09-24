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



        public RunOnceManager(string filePath)
        {
            m_dataFilePath = filePath;
            m_entries = new List<Entry>();
            m_sentMessages = new List<PendingMessage>();

            LoadData();
        }


        public IEnumerable<Entry> PendingActions { get; }

        public void PostActions(IEnumerable<Entry> entries)
        {
            Assert(entries != null);

            foreach (Entry e in entries)
                AddEntry(e);

            SaveData();
        }



        //private:
        byte[] FileSignature => Encoding.UTF8.GetBytes("HUBRO1");

        void LoadData()
        {
            //using (FileStream fs = File.OpenRead(m_dataFilePath))
            //{
            //    var reader = new RawDataReader(fs , Encoding.UTF8);
            //    byte[] sign = FileSignature;

            //    foreach (byte b in sign)
            //        if (b != reader.ReadByte())
            //            throw new CorruptedFileException(m_dataFilePath);

            //    int count = reader.ReadInt();

            //    for (int i = 0; i < count; ++i)

            //}
        }

        void SaveData()
        { }

        IEnumerable<IRunOnceAction> GetPenddingActions(uint clID)
        {
            throw null;
        }

        void PostMessage(Entry e)
        { }

        void AddEntry(Entry e)
        {
            bool found = false;

            for (int i = 0; i < m_entries.Count; ++i)
            {
                Entry entry = m_entries[i];

                if (entry.ClientID == e.ClientID && entry.Action.ActionCode == e.Action.ActionCode)
                {
                    m_entries[i] = e;
                    found = true;
                    break;
                }
            }


            if (!found)
                m_entries.Add(e);


            if (AppContext.ClientsManager.IsClientRunning(e.ClientID))
                PostMessage(e);
        }
    }
}
