using Microsoft.EntityFrameworkCore;
using Week5.Application.Interfaces;
using Week5.Infrastructure;
using Week5.Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add IRepository<T> with Repository<T>
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add IStudentService with StudentService
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
