using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace easyLib
{
    public class MRUList<T>: IEnumerable<T> where T : IEquatable<T>
    {
        readonly T[] m_items;
        int m_count;


        public MRUList(int capacity)
        {
            Assert(capacity >= 0);

            m_items = new T[capacity];
        }

        public MRUList(IEnumerable<T> items, int capacity) :
            this(capacity)
        {
            foreach(T item in items.Take(capacity))
                m_items[m_count++] = item;
        }
        

        public int Capacity => m_items.Length;
        public int Count => m_count;


        public bool Add(T item)
        {
            if (Capacity == 0)
                return false;

            bool result;
            int ndx = Array.FindIndex(m_items , x => item.Equals(x));

            if(ndx >= 0)
            {
                T t = m_items[ndx];
                Array.Copy(m_items , 0 , m_items , 1 , ndx);
                m_items[0] = t;
                result = false;
            }
            else
            {
                if (m_count == Capacity)
                    Array.Copy(m_items , 0 , m_items , 1 , Count - 1);
                else
                {
                    Array.Copy(m_items , 0 , m_items , 1 , Count);
                    ++m_count;
                }

                m_items[0] = item;
                result = true;
            }

            return result;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_count; ++i)
                yield return m_items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
