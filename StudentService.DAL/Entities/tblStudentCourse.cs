using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.Entities
{
    public class tblStudentCourse
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int CourseId { get; set; }
    }
}
