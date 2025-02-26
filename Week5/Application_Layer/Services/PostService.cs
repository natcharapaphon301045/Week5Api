using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.IRepositories;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;
using Week5.Application_Layer.Services;

namespace Week5.Application_Layer.Services
{
    public class PostService : IStudentService.PostService
    {
        private readonly IStudentRepository _studentRepository;

        public PostService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ApiResponse<StudentDTO>> CreateStudentAsync(StudentDTO StudentDto)
        {
            var professor = await _studentRepository.GetProfessorByIdAsync(StudentDto.ProfessorID);
            var major = await _studentRepository.GetMajorByIdAsync(StudentDto.MajorID);

            if (professor == null)
            {
                return new ApiResponse<StudentDTO> { Success = false, Message = "Professor not found" };
            }

            if (major == null)
            {
                return new ApiResponse<StudentDTO> { Success = false, Message = "Major not found" };
            }

            var student = new Student
            {
                StudentName = StudentDto.StudentName,
                StudentSurname = StudentDto.StudentSurname,
                ProfessorID = StudentDto.ProfessorID,
                MajorID = StudentDto.MajorID,
                Professor = professor,
                Major = major,
                StudentClass = new List<StudentClass>(),
                BehaviorScore = new List<BehaviorScore>() 
            };


            await _studentRepository.CreateStudentAsync(student);
            var result = await _studentRepository.SaveChangeAsync();

            if (!result)
            {
                return new ApiResponse<StudentDTO> { Success = false, Message = "Error creating student" };
            }

            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                ProfessorID = student.ProfessorID,
                MajorID = student.MajorID,
            };

            return new ApiResponse<StudentDTO>
            {
                Success = true,
                Message = "Student created successfully",
                Data = studentDTO
            };
        }
    }
}
