using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using Week5.Application_Layer.DTOs;

public class StudentListModel : PageModel
{
    private readonly HttpClient _httpClient;


    public StudentListModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();


    public async Task OnGetAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<StudentDTO>>>("https://localhost:5285/api/student");

            if (response != null && response.Success && response.Data != null)
            {
                Students = response.Data.ToList();
            }
            else
            {
                Students = new List<StudentDTO>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching student data: {ex.Message}");
            Students = new List<StudentDTO>();
        }
    }
}
