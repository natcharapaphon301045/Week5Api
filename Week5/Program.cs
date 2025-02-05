using Microsoft.EntityFrameworkCore;
using Week5.Application.Interfaces;
using Week5.Application.Services;
using Week5.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// กำหนด connection string ของฐานข้อมูลจาก appsettings.json
builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentService, StudentService>();


// เพิ่มบริการ Controller
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Map Controllers
app.MapControllers();

app.Run();
