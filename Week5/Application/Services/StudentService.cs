using Week5.Application.DTOs;
using Week5.Application.Interfaces;
using Week5.Domain;
using Week5.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Week5.Application.Service { 
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllWithIncludeAsync(
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);

            return students.Select(s => new StudentDetailsDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname,
                ProfessorID = s.ProfessorID,
                ProfessorName = s.Professor.ProfessorName,
                MajorID = s.MajorID,
                MajorName = s.Major.MajorName,
                Scores = s.BehaviorScore.Select(b => b.Score).ToList()
            }).ToList();
        }

        public async Task<StudentDetailsDTO> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);

            if (student == null) return null;

            return new StudentDetailsDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                ProfessorID = student.ProfessorID,
                ProfessorName = student.Professor.ProfessorName,
                MajorID = student.MajorID,
                MajorName = student.Major.MajorName,
                Scores = student.BehaviorScore.Select(b => b.Score).ToList()
            };
        }

        public async Task<Student> AddStudentAsync(StudentCreateDTO studentCreateDto)
        {
            var student = new Student
            {
                StudentName = studentCreateDto.StudentName,
                StudentSurname = studentCreateDto.StudentSurname,
                ProfessorID = studentCreateDto.ProfessorID,
                MajorID = studentCreateDto.MajorID,
                Professor = new Professor
                {
                    ProfessorID = studentCreateDto.ProfessorID,
                    ProfessorName = "DefaultName", // Added this line
                    ProfessorSurname = "DefaultSurname" // Added this line
                },
                Major = new Major
                {
                    MajorID = studentCreateDto.MajorID,
                    MajorName = "DefaultMajorName" // Added this line
                },
                StudentClass = new List<StudentClass>(),
                BehaviorScore = new List<BehaviorScore>()
            };

            await _studentRepository.AddAsync(student);
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(int studentId, StudentUpdateDTO updatedStudent)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);

            if (student == null) return null;

            student.StudentName = updatedStudent.StudentName;
            student.StudentSurname = updatedStudent.StudentSurname;
            student.ProfessorID = updatedStudent.ProfessorID;
            student.MajorID = updatedStudent.MajorID;

            await _studentRepository.UpdateAsync(student);
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.BehaviorScore);

            if (student == null) return false;

            await _studentRepository.DeleteAsync(student);
            return true;
        }
    }
}