using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Week5.Application.Interfaces;
using Week5.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Week5DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(Week5DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
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
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "StudentID") == id);
    }

    // เพิ่ม method สำหรับการเพิ่มข้อมูล
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // เพิ่ม method สำหรับการอัพเดตข้อมูล
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    // เพิ่ม method สำหรับการลบข้อมูล
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
