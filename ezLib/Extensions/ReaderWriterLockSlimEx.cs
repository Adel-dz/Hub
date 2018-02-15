using System.Threading;
using System.Diagnostics;


namespace easyLib.Extensions
{
    public static class ReaderWriterLockSlimEx
    {
        public static AutoReleaser AcquireReadLock(this ReaderWriterLockSlim rwLock)
        {
            Debug.Assert(rwLock != null);

            rwLock.EnterReadLock();
            return new AutoReleaser(() => rwLock.ExitReadLock());
        }

        public static AutoReleaser AcquireWriteLock(this ReaderWriterLockSlim rwLock)
        {
            Debug.Assert(rwLock != null);

            rwLock.EnterWriteLock();
            return new AutoReleaser(() => rwLock.ExitWriteLock());
        }
    }
}
