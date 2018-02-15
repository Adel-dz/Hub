using System;
using System.Collections.Generic;

namespace DGD.HubCore.DLG
{
    public enum ProfilePrivilege_t: byte
    {
        Default = 0
    }

    public static class ProfilePrivileges
    {
        readonly static string[] m_privilegesText = { "Par défaut" };


        public static IEnumerable<ProfilePrivilege_t> Privileges
        {
            get
            {
                foreach (ProfilePrivilege_t item in Enum.GetValues(typeof(ProfilePrivilege_t)))
                    yield return item;
            }
        }

        public static IEnumerable<string> PrivilegesNames => m_privilegesText;

        public static string GetPrivilegeName(ProfilePrivilege_t privilege) =>
            m_privilegesText[(byte)privilege];
    }
}
