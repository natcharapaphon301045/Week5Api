using Week5.Application_Layer.DTOs;
using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.Entity;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Repositories;

namespace Week5.Application_Layer.Services
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
            var student = await _studentRepository.GetAllAsync();
            var studentDTOs = student.Select(s => new StudentDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname
            }).ToList();
            return new ApiResponse<IEnumerable<StudentDTO>>(true, ResponseMessages.StudentGetSuccess, studentDTOs);
        }

        public async Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null) 
                return new ApiResponse<StudentDTO>(ResponseMessages.StudentGetNotFound);

            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname
            };
            return new ApiResponse<StudentDTO>(studentDTO);
        }

        public async Task<ApiResponse<StudentDTO>> CreateStudentAsync(StudentDTO studentDTO)
        {
            var student = new Student
            {
                StudentID = studentDTO.StudentID,
                StudentName = studentDTO.StudentName,
                StudentSurname = studentDTO.StudentSurname,
                Professor = professor,
                Major = major

            };

            await _studentRepository.CreateStudentAsync(student);
            return new ApiResponse<StudentDTO>(true, ResponseMessages.StudentPostSuccess ,studentDTO);
        }


    }
}
