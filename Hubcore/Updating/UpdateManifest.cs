using System;
using System.Collections.Generic;

namespace DGD.HubCore.Updating
{

    public interface IUpdateManifest
    {
        uint UpdateKey { get; }
        uint DataGeneration { get; }
        Version GetAppVersion(AppArchitecture_t appArch);
    }

    public sealed class UpdateManifest: IUpdateManifest
    {
        readonly IDictionary<AppArchitecture_t , Version> m_appVersions;

        public UpdateManifest(uint updateKey, uint dataGeneration, IDictionary<AppArchitecture_t, Version> appVersions)
        {
            UpdateKey = updateKey;
            DataGeneration = dataGeneration;
        }


        public uint UpdateKey { get; }
        public uint DataGeneration { get; }

        public Version GetAppVersion(AppArchitecture_t appArch)
        {
            Version ver;

            m_appVersions.TryGetValue(appArch , out ver);
            return ver;
        }
    }
}
