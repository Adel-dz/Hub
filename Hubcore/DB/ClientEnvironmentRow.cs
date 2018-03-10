using System;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{
    public interface IClientEnvironmentRow : IDataRow, IClientEnvironment
    {
        uint ClientID { get; }
        DateTime CreationTime { get; }        
    }


    public class ClientEnvironmentRow: DataRow, IClientEnvironmentRow
    {        
        public ClientEnvironmentRow(uint id , uint clientID , DateTime tmCreation):
            base(id)
        {
            CreationTime = tmCreation;
            ClientID = clientID;

            HubVersion = MachineName = OSVersion = UserName = "";
        }

        public ClientEnvironmentRow(uint id , uint clientID):
            this(id , clientID , DateTime.Now)
        { }

        public ClientEnvironmentRow()
        {
            HubVersion = MachineName = OSVersion = UserName = "";
        }


        public uint ClientID { get; private set; }
        public DateTime CreationTime { get; private set; }
        public AppArchitecture_t HubArchitecture { get; set; }
        public string HubVersion { get; set; }
        public bool Is64BitOperatingSystem { get; set; }
        public string MachineName { get; set; }
        public string OSVersion { get; set; }
        public string UserName { get; set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            uint clID = reader.ReadUInt();
            bool is64 = reader.ReadBoolean();
            byte arch = reader.ReadByte();

            if (!Enum.IsDefined(typeof(AppArchitecture_t) , arch))
                throw new CorruptedStreamException();

            ClientID = clID;
            HubArchitecture = (AppArchitecture_t)arch;
            Is64BitOperatingSystem = is64;
            CreationTime = reader.ReadTime();
            HubVersion = reader.ReadString();
            MachineName = reader.ReadString();
            OSVersion = reader.ReadString();
            UserName = reader.ReadString();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(ClientID > 0);
            writer.Write(ClientID);

            writer.Write(Is64BitOperatingSystem);
            writer.Write((byte)HubArchitecture);

            writer.Write(CreationTime);
            writer.Write(HubVersion);
            writer.Write(MachineName);
            writer.Write(OSVersion);
            writer.Write(UserName);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ClientID.ToString("X"),
                UserName,
                MachineName,
                OSVersion,
                Is64BitOperatingSystem? "64 Bits" : "32 Bits",
                HubVersion,
                AppArchitectures.GetArchitectureName(HubArchitecture)
            };
        }
    }
}
