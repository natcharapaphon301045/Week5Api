using Week5.Domain_Layer.Entity;

namespace Week5.Domain_Layer.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentAsync();
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<Professor?> GetProfessorByIdAsync(int professorId);
        Task<Major?> GetMajorByIdAsync(int majorId);
        Task CreateStudentAsync(Student student);
         
    }
}
