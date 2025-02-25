using Week5.Application_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;

namespace Week5.Api_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController: ControllerBase
    {
        private readonly IStudentService.IStudentGetService _studentGetService;
        private readonly IStudentService.IStudentPostService _studentPostService;

        public StudentController(IStudentService.IStudentGetService studentGetService,
                                    IStudentService.IStudentPostService studentPostService)
        {
            _studentGetService = studentGetService;
            _studentPostService = studentPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentGetService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            var student = await _studentGetService.GetStudentByIdAsync(studentId);
            if (student == null) throw new KeyNotFoundException($"Student with ID {studentId} not found.");
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = "error", message = "Invalid data" });
            }

            var result = await _studentPostService.CreateStudentAsync(createDto);

            if (result?.Data == null)
            {
                return BadRequest(new { status = "error", message = "Student not created" });
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = result.Data.StudentID },
                new { status = "success", message = "Student created" });
        }


        [HttpPost("initialize")]
        public async Task<IActionResult> InitializeStudentData()
            {
                var result = await _studentPostService.InitializeStudentDataAsync();
                if (!result.Success) return BadRequest(result.Message);
                return Ok(result.Message);
            }
        
    }
}
