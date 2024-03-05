using StudentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.Repositories
{
    public interface IStudentRepository
    {
        Task<tblStudent> GetByIdAsync(int id);
        Task<List<tblStudent>> GetAllAsync();
        Task AddAsync(tblStudent student);
        Task UpdateAsync(tblStudent student);
        Task DeleteAsync(tblStudent student);
    }
}
