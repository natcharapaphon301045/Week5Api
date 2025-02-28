using Week5.Application_Layer.DTOs;
using Week5.Application_Layer.Interfaces;
using Week5.Domain_Layer.Entity;
using Week5.Domain_Layer.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week5.Application_Layer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IBehaviorScoreRepository _behaviorScoreRepository;
        private readonly IStudentClassRepository _studentClassRepository;

        public StudentService(
            IStudentRepository studentRepository,
            IMajorRepository majorRepository,
            IProfessorRepository professorRepository,
            IBehaviorScoreRepository behaviorScoreRepository,
            IStudentClassRepository studentClassRepository)
        {
            _studentRepository = studentRepository;
            _majorRepository = majorRepository;
            _professorRepository = professorRepository;
            _behaviorScoreRepository = behaviorScoreRepository;
            _studentClassRepository = studentClassRepository;
        }

        public async Task<ApiResponse<IEnumerable<StudentDTO>>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentAsync();
            var studentDTOs = students.Select(s => new StudentDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentSurname = s.StudentSurname
            }).ToList();
            return new ApiResponse<IEnumerable<StudentDTO>>(true, ResponseMessages.StudentGetSuccess, studentDTOs);
        }

        public async Task<ApiResponse<StudentDTO>> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId);
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
            int professorId = studentDTO.ProfessorID > 0 ? studentDTO.ProfessorID : 1;
            int majorId = studentDTO.MajorID > 0 ? studentDTO.MajorID : 1;

            var professor = await _professorRepository.GetProfessorByIdAsync(professorId);
            var major = await _majorRepository.GetMajorByIdAsync(majorId);

            var student = new Student
            {
                StudentName = studentDTO.StudentName,
                StudentSurname = studentDTO.StudentSurname,
                Professor = professor,
                Major = major,
                StudentClass = new List<StudentClass>(),
                BehaviorScore = new List<BehaviorScore>()
            };

            if (studentDTO.StudentClass?.Any() == true)
            {
                student.StudentClass = await Task.WhenAll(studentDTO.StudentClass.Select(async sc => new StudentClass
                {
                    ClassID = sc.ClassID,
                    Class = await _studentClassRepository.GetStudentClassByIdAsync(sc.ClassID, student.StudentID),
                    Student = student
                }));
            }

            if (studentDTO.BehaviorScore?.Any() == true)
            {
                student.BehaviorScore = studentDTO.BehaviorScore.Select(bs => new BehaviorScore
                {
                    Score = bs.Score,
                    Student = student
                }).ToList();
            }
            else
            {
                student.BehaviorScore.Add(new BehaviorScore { Score = 100, Student = student });
            }

            await _studentRepository.CreateStudentAsync(student);
            return new ApiResponse<StudentDTO>(true, ResponseMessages.StudentPostSuccess, studentDTO);
        }
    }
}
