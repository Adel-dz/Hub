using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace easyLib.Extensions
{
    public static class EnumerableEx
    {
        struct EqualityComparer<T>: IEqualityComparer<T>
        {
            Func<T , T , bool> m_comparer;


            public EqualityComparer(Func<T,T,bool> comparer)
            {
                m_comparer = comparer;
            }
                        
            public bool Equals(T x , T y) => m_comparer(x , y);
            
            public int GetHashCode(T obj) => obj.GetHashCode();
        }


        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable seq)
        {
            Assert(seq != null);

            foreach (T item in seq)
                yield return item;
        }

        public static IEnumerable<T> NoDup<T>(this IEnumerable<T> seq, Func<T,T,bool> comparer)
        {
            Assert(seq != null);
            Assert(comparer != null);

            return seq.Distinct(new EqualityComparer<T>(comparer));
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> seq, params T[] items) => seq.Concat(items);
    }
}
