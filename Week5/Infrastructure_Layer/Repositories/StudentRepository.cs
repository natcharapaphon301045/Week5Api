using Week5.Domain_Layer.Entity;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Week5.Infrastructure_Layer.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Week5DbContext _context;

        public StudentRepository(Week5DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _context.Student.ToListAsync(); 
        }
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            return student;

        }
        public async Task<Professor?> GetProfessorByIdAsync(int professorId)
        {
            return await _context.Professor.FirstOrDefaultAsync(p => p.ProfessorID == professorId);
        }
        public async Task<Major?> GetMajorByIdAsync(int majorId)
        {
            return await _context.Major.FirstOrDefaultAsync(m => m.MajorID == majorId);
        }
        public async Task CreateStudentAsync(Student student)
        {
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();
        }
    }
}
