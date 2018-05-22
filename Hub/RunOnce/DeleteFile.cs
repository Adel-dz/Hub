using DGD.HubCore.RunOnce;
using System.IO;

namespace DGD.Hub.RunOnce
{
    sealed class DeleteFile: HubCore.RunOnce.DeleteFile, IRunOnceAction
    {
        public DeleteFile(string filePath = ""):
            base(filePath)
        {
            Dbg.Assert(filePath != null);
        }
        
        public RunOnceAction_t ActionCode => RunOnceAction_t.DeleteFile;

        public void Run()
        {
            File.Delete(FilePath);
        }

    }
}
