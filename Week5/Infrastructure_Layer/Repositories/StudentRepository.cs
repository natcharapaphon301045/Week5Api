using Week5.Domain_Layer.Entity;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Persistence;
using Microsoft.EntityFrameworkCore;
using Week5.Application_Layer.DTOs;

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
            return await _context.Student
                .AsNoTracking()
                .Include(s => s.StudentClass.Where(sc => !sc.IsDeleted))
                .ThenInclude(sc => sc.Class)
                .Include(s => s.BehaviorScore.Where(bs => !bs.IsDeleted))
                .ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await _context.Student
                .Include(s => s.StudentClass.Where(sc => !sc.IsDeleted))
                .ThenInclude(sc => sc.Class)
                .Include(s => s.BehaviorScore.Where(bs => !bs.IsDeleted))
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

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

        public async Task<bool> UpdateStudentAsync(int studentId, StudentDTO studentDTO)
        {
            var student = await _context.Student
                .Include(s => s.StudentClass)
                .Include(s => s.BehaviorScore)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null)
                return false;

            student.StudentName = studentDTO.StudentName;
            student.StudentSurname = studentDTO.StudentSurname;
            student.ProfessorID = studentDTO.ProfessorID;
            student.MajorID = studentDTO.MajorID;

            _context.Student.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
                return false;

            // Soft delete related StudentClass and BehaviorScore
            var studentClasses = await _context.StudentClass.Where(sc => sc.StudentID == studentId).ToListAsync();
            foreach (var studentClass in studentClasses)
            {
                studentClass.IsDeleted = true;
            }

            var behaviorScores = await _context.BehaviorScore.Where(bs => bs.StudentID == studentId).ToListAsync();
            foreach (var behaviorScore in behaviorScores)
            {
                behaviorScore.IsDeleted = true;
            }

            _context.Student.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
