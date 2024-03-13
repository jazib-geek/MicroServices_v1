using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.Models;
using StudentService.BLL.Interfaces;
using StudentService.DAL.Entities;
using StudentService.DAL.Repositories;

namespace StudentService.BLL.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IStudentCourseRepository _studentCourseRepository;

        public StudentCourseService(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }

        public async Task<List<StudentCourseDto>> GetAllStudentCoursesAsync()
        {
            var studentCourses = await _studentCourseRepository.GetAllAsync();
            return studentCourses.Select(MapToDto).ToList();
        }

        public async Task<StudentCourseDto> GetStudentCourseByIdAsync(int id)
        {
            var student = await _studentCourseRepository.GetByIdAsync(id);
            return student == null ? null : MapToDto(student);
        }

        public async Task<StudentCourseDto> AssignCourseToStudentAsync(StudentCourseDto StudentCourseDto)
        {
            var student = MapToEntity(StudentCourseDto);
            string response = await _studentCourseRepository.AddAsync(student);
            if (response == "duplicate")
            {
                return MapToDto(new tblStudentCourse() { Id = 0 });
            }
            return MapToDto(student);
        }


        public async Task DeleteStudentCourseAsync(int studentId)
        {
            var existingStudent = await _studentCourseRepository.GetByIdAsync(studentId);
            if (existingStudent == null)
            {
                throw new Exception($"Student with ID {studentId} not found.");
            }

            await _studentCourseRepository.DeleteAsync(existingStudent);
        }

        private static StudentCourseDto MapToDto(tblStudentCourse studentCourse)
        {
            return new StudentCourseDto
            {
                Id = studentCourse.Id,
                StudentId = studentCourse.StudentId,
                CourseId = studentCourse.CourseId,
            };
        }

        private static tblStudentCourse MapToEntity(StudentCourseDto studentCourseDto)
        {
            return new tblStudentCourse
            {
                Id = studentCourseDto.Id,
                StudentId = studentCourseDto.StudentId,
                CourseId = studentCourseDto.CourseId,
            };
        }


    }

}
