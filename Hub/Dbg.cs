using easyLib;

namespace DGD.Hub
{

    static class Dbg
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(string msg) => DebugHelper.LogDbgInfo("Hub: " + msg);

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Assert(bool exp) => System.Diagnostics.Debug.Assert(exp);
    }
}
