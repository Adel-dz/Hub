using easyLib;

namespace DGD.HubCore.RunOnce
{
    public enum RunOnceAction_t
    {
        None,
        DeleteFile,
        ResetClientInfo,
        ResetUpdateInfo,
        ForceDataUpdate,
    }


    public interface IRunOnceAction: IStorable
    {
        RunOnceAction_t ActionCode { get; }
    }
}
