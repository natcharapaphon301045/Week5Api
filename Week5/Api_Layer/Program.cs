using Week5.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Week5.Application.Interfaces;
using Week5.Application.Services;
using Week5.Domain.IRepositories;
using Week5.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"Api_Layer"))
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                     .AddEnvironmentVariables();
// ตรวจสอบการโหลดค่า
Console.WriteLine($"✅ Loaded Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");

// Register Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure HTTP Request Pipeline
app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Ensure controllers are mapped

app.Run();
