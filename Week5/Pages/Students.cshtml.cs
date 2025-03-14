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
    public async Task<IActionResult> OnDeleteAsync(int id)
    {
        var response = await _studentService.DeleteStudentAsync(id);
        if (response.Success)
        {
            return new JsonResult(new { success = true });
        }
        return new JsonResult(new { success = false });
    }
    public async Task<IActionResult> OnGetStudentByIdAsync(int id)
    {
        var response = await _studentService.GetStudentByIdAsync(id);
        if (response.Success && response.Data != null)
        {
            // กำหนดค่า Students ถ้าข้อมูลถูกต้อง
            Students = new List<StudentDTO> { response.Data };
            return Page();
        }
        // ถ้าไม่พบข้อมูล หรือเกิดข้อผิดพลาดในการเรียกข้อมูล
        return NotFound();
    }
}
