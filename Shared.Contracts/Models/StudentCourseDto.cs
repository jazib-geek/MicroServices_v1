using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Models
{
    public class StudentCourseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int CourseId { get; set; }
    }
}
