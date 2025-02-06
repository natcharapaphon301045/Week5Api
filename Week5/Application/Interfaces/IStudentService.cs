using Week5.Application.DTOs;
using Week5.Domain;

namespace Week5.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int studentId);
        Task<Student?> AddStudentAsync(Student student);
        Task<Student?> UpdateStudentAsync(int studentId, Student updatedStudent);
        Task<bool> DeleteStudentAsync(int studentId);
    }
}
