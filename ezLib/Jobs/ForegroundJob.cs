using System;
using System.Threading;
using static System.Diagnostics.Debug;


namespace easyLib.Jobs
{
    public interface IForegroundJob : IJob
    {
        event Action<IForegroundJob> Canceled;

        void Cancel();
    }

    public abstract class ForegroundJob: IForegroundJob
    {
        public event Action<IJob,Exception> Aborted;
        public event Action<IForegroundJob> Canceled;
        public event Action<IJob> Succeeded;


        public JobState_t State { get; private set; }


        public void Cancel()
        {
            Assert(State != JobState_t.Pending);

            CancelRequired = true;
        }

        public void Start()
        {
            Assert(State == JobState_t.Pending);
            DoStart();
        }


        //protected:        
        protected abstract bool DoWork();

        protected virtual void DoStart()
        {
            State = JobState_t.Running;
            new Thread(ThreadProc).Start();
        }

        protected bool CancelRequired { get; private set; }
        

        //private:
        void ThreadProc()
        {
            try
            {
                bool done = DoWork();
                State = JobState_t.Finished;
                
                if (done)
                    Succeeded?.Invoke(this);
                else
                    Canceled?.Invoke(this);
            }
            catch(Exception ex)
            {
                State = JobState_t.Finished;
                Aborted?.Invoke(this,ex);
            }

            Aborted = null;
            Canceled = Succeeded = null;
        }
    }
}
