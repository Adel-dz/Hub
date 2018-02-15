using DGD.HubCore.DLG;
using System;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IClientStatusRow: IDataRow
    {
        uint ClientID { get; }
        ClientStatus_t Status { get; }
    }

    public class ClientStatusRow: DataRow, IClientStatusRow
    {
        public ClientStatusRow()
        { }

        public ClientStatusRow(uint idClient, ClientStatus_t status):
            base(idClient)
        {
            Status = status;
        }

        public uint ClientID => ID;
        public ClientStatus_t Status { get; private set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            byte st = reader.ReadByte();

            if (st == (byte)ClientStatus_t.Unknown ||
                    !Enum.IsDefined(typeof(ClientStatus_t) , st))
                throw new CorruptedStreamException();

            Status = (ClientStatus_t)st;
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(Status != ClientStatus_t.Unknown);

            writer.Write((byte)Status);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ClientStatuses.GetStatusName(Status)
            };
        }
    }
}
