using Week5.Application_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer;

namespace Week5.Api_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

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

        [HttpPost("initialize")]
        public async Task<IActionResult> InitializeStudentData()
        {
            var result = await _studentService.InitializeStudentDataAsync();
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
