using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Contexts;
using StudentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<tblStudent> GetByIdAsync(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<List<tblStudent>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task AddAsync(tblStudent student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(tblStudent student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(tblStudent student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}
