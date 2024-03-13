using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using CourseService.DAL.Contexts;

namespace CourseService.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _dbContext;
        public CourseRepository(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(tblCourse course)
        {
            await _dbContext.tblCourse.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(tblCourse course)
        {
            _dbContext.tblCourse.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async void Detach(tblCourse course)
        {
            _dbContext.Entry(course).State = EntityState.Detached;
        }

        public async Task<List<tblCourse>> GetAllAsync()
        {
            return await _dbContext.tblCourse.ToListAsync();
        }

        public async Task<tblCourse> GetByIdAsync(int id)
        {
            return await _dbContext.tblCourse.FindAsync(id);
        }

        public async Task UpdateAsync(tblCourse course)
        {
            _dbContext.tblCourse.Update(course);
            await _dbContext.SaveChangesAsync();
        }
    }
}
