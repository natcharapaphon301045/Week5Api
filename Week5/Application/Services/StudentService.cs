using Week5.Application.Interfaces;
using Week5.Domain;
using Week5.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Week5.Application.DTOs;


namespace Week5.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly Week5DbContext _context;

        public StudentService(Week5DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsAsync()
        {
            return await _context.Student
                .Include(s => s.Professor)
                .Include(s => s.Major)
                .Include(s => s.BehaviorScore)
                .Select(s => new StudentDetailsDTO
                {
                    StudentID = s.StudentID,
                    StudentName = s.StudentName,
                    StudentSurname = s.StudentSurname,
                    ProfessorName = s.Professor.ProfessorName,
                    MajorID = s.MajorID,
                    MajorName = s.Major.MajorName,
                    Scores = s.BehaviorScore.Select(bs => bs.Score).ToList()
                })
                .ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return await _context.Student
                .Include(s => s.Professor)
                .Include(s => s.Major)
                .Include(s => s.StudentClass)
                .Include(s => s.BehaviorScore)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);
        }

        public async Task<Student?> AddStudentAsync(Student student)
        {
            var existingStudent = await _context.Student
                .FirstOrDefaultAsync(s => s.StudentID == student.StudentID);
            if (existingStudent != null) { 
                return null; // ถ้าพบข้อมูล student ซ้ำ
            }
            
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(int studentId, Student updatedStudent)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null) return null; // ถ้าไม่พบข้อมูล student

            // อัพเดตข้อมูลที่ต้องการ
            student.StudentName = updatedStudent.StudentName;
            student.StudentSurname = updatedStudent.StudentSurname;
            student.ProfessorID = updatedStudent.ProfessorID;
            student.MajorID = updatedStudent.MajorID;

            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var student = await _context.Student.FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null) return false; // ถ้าไม่พบข้อมูล student

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
