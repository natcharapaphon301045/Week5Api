using Week5.Infrastructure_Layer.Persistence;
using Microsoft.EntityFrameworkCore;
using Week5.Application_Layer.Interfaces;
using Week5.Application_Layer.Services;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();


builder.Services.AddScoped<IStudentService, GetService>();
builder.Services.AddScoped<IStudentService, PostService>();

/*IStudentService.IStudentGetService*/
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
