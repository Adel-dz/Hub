using System;
using static System.Diagnostics.Debug;



namespace easyLib
{
    public sealed class AutoReleaser : IDisposable
    {
        readonly Action m_releaser;


        public AutoReleaser(Action releaser)
        {
            Assert(releaser != null);
            m_releaser = releaser;
        }


        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                m_releaser();
                IsDisposed = true;
            }
        }       
    }



    public sealed class AutoReleaser<T> : IDisposable
    {
        readonly T m_obj;
        readonly Action<T> m_releaser;


        public AutoReleaser(T obj, Action<T> releaser)
        { 
            Assert(obj != null);
            Assert(releaser != null);

            m_obj = obj;
            m_releaser = releaser;
        }


        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                m_releaser(m_obj);
                IsDisposed = true;
            }
        }

        public static implicit operator T(AutoReleaser<T> obj)
        {
            Assert(obj != null);
            Assert(!obj.IsDisposed);

            return obj.m_obj;
        }
    }
}
