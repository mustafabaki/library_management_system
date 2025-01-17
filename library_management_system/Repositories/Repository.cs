using library_management_system.Data;
using library_management_system.Repository;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryDB _context;

        public Repository(LibraryDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<(IEnumerable<T>, int)> GetPagedAsync(Func<IQueryable<T>, IQueryable<T>> query, int page, int pageSize)
        {
            var data = query(_context.Set<T>());

            var totalItems = await data.CountAsync();
            var items = await data
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}
