using System;



namespace easyLib.Jobs
{
    public enum JobState_t
    {
        Pending,
        Running,
        Finished,
        Paused
    }


    public interface IJob
    {
        event Action<IJob> Succeeded;     
        event Action<IJob,Exception> Aborted;

        JobState_t State { get; }

        void Start();
    }    
}
