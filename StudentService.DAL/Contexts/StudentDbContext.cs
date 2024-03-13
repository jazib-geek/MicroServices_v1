using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentService.DAL.Entities;

namespace StudentService.DAL.Contexts
{
    public class StudentDbContext : DbContext
    {
        public DbSet<tblStudent> Students { get; set; }
        public DbSet<tblStudentCourse> StudentCourse { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings, relationships, etc. if needed
            modelBuilder.Entity<tblStudent>().ToTable("tblStudent");
            modelBuilder.Entity<tblStudentCourse>().ToTable("tblStudentCourse");
        }
    }
}
