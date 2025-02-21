using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.IRepositories;
using Week5.Application_Layer.DTOs;
using Week5.Infrastructure;
using Week5.Domain_Layer.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week5.Application_Layer.Services
{
    public class StudentService(IStudentRepository studentRepository) : IStudentService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;

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
            }).ToList();

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

        public Task<ApiResponse<bool>> InitializeStudentDataAsync()
        {
            return Task.FromResult(new ApiResponse<bool>
            {
                Success = true,
                Message = ResponseMessages.StudentGetNotFound,
                Data = true
            });
        }
    }
}
