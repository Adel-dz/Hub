using System;
using System.Threading;
using static System.Diagnostics.Debug;



namespace easyLib.Jobs
{

    public abstract class Processing 
    {
        public enum State_t
        {
            Pending,
            Running,
            Finished,
            Paused
        }


        AutoResetEvent m_evPause;

        event Action<Processing> Pausing;
        event Action<Processing> Resumed;
        public event Action<Processing, Exception> Aborted;
        public event Action<Processing> Canceled;
        public event Action<Processing> Succeeded;


        public Processing(bool bkgndProcessing = false)
        {
            IsBackground = bkgndProcessing;
        }


        public State_t State { get; private set; }
        public bool IsBackground{ get; private set; }


        public void Start()
        {
            Assert(State == State_t.Pending);

            var t = new Thread(ThreadProc);

            if (IsBackground)
                t.Priority = ThreadPriority.BelowNormal;

            t.Start();            
        }

        public void Pause()
        {
            Assert(State == State_t.Running);
            PauseRequired = true;
        }

        public void Resume()
        {
            Assert(State == State_t.Paused);
            m_evPause.Set();
        }

        public void Cancel()
        {
            Assert(State == State_t.Running);
            CancelRequired = true;
        }


        //protected:
        protected abstract void DoWork();

        protected bool PauseRequired { get; private set; }
        protected bool CancelRequired { get; private set; }

        protected void OnPause()
        {
            Assert(State == State_t.Running);

            Pausing?.Invoke(this);

            using (m_evPause = new AutoResetEvent(false))
            {
                State = State_t.Paused;
                m_evPause.WaitOne();
            }

            State = State_t.Running;
            Resumed?.Invoke(this);
        }

        protected void OnCancel()
        {
            Assert(State == State_t.Running);

            Thread.CurrentThread.Abort();
        }


        //private:
        void ThreadProc()
        {
            State = State_t.Running;

            try
            {
                DoWork();
                State = State_t.Finished;
                Succeeded?.Invoke(this);
            }
            catch(ThreadAbortException)
            {
                Thread.ResetAbort();
                State = State_t.Finished;
                Canceled?.Invoke(this);
            }
            catch(Exception ex)
            {
                State = State_t.Finished;
                Aborted?.Invoke(this , ex);
            }
        }
    }
}
