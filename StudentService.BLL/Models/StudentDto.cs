using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.BLL.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? RollNum { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
