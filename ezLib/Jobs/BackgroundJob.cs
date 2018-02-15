using System;
using System.Threading;
using static System.Diagnostics.Debug;



namespace easyLib.Jobs
{
    public abstract class BackgroundJob: IJob
    {
        public event Action<IJob,Exception> Aborted;
        public event Action<IJob> Succeeded;


        public JobState_t State { get; private set; }

        public void Start()
        {
            Assert(State == JobState_t.Pending);

            Thread thread = new Thread(ThreadProc);
            thread.Priority = ThreadPriority.BelowNormal;
            thread.IsBackground = true;

            State = JobState_t.Running;
            thread.Start();
        }


        //protected:
        protected abstract void DoWork();
        

        //private:
        void ThreadProc()
        {
            try
            {
                DoWork();
                State = JobState_t.Finished;
                Succeeded?.Invoke(this);
            }
            catch(Exception ex)
            {
                State = JobState_t.Finished;
                Aborted?.Invoke(this,ex);
            }

            Aborted = null;
            Succeeded = null;
        }
    }
}
