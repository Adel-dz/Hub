using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubCore
{
    public static class FileLocker
    {
        class FileLock
        {
            public FileLock(string file)
            {
                FilePath = file;
                LockCount = 1;
            }

            public string FilePath { get; }
            public int LockCount { get; set; }
        }


        static readonly List<FileLock> m_locks = new List<FileLock>();
              
        public static IDisposable Lock(string filePath)
        {
            string path = Path.GetFullPath(filePath).ToLower();

            FileLock fLock;

            lock (m_locks)
            {
                int ndx = m_locks.FindIndex(fl => fl.FilePath == path);                

                if (ndx == -1)
                {
                    fLock = new FileLock(path);                    
                    m_locks.Add(fLock);
                }
                else
                {
                    fLock = m_locks[ndx];
                    ++fLock.LockCount;
                }
            }

            Monitor.Enter(fLock);

            return new AutoReleaser(() => Unlock(fLock));
        }

        //private:
        static void Unlock(FileLock fLock)
        {

            lock(m_locks)
            {
                if (--fLock.LockCount == 0)
                    m_locks.Remove(fLock);
            }

            Monitor.Exit(fLock);
        }
    }
}
