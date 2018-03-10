using System;
using System.Collections.Generic;

namespace DGD.HubCore
{
    public enum AppArchitecture_t : byte
    {
        Win7SP1,
        Win7SP1X64,
        WinXP
    }


    public static class AppArchitectures
    {
        static string[] m_archNames =
        {
            "Microsoft Windows 7 SP 1",
            "Microsoft Windows 7 SP 1, 64 bits",
            "Microsoft Windows XP"
        };


        public static IEnumerable<AppArchitecture_t> Architectures
        {
            get
            {
                foreach (AppArchitecture_t item in Enum.GetValues(typeof(AppArchitecture_t)))
                    yield return item;
            }
        }

        public static IEnumerable<string> ArchitecturesNames => m_archNames;

        public static string GetArchitectureName(AppArchitecture_t sys) => m_archNames[(byte)sys];
    }
}
