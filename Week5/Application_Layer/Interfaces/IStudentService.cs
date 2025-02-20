using Week5.Application.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Week5.Domain.Entity;
using Week5.Application.DTOs;

namespace Week5.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<IEnumerable<StudentDTO>>> GetAllStudentsAsync();
        Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId);
        Task<ApiResponse<bool>> InitializeStudentDataAsync();

    }
}
