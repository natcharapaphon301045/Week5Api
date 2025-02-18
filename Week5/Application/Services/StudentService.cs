using Week5.Application.Interfaces;
using Week5.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week5.Application.Constants;
using Week5.trash;

namespace Week5.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Professor> _professorRepository;
        private readonly IRepository<Major> _majorRepository;

        public StudentService(IRepository<Student> studentRepository, IRepository<Professor> professorRepository, IRepository<Major> majorRepository)
        {
            _studentRepository = studentRepository;
            _professorRepository = professorRepository;
            _majorRepository = majorRepository;
        }

        public async Task<ApiResponse<IEnumerable<StudentDetailsDTO>>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllWithIncludeAsync(
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);
            var studentDetails = students.Select(MapToStudentDetailsDTO).ToList();
            return new ApiResponse<IEnumerable<StudentDetailsDTO>>(true, ResponseMessages.StudentsRetrievedSuccessfully, studentDetails);
        }

        public async Task<ApiResponse<StudentDetailsDTO?>> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);
            if (student == null)
            {
                return new ApiResponse<StudentDetailsDTO?>(false, ResponseMessages.StudentNotFound);
            }
            var studentDetails = MapToStudentDetailsDTO(student);
            return new ApiResponse<StudentDetailsDTO?>(true, ResponseMessages.StudentsRetrievedSuccessfully, studentDetails);
        }

        public async Task<ApiResponse<Student>> AddStudentAsync(StudentCreateDTO studentCreateDto)
        {
            var professor = await _professorRepository.GetByIdWithIncludeAsync(studentCreateDto.ProfessorID);
            var major = await _majorRepository.GetByIdWithIncludeAsync(studentCreateDto.MajorID);

            if (professor == null || major == null)
            {
                return new ApiResponse<Student>(false, ResponseMessages.InvalidProfessorOrMajorID);
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

            await _studentRepository.AddAsync(student);
            return new ApiResponse<Student>(true, ResponseMessages.StudentAddedSuccessfully, student);
        }

        public async Task<ApiResponse<Student?>> UpdateStudentAsync(int studentId, StudentUpdateDTO updatedStudent)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.Professor,
                s => s.Major,
                s => s.BehaviorScore);
            if (student == null)
            {
                return new ApiResponse<Student?>(false, ResponseMessages.StudentNotFound);
            }

            student.StudentName = updatedStudent.StudentName;
            student.StudentSurname = updatedStudent.StudentSurname;
            student.ProfessorID = updatedStudent.ProfessorID;
            student.MajorID = updatedStudent.MajorID;

            await _studentRepository.UpdateAsync(student);
            return new ApiResponse<Student?>(true, ResponseMessages.StudentUpdatedSuccessfully, student);
        }

        public async Task<ApiResponse<bool>> DeleteStudentAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(studentId,
                s => s.BehaviorScore);

            if (student == null)
            {
                return new ApiResponse<bool>(false, ResponseMessages.StudentNotFound);
            }

            await _studentRepository.DeleteAsync(student);
            return new ApiResponse<bool>(true, ResponseMessages.StudentDeletedSuccessfully);
        }

        private static StudentDetailsDTO MapToStudentDetailsDTO(Student student)
        {
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
    }
}