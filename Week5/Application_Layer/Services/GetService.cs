/*using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.IRepositories;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;
using Week5.Application_Layer.Services;

namespace Week5.Application_Layer.Services
{
    public class IStudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public IStudentService(IStudentRepository studentRepository) =>
            _studentRepository = studentRepository;

        // แก้ไขให้ชนิดการคืนค่าตรงตามที่กำหนดใน interface
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

        // แก้ไขให้ชนิดการคืนค่าตรงตามที่กำหนดใน interface
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

        // แก้ไขให้ชนิดการคืนค่าตรงตามที่กำหนดใน interface
        public async Task<ApiResponse<Professor>> GetProfessorByIdAsync(int professorId)
        {
            var professor = await _studentRepository.GetProfessorByIdAsync(professorId);
            if (professor == null)
            {
                return new ApiResponse<Professor>
                {
                    Success = false,
                    Message = ResponseMessages.ProfessorNotFound,
                    Data = null
                };
            }
            return new ApiResponse<Professor>
            {
                Success = true,
                Message = ResponseMessages.ProfessorGetSuccess,
                Data = professor
            };
        }

        // แก้ไขให้ชนิดการคืนค่าตรงตามที่กำหนดใน interface
        public async Task<ApiResponse<Major>> GetMajorByIdAsync(int majorId)
        {
            var major = await _studentRepository.GetMajorByIdAsync(majorId);
            if (major == null)
            {
                return new ApiResponse<Major>
                {
                    Success = false,
                    Message = ResponseMessages.MajorNotFound,
                    Data = null
                };
            }
            return new ApiResponse<Major>
            {
                Success = true,
                Message = ResponseMessages.MajorGetSuccess,
                Data = major
            };
        }
    }
}
*/