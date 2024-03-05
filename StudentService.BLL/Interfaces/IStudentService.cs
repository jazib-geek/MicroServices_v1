using StudentService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int studentId);
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task<StudentDto> UpdateStudentAsync(int studentId, StudentDto studentDto);
        Task DeleteStudentAsync(int studentId);
    }
}
