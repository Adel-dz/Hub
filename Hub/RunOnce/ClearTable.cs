using DGD.HubCore.RunOnce;
using System.IO;

namespace DGD.Hub.RunOnce
{
    sealed class ClearTable: ClearTableData, IRunOnceAction
    {
        public ClearTable(string tblPath = ""):
            base(tblPath)
        {
            Dbg.Assert(tblPath != null);
        }
        
        public RunOnceAction_t ActionCode => RunOnceAction_t.ClearTable;

        public void Run()
        {
            File.Delete(TablePath);
        }

    }
}
