using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Contexts;
using StudentService.DAL.Repositories;
using StudentService.BLL.Interfaces;
using StudentService.BLL.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add BLL and DAL services
builder.Services.AddScoped<IStudentService, StudentService.BLL.Services.StudentService>();
builder.Services.AddScoped<IStudentCourseService, StudentService.BLL.Services.StudentCourseService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();

// Add DbContext
//builder.Services.AddDbContext<StudentDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StudentService.WebApi")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
