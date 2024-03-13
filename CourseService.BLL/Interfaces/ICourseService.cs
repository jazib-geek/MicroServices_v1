using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Contracts.Models;

namespace CourseService.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int CourseId);
        Task<CourseDto> CreateCourseAsync(CourseDto CourseDto);
        Task<CourseDto> UpdateCourseAsync(int CourseId, CourseDto CourseDto);
        Task DeleteCourseAsync(int CourseId);
    }
}
