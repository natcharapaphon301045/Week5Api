using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.Entity;
using Week5.Infrastructure_Layer.Repositories;

namespace Week5.Application_Layer.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<ApiResponse<Professor>> GetProfessorByIdAsync(int professorId)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId);
            if (professor == null) return new ApiResponse<Professor>("Professor not found.");
            return new ApiResponse<Professor>(professor);
        }
    }
}
