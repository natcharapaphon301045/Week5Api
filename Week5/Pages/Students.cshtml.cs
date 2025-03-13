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

    public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();  // กำหนดค่าเริ่มต้น

    public async Task OnGetAsync()
    {
        var response = await _studentService.GetAllStudentsAsync();
        if (response.Success)
        {
            Students = response.Data ?? new List<StudentDTO>();
        }
        else
        {
            Students = new List<StudentDTO>();  // หากไม่มีข้อมูลก็ให้เป็นค่าว่าง
        }
    }

    public async Task<IActionResult> OnDeleteAsync(int id)
    {
        var response = await _studentService.DeleteStudentAsync(id);
        if (response.Success)
        {
            // เมื่อการลบสำเร็จ ให้ส่งผลลัพธ์แบบ JSON พร้อมค่าความสำเร็จ
            return new JsonResult(new { success = true });
        }
        return new JsonResult(new { success = false });
    }
}
