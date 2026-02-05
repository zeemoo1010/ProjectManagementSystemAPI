using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Services;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Repository;
using ProjectManagement.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 Swagger (THIS IS THE KEY PART)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// 🔹 Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
