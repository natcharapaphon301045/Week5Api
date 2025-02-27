using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.Entity;
using Week5.Infrastructure_Layer.Repositories;

namespace Week5.Application_Layer.Services
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _majorRepository;

        public MajorService(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        public async Task<ApiResponse<Major>> GetMajorByIdAsync(int majorId)
        {
            var major = await _majorRepository.GetByIdAsync(majorId);
            if (major == null) return new ApiResponse<Major>("Major not found.");
            return new ApiResponse<Major>(major);
        }
    }
}
