namespace easyLib.DB
{
    public interface IWeakProvider<T> : IProvider<T>
    {
        bool AutoFlush { get; set; }

        void Insert(T item);
        void Replace(int ndxItem , T item);
        void Delete(int ndxItem);
    }
}
