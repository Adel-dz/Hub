using easyLib;
using System;
using System.Collections.Generic;

namespace DGD.HubCore.Updating
{

    public interface IUpdateManifest
    {
        uint UpdateKey { get; }
        uint DataGeneration { get; }
        IReadOnlyDictionary<AppArchitecture_t , Version> Versions { get; }

        Version GetAppVersion(AppArchitecture_t appArch);
    }



    public sealed class UpdateManifest: IUpdateManifest
    {
        readonly Dictionary<AppArchitecture_t , Version> m_appVersions = new Dictionary<AppArchitecture_t , Version>();

        public UpdateManifest(uint updateKey , uint dataGeneration , IReadOnlyDictionary<AppArchitecture_t , Version> appVersions = null)
        {
            UpdateKey = updateKey;
            DataGeneration = dataGeneration;

            if (appVersions != null)
                foreach (KeyValuePair<AppArchitecture_t , Version> pair in appVersions)
                    m_appVersions.Add(pair.Key , pair.Value);
        }


        public uint UpdateKey { get; }
        public uint DataGeneration { get; }
        public IReadOnlyDictionary<AppArchitecture_t , Version> Versions => m_appVersions;

        public Version GetAppVersion(AppArchitecture_t appArch)
        {
            Version ver;

            m_appVersions.TryGetValue(appArch , out ver);
            return ver;
        }
    }
}
