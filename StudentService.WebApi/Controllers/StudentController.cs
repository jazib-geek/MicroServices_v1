using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentService.BLL.Interfaces;
using Shared.Contracts.Models;

namespace StudentService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IStudentCourseService _studentCourseService;

        public StudentsController(IStudentService studentService, IStudentCourseService studentCourseService)
        {
            _studentService = studentService;
            _studentCourseService = studentCourseService;
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

        #region Student-Course 
        [HttpPost("students/{studentId}/courses/{courseId}")]

        public async Task<IActionResult> AssignCourseToStudent(int studentId, int courseId)
        {
            try
            {
                // Call Course API to retrieve course information
                var courseApiUrl = "http://localhost:5109/api/course/" + courseId;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(courseApiUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var courseJsonString = await response.Content.ReadAsStringAsync();
                            var course = JsonConvert.DeserializeObject<CourseDto>(courseJsonString);

                            // If course is successfully retrieved, assign it to the student
                            StudentCourseDto result = await _studentCourseService.AssignCourseToStudentAsync(
                                 new StudentCourseDto() { StudentId = studentId, CourseId = courseId }
                                 );

                            string msg = result.Id > 0 ? "Course assigned successfully" : "Course already assigned to this student";

                            return Ok(msg);
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Failed to retrieve course information from Course service");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //public async Task<IActionResult> AssignCourseToStudent(int studentId, int courseId)
        //{
        //    try
        //    {
        //        // Call method from StudentCourseRepository to assign course to student
        //        StudentCourseDto model = new StudentCourseDto() { StudentId = studentId , CourseId = courseId };
        //        await _studentCourseService.AssignCourseToStudentAsync(model);

        //        return Ok("Course assigned successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        #endregion
    }
}
