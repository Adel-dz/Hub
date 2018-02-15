using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;

namespace DGD.HubGovernor
{
    static class Dbg
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(string msg) => DebugHelper.LogDbgInfo("Hub Governor: " + msg);

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Assert(bool exp) => System.Diagnostics.Debug.Assert(exp);
    }
}
