using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public class ClientDialog: IStorable
    {
        ClientDialog()
        { }

        public ClientDialog(uint idClient, ClientStatus_t status, IEnumerable<Message> messages)
        {
            ClientID = idClient;
            ClientStatus = status;
            Messages = messages;
        }

        public uint ClientID { get; private set; }
        public ClientStatus_t ClientStatus { get; set; }
        public IEnumerable<Message> Messages { get; private set; }

        public void Read(IReader reader)
        {
            ClientID = reader.ReadUInt();
            byte status = reader.ReadByte();

            if (ClientID == 0 || status == 0 || !Enum.IsDefined(typeof(ClientStatus_t) , status))
                throw new CorruptedStreamException();

            ClientStatus = (ClientStatus_t)status;
            int msgCount = reader.ReadInt();

            var lst = new List<Message>(msgCount);

            for (int i = 0; i < msgCount; ++i)
                lst.Add(Message.LoadMessage(reader));

            Messages = lst;
        }

        public void Write(IWriter writer)
        {
            Assert(ClientID > 0);
            Assert(ClientStatus != ClientStatus_t.Unknown);
            Assert(Messages != null);

            writer.Write(ClientID);
            writer.Write((byte)ClientStatus);
            writer.Write(Messages.Count());

            foreach (Message msg in Messages)
                msg.Write(writer);
        }

        public static ClientDialog LoadClientDialog(IReader reader)
        {
            var cd = new ClientDialog();
            cd.Read(reader);

            return cd;
        }
    }
}
