using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Week5.Application_Layer.DTOs;
using Week5.Application_Layer.Interfaces;

public class StudentsModel : PageModel
{
    private readonly IStudentService _studentService;

    public StudentsModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();

    public async Task OnGetAsync()
    {
        var response = await _studentService.GetAllStudentsAsync();
        if (response.Success)
        {
            Students = response.Data ?? new List<StudentDTO>();
        }
        else
        {
            Students = new List<StudentDTO>();
        }
    }

    public async Task<IActionResult> OnGetStudentByIdAsync(int id)
    {
        var response = await _studentService.GetStudentByIdAsync(id);
        if (response.Success && response.Data != null)
        {
            Students = new List<StudentDTO> { response.Data };
            return Page();
        }
        return NotFound();
    }

    public async Task<IActionResult> OnPostCreateStudentAsync(StudentDTO studentDTO)
    {
        var response = await _studentService.CreateStudentAsync(studentDTO);
        if (response.Success)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, response.Message);
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateStudentAsync(StudentDTO studentDTO)
    {
        var response = await _studentService.UpdateStudentAsync(studentDTO.StudentID, studentDTO);
        if (response.Success)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, response.Message);
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteStudentAsync(int studentId)
    {
        var response = await _studentService.DeleteStudentAsync(studentId);
        if (response.Success)
        {
            return RedirectToPage();
        }
        ModelState.AddModelError(string.Empty, response.Message);
        return Page();
    }
}

