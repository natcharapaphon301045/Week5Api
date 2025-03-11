using Microsoft.EntityFrameworkCore;
using Week5.Infrastructure_Layer.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Week5DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/", context =>
{
    context.Response.Redirect("/yourpage");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();