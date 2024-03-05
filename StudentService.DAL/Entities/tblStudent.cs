using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.Entities
{
    public class tblStudent
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public string? Name { get; set; }
        public string? RollNum { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
