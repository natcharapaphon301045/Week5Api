using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;

namespace Week5.Application_Layer.Interfaces
{
    public interface IStudentService  
    {
        public interface IStudentGetService
        {
            Task<ApiResponse<IEnumerable<StudentDTO>>> GetAllStudentsAsync();
            Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId);
            Task<Professor?> GetProfessorByIdAsync(int professorId);
            Task<Major?> GetMajorByIdAsync(int majorId);
        }

        public interface IStudentPostService
        {
            Task<ApiResponse<StudentDTO>> CreateStudentAsync(StudentDTO studentDTO);
            Task<ApiResponse<bool>> InitializeStudentDataAsync();
        }
    }
}
