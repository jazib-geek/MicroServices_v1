using StudentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.Repositories
{
    public interface IStudentCourseRepository
    {
        Task<tblStudentCourse> GetByIdAsync(int id);
        Task<List<tblStudentCourse>> GetAllAsync();
        Task<string> AddAsync(tblStudentCourse student);
        Task DeleteAsync(tblStudentCourse student);
        void Detach(tblStudentCourse student);
    }
}
