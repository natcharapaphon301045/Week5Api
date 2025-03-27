using Microsoft.EntityFrameworkCore;
using Week5.Application_Layer.Interfaces;
using Week5.Application_Layer.Services;
using Week5.Domain_Layer.IRepositories;
using Week5.Infrastructure_Layer.Persistence;
using Week5.Infrastructure_Layer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IBehaviorScoreRepository, BehaviorScoreRepository>();
builder.Services.AddScoped<IStudentClassRepository, StudentClassRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();

var app = builder.Build();

// 🔹 Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Default Redirect to /Students
app.MapGet("/", context =>
{
    context.Response.Redirect("/students");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
//test change