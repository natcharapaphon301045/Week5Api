using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);  
    Task UpdateAsync(T entity);  
    Task DeleteAsync(T entity);  
}


