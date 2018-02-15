using System.Collections.Generic;

namespace easyLib.DB
{

    public interface IProvider<out T>
    {
        int Count { get; }  
        T Get(int ndx);

        IEnumerable<T> Enumerate(int ndxFirst);
        IEnumerable<T> Enumerate();

    }
}
