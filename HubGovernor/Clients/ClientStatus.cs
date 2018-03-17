using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using easyLib;
using System;

namespace DGD.HubGovernor.Clients
{
    public interface IClientStatus: IDataRow
    {
        uint ClientID { get; }
        ClientStatus_t Status { get; }
        DateTime LastSeen { get; }
        uint SentMsgCount { get; }
        uint ReceivedMsgCount { get; }
    }


    sealed class ClientStatus: DataRow, IClientStatus
    {
        public ClientStatus()
        { }

        public ClientStatus(uint idClient , ClientStatus_t status) :
            this(idClient , status , DateTime.Now)
        { }

        public ClientStatus(uint idClient , ClientStatus_t status , DateTime seen) :
            base(idClient)
        {
            Status = status;
            LastSeen = seen;
        }


        public uint ClientID => ID;
        public ClientStatus_t Status { get; set; }
        public DateTime LastSeen { get; set; }
        public uint SentMsgCount { get; set; }
        public uint ReceivedMsgCount { get; set; }

        public static int Size => (sizeof(uint) * 3) + sizeof(byte) + sizeof(long);
        

        //protected:
        protected override void DoRead(IReader reader)
        {
            byte st = reader.ReadByte();

            if (st == (byte)ClientStatus_t.Unknown ||
                    !Enum.IsDefined(typeof(ClientStatus_t) , st))
                throw new CorruptedStreamException();

            Status = (ClientStatus_t)st;
            LastSeen = reader.ReadTime();
            SentMsgCount = reader.ReadUInt();
            ReceivedMsgCount = reader.ReadUInt();
        }

        protected override void DoWrite(IWriter writer)
        {
            Dbg.Assert(Status != ClientStatus_t.Unknown);

            writer.Write((byte)Status);
            writer.Write(LastSeen);
            writer.Write(SentMsgCount);
            writer.Write(ReceivedMsgCount);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString("X"),
                ClientStatuses.GetStatusName(Status),
                LastSeen.ToString(),
                SentMsgCount.ToString(),
                ReceivedMsgCount.ToString()
            };
        }
    }
}
