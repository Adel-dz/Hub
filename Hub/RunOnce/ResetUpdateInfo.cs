using DGD.HubCore.RunOnce;
using easyLib;

namespace DGD.Hub.RunOnce
{
    sealed class ResetUpdateInfo: IRunOnceAction
    {
        public RunOnceAction_t ActionCode => RunOnceAction_t.ResetUpdateInfo;

        public void Read(IReader reader)
        { }

        public void Run()
        {
            Program.Settings.DataGeneration = 0;
            Program.Settings.UpdateKey = 0;
        }

        public void Write(IWriter writer)
        { }
    }
}
