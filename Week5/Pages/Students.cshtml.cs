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

    public IEnumerable<StudentDTO> Students { get; set; } = new List<StudentDTO>();  // ��˹�����������

    public async Task OnGetAsync()
    {
        var response = await _studentService.GetAllStudentsAsync();
        if (response.Success)
        {
            Students = response.Data ?? new List<StudentDTO>();
        }
        else
        {
            Students = new List<StudentDTO>();  // �ҡ����բ����š�����繤����ҧ
        }
    }

    public async Task<IActionResult> OnDeleteAsync(int id)
    {
        var response = await _studentService.DeleteStudentAsync(id);
        if (response.Success)
        {
            // ����͡��ź����� ����觼��Ѿ��Ẻ JSON �������Ҥ��������
            return new JsonResult(new { success = true });
        }
        return new JsonResult(new { success = false });
    }
}
