using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Week5.Application_Layer.DTOs;
using Week5.Application_Layer.Services;

public class StudentListModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<StudentListModel> _logger;

    public StudentListModel(HttpClient httpClient, ILogger<StudentListModel> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
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
                _logger.LogWarning("No student data received from API.");
                Students = new List<StudentDTO>();
            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError($"HTTP request error: {httpEx.Message}");
            Students = new List<StudentDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}");
            Students = new List<StudentDTO>();
        }
    }
}