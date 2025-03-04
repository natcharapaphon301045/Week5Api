using Microsoft.EntityFrameworkCore;
using Week5.Infrastructure_Layer.Persistence;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Repositories;
using Week5.Application_Layer.Interfaces;
using Week5.Application_Layer.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Database Context
builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Register Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IBehaviorScoreRepository, BehaviorScoreRepository>();
builder.Services.AddScoped<IStudentClassRepository, StudentClassRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();

// 🔹 Register Services
builder.Services.AddScoped<IStudentService, StudentService>();

// 🔹 Enable CORS (Allow Any Origin for Testing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// 🔹 Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 🔹 Enable Middleware
app.UseRouting();
app.UseCors("AllowAll"); // ✅ Enable CORS
app.UseAuthorization();

// 🔹 Map Endpoints
app.MapControllers();
app.Run();
