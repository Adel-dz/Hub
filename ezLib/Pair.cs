using System;

namespace easyLib
{
    public struct Pair<T1, T2>: IEquatable<Pair<T1 , T2>>, IDisposable
    {
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
            IsDisposed = false;
        }


        public T1 First { get; }
        public T2 Second { get; }
        public bool IsDisposed { get; private set; }


        public void Dispose()
        {
            if (!IsDisposed)
            {
                IDisposable elt = First as IDisposable;
                elt?.Dispose();

                elt = Second as IDisposable;
                elt?.Dispose();

                IsDisposed = true;
            }
        }

        public bool Equals(Pair<T1 , T2> other) => CompareItem(First , other.First) && CompareItem(Second , other.Second);

        public override bool Equals(object obj) => Equals((Pair<T1 , T2>)obj);
        public override int GetHashCode() => First.GetHashCode() + 31 * Second.GetHashCode();


        //private:
        static bool CompareItem<T>(T item1, T item2) => item1.Equals(item2);
    }


    public static class Pair
    {
        public static Pair<T1 , T2> Create<T1, T2>(T1 first , T2 second) => new Pair<T1 , T2>(first , second);
    }
}
