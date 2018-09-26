using DGD.HubCore.RunOnce;
using easyLib;
using System;

namespace DGD.HubGovernor.RunOnce
{
    partial class RunOnceManager
    {
        public sealed class Entry: IStorable
        {
            Entry()
            { }
            
            public Entry(uint idClient, RunOnceAction_t action)
            {
                ClientID = idClient;
                Action = action;
                CreationTime = DateTime.Now;
            }


            public uint ClientID { get; private set; }            
            public RunOnceAction_t Action { get; private set; }
            public DateTime CreationTime { get; set; }

            public void Read(IReader reader)
            {
                ClientID = reader.ReadUInt();
                byte acCode = reader.ReadByte();
                CreationTime = reader.ReadTime();

                if (ClientID == 0 || !Enum.IsDefined(typeof(RunOnceAction_t) , acCode))
                    throw new CorruptedStreamException();

                Action = (RunOnceAction_t)acCode;
            }

            public void Write(IWriter writer)
            {
                writer.Write(ClientID);
                writer.Write((byte)Action);
                writer.Write(CreationTime);
            }

            public static Entry LoadEntry(IReader reader)
            {
                var entry = new Entry();
                entry.Read(reader);

                return entry;
            }
        }
    }
}
