using Week5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Week5.Application.DTOs;
using Week5.Domain;

namespace Week5.Adapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        /*
        public async Task<IActionResult> AddStudent([FromBody] StudentCreateDTO studentCreateDto)
        {

            if (studentCreateDto == null)
            {
                return BadRequest("Student data is null.");
            }

            var response = await _studentService.AddStudentAsync(studentCreateDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var student = response.Data;
            if (student == null)
            {
                return StatusCode(500, "An error occurred while creating the student.");
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentID }, "Create successfully");
        }
        */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentUpdateDTO updateStudentDto)
        {
            if (id != updateStudentDto.StudentID)
            {
                return BadRequest();
            }
            var student = await _studentService.UpdateStudentAsync(id, updateStudentDto);
            if (student == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

