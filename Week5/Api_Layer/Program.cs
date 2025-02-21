using Week5.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Week5.Application_Layer.Interfaces;
using Week5.Application_Layer.Services;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
