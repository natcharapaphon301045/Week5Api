using Week5.Domain_Layer.Entity;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Week5.Infrastructure_Layer.Repositories
{
    public class StudentRepository(Week5DbContext _context) : IStudentRepository
    {
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
