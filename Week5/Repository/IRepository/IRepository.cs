using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);  // เพิ่ม method นี้
    Task UpdateAsync(T entity);  // เพิ่ม method นี้
    Task DeleteAsync(T entity);  // เพิ่ม method นี้
}


