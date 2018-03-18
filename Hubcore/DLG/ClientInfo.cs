using easyLib;
using System;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public sealed class ClientInfo : IStorable
    {
        ClientInfo()
        { }

        public ClientInfo(uint clientID = 0, uint profileID = 0)
        {
            ClientID = clientID;
            ProfileID = profileID;
        }

        public uint ClientID { get; private set; }
        public uint ProfileID { get; private set; }
        //public string MachineName { get; set; }
        public string ContactName { get; set; }
        public string ContaclEMail { get; set; }
        public string ContactPhone { get; set; }

        public void Read(IReader reader)
        {
            ClientID = reader.ReadUInt();
            ProfileID = reader.ReadUInt();

            if (ClientID == 0 || ProfileID == 0)
                throw new CorruptedStreamException();

            //MachineName = reader.ReadString();
            ContactName = reader.ReadString();
            ContactPhone = reader.ReadString(); 
            ContaclEMail = reader.ReadString(); 
        }

        public void Write(IWriter writer)
        {
            Assert(ClientID > 0);
            Assert(ProfileID > 0);

            writer.Write(ClientID);
            writer.Write(ProfileID);
            //writer.Write(MachineName);
            writer.Write(ContactName);
            writer.Write(ContactPhone);
            writer.Write(ContaclEMail);
        }

        public static ClientInfo CreateClient(uint profileID) => 
            new ClientInfo((uint)DateTime.Now.Ticks , profileID);
        
        public static ClientInfo LoadClientInfo(IReader reader)
        {
            var clInfo = new ClientInfo();
            clInfo.Read(reader);

            return clInfo;
        }
    }
}
