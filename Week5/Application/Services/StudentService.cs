using Week5.Application.Interfaces;
using Week5.Domain;
using Week5.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Week5.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly Week5DbContext _context;

        public StudentService(Week5DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Student.ToListAsync();
            /*return await _context.Students
                .Include(s => s.BehaviorScores)  // ดึงข้อมูล BehaviorScore มาด้วย
                .ToListAsync();*/
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return await _context.Student
                .Include(s => s.BehaviorScore)  // ดึงข้อมูล BehaviorScore มาด้วย
                .FirstOrDefaultAsync(s => s.StudentID == studentId);
        }
    }

}
