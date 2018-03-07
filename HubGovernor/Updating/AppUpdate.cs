using DGD.HubCore.DB;
using DGD.HubCore.Updating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;
using DGD.HubCore;

namespace DGD.HubGovernor.Updating
{

    interface IAppUpdate: IDataRow
    {
        ClientApplication_t Application { get; }
        string Version { get; }
        TargetSystem_t TargetSystem { get; }
        DateTime CreationTime { get; }
        DateTime DeployTime { get; set; }
    }

    sealed class AppUpdate: DataRow, IAppUpdate
    {
        public static readonly DateTime NULL_TIME = default(DateTime);

        public ClientApplication_t Application { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime DeployTime { get; set; }
        public TargetSystem_t TargetSystem { get; private set; }
        public string Version { get; private set; }        


        //protected:
        protected override void DoRead(IReader reader)
        {
            reader.ReadByte(); //Client Application ignored for now
            byte targetSys = reader.ReadByte();
            string ver = reader.ReadString();

            if (!Enum.IsDefined(typeof(TargetSystem_t) , targetSys) || string.IsNullOrWhiteSpace(ver))
                throw new CorruptedStreamException();

            CreationTime = reader.ReadTime();
            DeployTime = reader.ReadTime();
            Version = ver;
            TargetSystem = (TargetSystem_t)targetSys;
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write((byte)ClientApplication_t.Hub);
            writer.Write((byte)TargetSystem);

            Dbg.Assert(!string.IsNullOrEmpty(Version));
            writer.Write(Version);

            writer.Write(CreationTime);
            writer.Write(DeployTime);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Version,
                TargetSystems.GetTargetName(TargetSystem),
                CreationTime.ToString(),
                DeployTime == NULL_TIME? "" : DeployTime.ToString()
            };
        }
    }
}
