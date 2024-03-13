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
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentCourseRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<tblStudentCourse> GetByIdAsync(int id)
        {
            return await _dbContext.StudentCourse.FindAsync(id);
        }

        public async Task<List<tblStudentCourse>> GetAllAsync()
        {
            return await _dbContext.StudentCourse.ToListAsync();
        }

        public async Task<string> AddAsync(tblStudentCourse studentcourse)
        {
            var exist = _dbContext.StudentCourse.Any(x => x.StudentId == studentcourse.StudentId && x.CourseId == studentcourse.CourseId);
            if (exist)
            {
                return "duplicate";
            }
            await _dbContext.StudentCourse.AddAsync(studentcourse);
            await _dbContext.SaveChangesAsync();

            return "success";
        }

        public async Task DeleteAsync(tblStudentCourse studentcourse)
        {
            _dbContext.StudentCourse.Remove(studentcourse);
            await _dbContext.SaveChangesAsync();
        }
        public void Detach(tblStudentCourse studentcourse)
        {
            _dbContext.Entry(studentcourse).State = EntityState.Detached;
        }
    }
}
