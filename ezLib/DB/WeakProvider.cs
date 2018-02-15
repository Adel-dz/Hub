namespace easyLib.DB
{
    public interface IWeakProvider<T> : IProvider<T>
    {
        void Insert(T item);
        void Replace(int ndxItem , T item);
        void Delete(int ndxItem);
    }
}
