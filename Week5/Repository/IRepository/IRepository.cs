using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Week5.Repository.IRepository
{
    public class IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes);
    }
}
