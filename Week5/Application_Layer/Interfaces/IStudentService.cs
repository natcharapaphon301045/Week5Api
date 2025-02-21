using Week5.Application_Layer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Week5.Domain_Layer.Entity;
using Week5.Application_Layer.DTOs;

namespace Week5.Application_Layer.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<IEnumerable<StudentDTO>>> GetAllStudentsAsync();
        Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId);
        Task<ApiResponse<bool>> InitializeStudentDataAsync();
    }
}

