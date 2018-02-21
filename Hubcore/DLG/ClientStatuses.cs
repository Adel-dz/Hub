using System;
using System.Collections.Generic;

namespace DGD.HubCore.DLG
{
    public enum ClientStatus_t: byte
    {
        Unknown = 0,
        Enabled,
        Disabled,
        Banned
    }


    public static class ClientStatuses
    {
        readonly static string[] m_statusNames =
        {
            "",
            "Activé",
            "Désactivé" ,
            "Banni"
        };

        public static IEnumerable<ClientStatus_t> Statuses
        {
            get
            {
                foreach (ClientStatus_t item in Enum.GetValues(typeof(ClientStatus_t)))
                    yield return item;
            }
        }

        public static IEnumerable<string> StatusNames => m_statusNames;

        public static string GetStatusName(ClientStatus_t status) => m_statusNames[(byte)status];
    }
}
