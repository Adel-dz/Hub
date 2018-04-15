using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDataGuard
{
    static class AppData
    {
        public static byte[] FileSignature => Encoding.UTF8.GetBytes("GSS1");
    }

    enum BackupFile_t: byte
    {
        Data,
        Updates,
        Logs,
        Sys
    }
}
