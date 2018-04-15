using DGD.HubCore.DB;
using System;
using easyLib;
using DGD.HubCore;

namespace DGD.HubGovernor.Updating
{

    interface IAppUpdate: IDataRow
    {
        Version Version { get; }
        AppArchitecture_t AppArchitecture { get; }
        DateTime CreationTime { get; }
        DateTime DeployTime { get; set; }
        bool IsDeployed { get; }
    }

    sealed class AppUpdate: DataRow, IAppUpdate
    {
        public static readonly DateTime NOT_YET = DateTime.MinValue;
        public static readonly DateTime NEVER = DateTime.MaxValue;

        public AppUpdate(uint id , Version ver , AppArchitecture_t appArch , DateTime tmCreation) :
            base(id)
        {
            Version = ver;
            AppArchitecture = appArch;
            CreationTime = tmCreation;
            DeployTime = NOT_YET;
        }

        public AppUpdate(uint id , Version ver , AppArchitecture_t appArch) :
            this(id , ver , appArch , DateTime.Now)
        { }

        public AppUpdate()
        {
            DeployTime = NOT_YET;
            CreationTime = DateTime.Now;
        }

        public DateTime CreationTime { get; private set; }
        public DateTime DeployTime { get; set; }
        public AppArchitecture_t AppArchitecture { get; private set; }
        public Version Version { get; private set; }
        public bool IsDeployed => DeployTime != NOT_YET && DeployTime != NEVER;


        //protected:
        protected override void DoRead(IReader reader)
        {
            byte appArch = reader.ReadByte();
            string ver = reader.ReadString();

            if (!Enum.IsDefined(typeof(AppArchitecture_t) , appArch) || string.IsNullOrWhiteSpace(ver))
                throw new CorruptedStreamException();

            CreationTime = reader.ReadTime();
            DeployTime = reader.ReadTime();
            Version = Version.Parse(ver);
            AppArchitecture = (AppArchitecture_t)appArch;
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write((byte)AppArchitecture);

            writer.Write(Version.ToString());

            writer.Write(CreationTime);
            writer.Write(DeployTime);
        }

        protected override string[] GetContent()
        {
            string deployStr = DeployTime == NOT_YET ? "" : (DeployTime == NEVER ? "Ignorée" : DeployTime.ToString());
            return new[]
            {
                ID.ToString(),
                Version.ToString(),
                AppArchitectures.GetArchitectureName(AppArchitecture),
                CreationTime.ToString(),
                DeployTime == NOT_YET? "" : (DeployTime == NEVER? "Ignorée" : DeployTime.ToString())
            };
        }
    }
}
