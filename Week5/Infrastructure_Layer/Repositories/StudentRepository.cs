using Week5.Domain.Entity;
using Week5.Domain.IRepositories;
using Week5.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Week5.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Week5DbContext _context;

        public StudentRepository(Week5DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int studentId)
        {
            return await _context.Student.FindAsync(studentId);
        }
    }
}
