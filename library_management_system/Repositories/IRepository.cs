namespace library_management_system.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task<(bool, T record)> Update(T entity);
        void Delete(T entity);

        Task<(IEnumerable<T>, int)> GetPagedAsync(Func<IQueryable<T>, IQueryable<T>> query, int page, int pageSize);
    }
}
