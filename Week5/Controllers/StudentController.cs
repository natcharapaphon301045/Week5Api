using Microsoft.EntityFrameworkCore;
using Week5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Week5.Application.DTOs;
using Week5.Domain;

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
    public async Task<IActionResult> AddStudent([FromBody] StudentCreateDTO studentCreateDto)
    {
        var student = await _studentService.AddStudentAsync(studentCreateDto);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentID }, student);
    }

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
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

