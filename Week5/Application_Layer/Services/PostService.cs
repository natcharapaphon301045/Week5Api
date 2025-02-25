using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.IRepositories;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;

namespace Week5.Application_Layer.Services
{
    public class PostService : IStudentService
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
                StudentName = createDto.StudentName,
                StudentSurname = createDto.StudentSurname,
                ProfessorID = (int?)createDto.ProfessorID ?? 1,
                MajorID = (int?)createDto.MajorID ?? 1,
                StudentClass = new List<StudentClass>(),
                BehaviorScore = new List<BehaviorScore>
        {
            new BehaviorScore { Score = 100 }
        }
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
                StudentClass = student.StudentClass?.Select(sc => new StudentClassDTO { ClassID = sc.ClassID }).ToList() ?? new List<StudentClassDTO>(),
                BehaviorScore = student.BehaviorScore?.Select(bs => bs.Score).ToList() ?? new List<int>()
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
