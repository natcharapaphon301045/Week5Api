using Week5.Domain.Entity;
using Week5.Infrastructure;

namespace Week5.Infrastructure.Repositories
{
    public class MajorRepository : Repository<Major>, IRepository<Major>
    {
        public MajorRepository(Week5DbContext context) : base(context)
        {
        }
    }
}
