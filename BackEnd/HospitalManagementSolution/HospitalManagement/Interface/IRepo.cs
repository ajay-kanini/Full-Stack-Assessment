namespace HospitalManagement.Interface
{
    public interface IRepo<T, K>
    {
        public Task<T?> Add(T item);
        public Task<T?> Update(T item);  
        public Task<T?> Delete(T item);  
        public Task<T?> Get(K key);   
        public Task<ICollection<T>?> GetAll();

    }
}
