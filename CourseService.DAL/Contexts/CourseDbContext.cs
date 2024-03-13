using CourseService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService.DAL.Contexts
{
    public class CourseDbContext : DbContext
    {
        public DbSet<tblCourse> tblCourse { get; set; }

        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings, relationships, etc. if needed
            modelBuilder.Entity<tblCourse>().ToTable("tblCourse");
        }
    }
}
