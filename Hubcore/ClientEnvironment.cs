using easyLib;
using System;
using static System.Diagnostics.Debug;


namespace DGD.HubCore
{
    public interface IClientEnvironment
    {
        string UserName { get; }
        string MachineName { get; }
        string OSVersion { get; }
        bool Is64BitOperatingSystem { get; }
        string HubVersion { get; }
        AppArchitecture_t HubArchitecture { get; }
    }


    public sealed class ClientEnvironment : IClientEnvironment, IStorable
    {
        public string UserName { get; set; } = "";
        public string MachineName { get; set; } = "";
        public string OSVersion { get; set; } = "";
        public string HubVersion { get; set; } = "";
        public bool Is64BitOperatingSystem { get; set; }        
        public AppArchitecture_t HubArchitecture { get; set; }

        public void Read(IReader reader)
        {
            Assert(reader != null);

            byte arch = reader.ReadByte();

            if (!Enum.IsDefined(typeof(AppArchitecture_t) , arch))
                throw new CorruptedStreamException();
                        
            HubArchitecture = (AppArchitecture_t)arch;
            Is64BitOperatingSystem = reader.ReadBoolean();
            UserName = reader.ReadString();
            MachineName = reader.ReadString();
            OSVersion = reader.ReadString();
            HubVersion = reader.ReadString();
        }

        public void Write(IWriter writer)
        {
            Assert(writer != null);

            writer.Write((byte)HubArchitecture);
            writer.Write(Is64BitOperatingSystem);
            writer.Write(UserName ?? "");
            writer.Write(MachineName ?? "");
            writer.Write(OSVersion ?? "");
            writer.Write(HubVersion ?? "");
        }

        public static ClientEnvironment Load(IReader reader)
        {
            Assert(reader != null);

            var clEnv = new ClientEnvironment();
            clEnv.Read(reader);

            return clEnv;
        }
    }
}
