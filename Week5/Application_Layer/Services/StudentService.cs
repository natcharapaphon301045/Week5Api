using Week5.Application.Interfaces;
using Week5.Application.Constants;
using Week5.Domain.IRepositories;
using System.Collections.Generic;
using Week5.Application.DTOs;
using System.Threading.Tasks;
using Week5.Domain.Entity;
using System.Linq;

namespace Week5.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ApiResponse<IEnumerable<StudentDTO>>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            var studentDTOs = students.Select(s => new StudentDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname,
                ProfessorID = s.ProfessorID,
                MajorID = s.MajorID
            });

            return new ApiResponse<IEnumerable<StudentDTO>>
            {
                Success = true,
                Message = ResponseMessages.StudentGetSuccess,
                Data = studentDTOs
            };
        }

        public async Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return new ApiResponse<StudentDTO>
                {
                    Success = false,
                    Message = ResponseMessages.StudentGetNotFound,
                    Data = null
                };
            }

            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                ProfessorID = student.ProfessorID,
                MajorID = student.MajorID
            };

            return new ApiResponse<StudentDTO>
            {
                Success = true,
                Message = ResponseMessages.StudentGetSuccess,
                Data = studentDTO
            };
        }

        public async Task<ApiResponse<bool>> InitializeStudentDataAsync()
        {
            // Initialize student data here
            // This is a placeholder implementation
            return await Task.Run(() => new ApiResponse<bool>
            {
                Success = true,
                Message = ResponseMessages.StudentGetNotFound,
                Data = true
            });
        }
    }
}
