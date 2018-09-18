using DGD.HubCore.RunOnce;
using easyLib;

namespace DGD.Hub.RunOnce
{
    sealed class ResetClientInfo: IRunOnceCommand
    {
        public RunOnceAction_t ActionCode => RunOnceAction_t.ResetClientInfo;

        public void Read(IReader reader)
        { }

        public void Run() => Program.Settings.ClientInfo = null;
        
        public void Write(IWriter writer) { }

    }
}
