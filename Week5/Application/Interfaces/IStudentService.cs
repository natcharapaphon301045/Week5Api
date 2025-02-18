using Week5.Application.Constants;
using Week5.Application.DTOs;
using Week5.Domain.Entity;

namespace Week5.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<IEnumerable<StudentDetailsDTO>>> GetAllStudentsAsync();
        Task<ApiResponse<StudentDetailsDTO?>> GetStudentByIdAsync(int studentId);
        Task<ApiResponse<Student>> AddStudentAsync(StudentCreateDTO studentCreateDto);
        Task<ApiResponse<Student?>> UpdateStudentAsync(int studentId, StudentUpdateDTO updatedStudent);
        Task<ApiResponse<bool>> DeleteStudentAsync(int studentId);
    }
}
