//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using static System.Diagnostics.Debug;


//namespace easyLib
//{
//    public interface IFastLock
//    {
//        IDisposable AcquireCooperativeLock();
//        IDisposable AcquireExclusiveLock();
//        void Release();        
//    }



//    public sealed class FastLock: IFastLock
//    {
//        readonly List<int> m_threads = new List<int>();
//        int m_exclLock;
//        int m_coopCount;


//        public IDisposable AcquireCooperativeLock()
//        {
//            while (true)
//                if (Interlocked.CompareExchange(ref m_exclLock , 1 , 0) == 0)
//                {
//                    int idThread = Thread.CurrentThread.ManagedThreadId;

//                    if (m_coopCount >= 0)
//                    {
//                        Assert(m_threads.Count == m_coopCount);

//                        ++m_coopCount;
//                        m_threads.Add(idThread);

//                        m_exclLock = 0;
//                        break;
//                    }
//                    else
//                    {
//                        m_exclLock = 0;
//                        Thread.Yield();
//                    }
//                }
//                else
//                    Thread.Yield();

//            return new AutoReleaser(Release);
//        }

//        public IDisposable AcquireExclusiveLock()
//        {
//            while (true)
//                if (Interlocked.CompareExchange(ref m_exclLock , 1 , 0) == 0)
//                {
//                    int idThread = Thread.CurrentThread.ManagedThreadId;

//                    if (m_coopCount == 0 || (m_coopCount < 0 && m_threads[0] == idThread) )
//                    {
//                        Assert(m_threads.Count == 0 || m_threads[0] == idThread);

//                        if (--m_coopCount == -1)
//                            m_threads.Add(idThread);                        

//                        m_exclLock = 0;
//                        break;
//                    }
//                    else
//                    {
//                        m_exclLock = 0;
//                        Thread.Yield();
//                    }
//                }
//                else
//                    Thread.Yield();

//            return new AutoReleaser(Release);
//        }
        
//        public void Release()
//        {
//            while (true)
//                if (Interlocked.CompareExchange(ref m_exclLock , 1 , 0) == 0)
//                {
//                    int idThread = Thread.CurrentThread.ManagedThreadId;

//                    Assert(m_coopCount != 0);
//                    Assert(m_threads.Contains(idThread));

//                    if(m_coopCount > 0)
//                    {
//                        --m_coopCount;
//                        m_threads.Remove(idThread);
//                    }
//                    else if (++m_coopCount == 0)
//                        m_threads.Remove(idThread);

//                    m_exclLock = 0;
//                    break;
//                }
//                else
//                    Thread.Yield();
//        }        
//    }
//}
