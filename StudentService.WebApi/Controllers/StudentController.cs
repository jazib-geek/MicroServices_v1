using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.BLL.Interfaces;
using StudentService.BLL.Models;

namespace StudentService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentDto studentDto)
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            await _studentService.UpdateStudentAsync(id, studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
