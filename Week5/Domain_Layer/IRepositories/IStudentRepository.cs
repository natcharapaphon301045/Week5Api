using Week5.Domain_Layer.Entity;

namespace Week5.Domain_Layer.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int studentId);
    }
}
