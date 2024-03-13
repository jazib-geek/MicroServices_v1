using Microsoft.EntityFrameworkCore;
using CourseService.DAL.Contexts;
using CourseService.DAL.Repositories;
using CourseService.BLL.Interfaces;
using CourseService.BLL.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add BLL and DAL services
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService.BLL.Services.CourseService>();

// Add DbContext

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CourseService.WebApi")));

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
