/*using Week5.Application_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Week5.Application_Layer.DTOs;
using Week5.Domain_Layer.Entity;

namespace Week5.Api_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        // Get Major by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajorById(int id)
        {
            var major = await _majorService.GetMajorByIdAsync(id);
            if (major == null)
                return NotFound(new { status = "error", message = $"Major with ID {id} not found." });
            return Ok(major);
        }

        // Get all Majors
        [HttpGet]
        public async Task<IActionResult> GetAllMajors()
        {
            var major = await _majorService.GetAllMajorsAsync();
            return Ok(major);
        }
    }
}
*/