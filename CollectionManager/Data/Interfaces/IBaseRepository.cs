namespace CollectionManager.Data.Interfaces
{
    public interface IBaseRepository<T> 
    {
        Task<IEnumerable<T>> GetAll ();
        Task<T> GetById (string Id);
        Task<bool> Create(T obj);
        Task<bool> Delete(T obj);
    }
}
