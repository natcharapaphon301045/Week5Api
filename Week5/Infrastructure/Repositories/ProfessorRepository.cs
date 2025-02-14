using Week5.Domain.Entity;
using Week5.Infrastructure;

namespace Week5.Infrastructure.Repositories
{
    public class ProfessorRepository : Repository<Professor>, IRepository<Professor>
    {
        public ProfessorRepository(Week5DbContext context) : base(context)
        {
        }
    }
}
