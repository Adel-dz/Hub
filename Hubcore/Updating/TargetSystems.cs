using System;
using System.Collections.Generic;

namespace DGD.HubCore.Updating
{
    public enum TargetSystem_t : byte
    {
        Win7SP1,
        Win7SP1X64,
        WinXP
    }

    public static class TargetSystems
    {
        static string[] m_sysNames =
        {
            "Microsoft Windows 7 SP 1",
            "Microsoft Windows 7 SP 1, 64 bits",
            "Microsoft Windows XP"
        };


        public static IEnumerable<TargetSystem_t> AllTargets
        {
            get
            {
                foreach (TargetSystem_t item in Enum.GetValues(typeof(TargetSystem_t)))
                    yield return item;
            }
        }

        public static IEnumerable<string> TargetsNames => m_sysNames;

        public static string GetTargetName(TargetSystem_t sys) => m_sysNames[(byte)sys];
    }
}
