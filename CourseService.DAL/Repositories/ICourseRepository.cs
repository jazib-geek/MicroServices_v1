using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseService.DAL.Entities;

namespace CourseService.DAL.Repositories
{
    public interface ICourseRepository
    {
        Task<tblCourse> GetByIdAsync(int id);
        Task<List<tblCourse>> GetAllAsync();
        Task AddAsync(tblCourse course);
        Task UpdateAsync(tblCourse course);
        Task DeleteAsync(tblCourse course);
        void Detach(tblCourse course);
    }
}
