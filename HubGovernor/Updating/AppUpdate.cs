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
        string Version { get; }
        AppArchitecture_t AppArchitecture { get; }
        DateTime CreationTime { get; }
        DateTime DeployTime { get; set; }
    }

    sealed class AppUpdate: DataRow, IAppUpdate
    {
        public static readonly DateTime NULL_TIME = default(DateTime);

        public AppUpdate(uint id, string ver, AppArchitecture_t appArch, DateTime tmCreation):
            base(id)
        {
            Dbg.Assert(!string.IsNullOrWhiteSpace(ver));

            Version = ver;
            AppArchitecture = appArch;
            CreationTime = tmCreation;
            DeployTime = NULL_TIME;
        }

        public AppUpdate(uint id, string ver, AppArchitecture_t appArch = AppArchitecture_t.Win7SP1): 
            this(id, ver, appArch, DateTime.Now)
        { }

        public AppUpdate()
        {
            DeployTime = NULL_TIME;
            CreationTime = DateTime.Now;
        }

        public DateTime CreationTime { get; private set; }
        public DateTime DeployTime { get; set; }
        public AppArchitecture_t AppArchitecture { get; private set; }
        public string Version { get; private set; }        


        //protected:
        protected override void DoRead(IReader reader)
        {
            byte appArch = reader.ReadByte();
            string ver = reader.ReadString();

            if (!Enum.IsDefined(typeof(AppArchitecture_t) , appArch) || string.IsNullOrWhiteSpace(ver))
                throw new CorruptedStreamException();

            CreationTime = reader.ReadTime();
            DeployTime = reader.ReadTime();
            Version = ver;
            AppArchitecture = (AppArchitecture_t)appArch;
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write((byte)AppArchitecture);

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
                HubCore.Updating.AppArchitectures.GetArchitectureName(AppArchitecture),
                CreationTime.ToString(),
                DeployTime == NULL_TIME? "" : DeployTime.ToString()
            };
        }
    }
}
