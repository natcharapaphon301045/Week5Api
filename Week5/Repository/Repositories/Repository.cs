using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Week5.Infrastructure;
using Week5.Repository.IRepository;

namespace Week5.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Week5DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(Week5DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }
    }

}
