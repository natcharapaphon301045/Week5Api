using Microsoft.AspNetCore.Mvc;
using Week5.Application_Layer.Interfaces;
using Week5.Application_Layer.DTOs;

namespace Week5.Api_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound(new { status = "error", message = $"Student with ID {id} not found." });

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = "error", message = "Invalid data" });
            }

            var result = await _studentService.CreateStudentAsync(createDto);

            if (result?.Data == null)
            {
                return BadRequest(new { status = "error", message = "Student not created" });
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = result.Data.StudentID },
                new { status = "success", message = "Student created" });
        }

        /*
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializeStudentData()
        {
            var result = await _studentService.InitializeStudentDataAsync();
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
        */
    }
}
