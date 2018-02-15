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
        public uint HubAppGeneration { get; set; }
        public string ServerURL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DialogTimerInterval { get; set; } = 5000;
        public int DialogInitializationInterval { get; set; } = 10000;
        
        public void Save(BinaryWriter writer)
        {
            Assert(writer != null);

            writer.Write((int)InputTransform);
            writer.Write((int)ImportTransform);
            writer.Write(PriceDecimalPlaces);
            writer.Write(DataGeneration);
            writer.Write(HubAppGeneration);
            writer.Write(ServerURL ?? "");  //ftp://douane.gov.dz
            writer.Write(UserName ?? "");
            writer.Write(Password ?? "");
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
            HubAppGeneration = reader.ReadUInt32();
            ServerURL = reader.ReadString();
            UserName = reader.ReadString();
            Password = reader.ReadString();
        }

        public void Reset()
        {
            ImportTransform = InputTransform = StringTransform_t.None;
            PriceDecimalPlaces = 4;
            DataGeneration = 0;
        }
    }
}
