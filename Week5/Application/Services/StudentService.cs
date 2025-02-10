using Week5.Application.Interfaces;
using Week5.Domain;
using Week5.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Week5.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
                })
                .ToListAsync();
        }

        public async Task<StudentDetailsDTO> GetStudentByIdAsync(int studentId)
        {
            var s = await _context.Student
                .Include(s => s.Professor)
                .Include(s => s.Major)
                .Include(s => s.BehaviorScore)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (s == null)
            {
                throw new KeyNotFoundException("ไม่มีข้อมูล");
            }

            return new StudentDetailsDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname,
                ProfessorName = s.Professor.ProfessorName,
                MajorID = s.MajorID,
                MajorName = s.Major.MajorName,
                Scores = s.BehaviorScore.Select(bs => bs.Score).ToList()
            };
        }

        public async Task<Student> AddStudentAsync(StudentCreateDTO studentCreateDto)
        {
            var professor = await _context.Professor
                .FirstOrDefaultAsync(p => p.ProfessorID == studentCreateDto.ProfessorID);
            var major = await _context.Major
                .FirstOrDefaultAsync(m => m.MajorID == studentCreateDto.MajorID);

            if (professor == null || major == null)
            {
                throw new Exception("Professor or Major not found.");
            }

            var student = new Student
            {
                StudentName = studentCreateDto.StudentName,
                StudentSurname = studentCreateDto.StudentSurname,
                ProfessorID = studentCreateDto.ProfessorID,
                MajorID = studentCreateDto.MajorID,
                Professor = professor,
                Major = major,
                StudentClass = new List<StudentClass>(),
                BehaviorScore = new List<BehaviorScore>()
            };


            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> UpdateStudentAsync(int studentId, StudentUpdateDTO updatedStudent)
        {
            var student = await _context.Student
                .Include(s => s.StudentClass)
                .Include(s => s.BehaviorScore)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null) return null;

            student.StudentName = updatedStudent.StudentName;
            student.StudentSurname = updatedStudent.StudentSurname;
            student.ProfessorID = updatedStudent.ProfessorID;
            student.MajorID = updatedStudent.MajorID;

            if (updatedStudent.BehaviorScore != null)
            {
                student.BehaviorScore.Clear();
                foreach (var score in updatedStudent.BehaviorScore)
                {
                    student.BehaviorScore.Add(new BehaviorScore
                    {
                        ScoreID = score.ScoreID,
                        StudentID = student.StudentID,
                        Score = score.Score,
                        Student = student
                    });
                }
            }

            await _context.SaveChangesAsync();
            return student;
        }


        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var student = await _context.Student.Include(s => s.BehaviorScore).FirstOrDefaultAsync(s => s.StudentID == studentId);
            if (student == null)
            {
                return false;
            }

            _context.BehaviorScore.RemoveRange(student.BehaviorScore);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
