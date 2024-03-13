using Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.BLL.Interfaces
{
    public interface IStudentCourseService
    {
        Task<List<StudentCourseDto>> GetAllStudentCoursesAsync();
        Task<StudentCourseDto> GetStudentCourseByIdAsync(int id);
        Task<StudentCourseDto> AssignCourseToStudentAsync(StudentCourseDto studentDto);
        Task DeleteStudentCourseAsync(int studentId);
    }
}
