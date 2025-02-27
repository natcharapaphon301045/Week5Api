using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;

namespace Week5.Application_Layer.Interfaces
{
    public interface IProfessorService
    {
        Task<ApiResponse<Professor>> GetProfessorByIdAsync(int professorId);
    }
}
