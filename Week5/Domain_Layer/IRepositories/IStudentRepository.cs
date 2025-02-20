using Week5.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Week5.Domain.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int studentId);
    }
}
