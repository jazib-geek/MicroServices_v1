using CourseService.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Contracts.Models;
using CourseService.DAL.Contexts;
using CourseService.DAL.Repositories;
using CourseService.DAL.Entities;

namespace CourseService.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CourseDto> CreateCourseAsync(CourseDto CourseDto)
        {
            var course = MapToEntity(CourseDto);
            await _courseRepository.AddAsync(course);
            return MapToDto(course);
        }

        public async Task DeleteCourseAsync(int CourseId)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(CourseId);
            if (existingCourse == null)
            {
                throw new Exception($"Course with ID {CourseId} not found.");
            }

            await _courseRepository.DeleteAsync(existingCourse);
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(MapToDto).ToList();
        }

        public async Task<CourseDto> GetCourseByIdAsync(int CourseId)
        {
            var Course = await _courseRepository.GetByIdAsync(CourseId);
            return Course == null ? null : MapToDto(Course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int CourseId, CourseDto CourseDto)
        {
            var existingcourse = await _courseRepository.GetByIdAsync(CourseId);
            if (existingcourse == null)
            {
                throw new Exception($"Course with ID {CourseId} not found.");
            }

            // Update the properties of the existing student entity with the new values
            existingcourse.Title = CourseDto.Title;
            existingcourse.Description = CourseDto.Description;
            existingcourse.CreditHours = CourseDto.CreditHours;

            // Detach the entity from the DbContext to prevent tracking conflicts
            _courseRepository.Detach(existingcourse);

            // Save the changes to the database
            await _courseRepository.UpdateAsync(existingcourse);

            return MapToDto(existingcourse);
        }

        private static CourseDto MapToDto(tblCourse course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreditHours = course.CreditHours
            };
        }

        private static tblCourse MapToEntity(CourseDto CourseDto)
        {
            return new tblCourse
            {
                Id = CourseDto.Id,
                Title = CourseDto.Title,
                Description = CourseDto.Description,
                CreditHours = CourseDto.CreditHours
            };
        }
    }
}
