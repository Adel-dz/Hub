using DGD.HubCore.Net;
using System;
using System.IO;
using static System.Diagnostics.Debug;



namespace DGD.HubGovernor.Opts
{
    enum StringTransform_t
    {
        None,
        UpperCase,
        LowerCase,
    }


    sealed class AppSettings : ICredential
    {
        public StringTransform_t InputTransform { get; set; }
        public StringTransform_t ImportTransform { get; set; }
        public byte PriceDecimalPlaces { get; set; } = 4;
        public uint DataGeneration { get; set; }
        public uint UpdateKey { get; set; }
        public uint HubAppGeneration { get; set; }
        public string ServerURL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableProxy { get; set; }
        public string ProxyAddress { get; set; } = "";
        public ushort ProxyPort { get; set; } = 8080;
        public int DialogTimerInterval { get; set; } = 30 * 1000;
        public int DialogInitializationInterval { get; set; } = 10000;
        
        public void Save(BinaryWriter writer)
        {
            Assert(writer != null);

            writer.Write((int)InputTransform);
            writer.Write((int)ImportTransform);
            writer.Write(PriceDecimalPlaces);
            writer.Write(DataGeneration);
            writer.Write(UpdateKey);
            writer.Write(HubAppGeneration);
            writer.Write(ServerURL ?? "");  //ftp://douane.gov.dz
            writer.Write(UserName ?? "");
            writer.Write(Password ?? "");
            writer.Write(EnableProxy);
            writer.Write(ProxyAddress ?? "");
            writer.Write(ProxyPort);
        }

        public void Load(BinaryReader reader)
        {
            int x = reader.ReadInt32();
            if (Enum.IsDefined(typeof(StringTransform_t) , x))
                InputTransform = (StringTransform_t)x;

            x = reader.ReadInt32();
            if (Enum.IsDefined(typeof(StringTransform_t) , x))
                ImportTransform = (StringTransform_t)x;

            PriceDecimalPlaces = reader.ReadByte();
            DataGeneration = reader.ReadUInt32();
            UpdateKey = reader.ReadUInt32();
            HubAppGeneration = reader.ReadUInt32();
            ServerURL = reader.ReadString();
            UserName = reader.ReadString();
            Password = reader.ReadString();
            EnableProxy = reader.ReadBoolean();
            ProxyAddress = reader.ReadString();
            ProxyPort = reader.ReadUInt16();
        }

        public void Reset()
        {
            ImportTransform = InputTransform = StringTransform_t.None;
            PriceDecimalPlaces = 4;
            DataGeneration = 0;
        }
    }
}
