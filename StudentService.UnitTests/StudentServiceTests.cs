using Xunit;
using StudentService.BLL;
using StudentService.DAL.Repositories;
using Moq;
using Shared.Contracts.Models;
using StudentService.DAL.Entities;

namespace StudentService.UnitTests
{
    public class StudentServiceTests
    {
        [Fact]
        public async void GetStudentById_ReturnsCorrectStudent()
        {
            // Arrange
            var mockRepository = new Mock<IStudentRepository>();
            var expectedStudent = new tblStudent { Id = 1, Name = "John Doe", RollNum = "123", Email = "john@example.com", IsActive = true };
            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedStudent);

            var studentService = new StudentService.BLL.Services.StudentService(mockRepository.Object);

            // Act
            var actualStudent = await studentService.GetStudentByIdAsync(1);

            // Assert
            Assert.Equal(expectedStudent.Id, actualStudent.Id);
            Assert.Equal(expectedStudent.Name, actualStudent.Name);
            Assert.Equal(expectedStudent.RollNum, actualStudent.RollNum);
            Assert.Equal(expectedStudent.Email, actualStudent.Email);
            Assert.Equal(expectedStudent.IsActive, actualStudent.IsActive);
        }

        [Fact]
        public async Task CreateStudent_ReturnsNewStudentId()
        {
            // Arrange
            var mockStudentRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService.BLL.Services.StudentService(mockStudentRepository.Object);

            var newStudent = new StudentDto
            {
                Name = "John Doe",
                RollNum = "S12345",
                Email = "john.doe@example.com"
            };

            var expectedStudentId = 1; // Assuming the repository returns the ID of the newly created student

            mockStudentRepository.Setup(repo => repo.AddAsync(It.IsAny<tblStudent>()))
                      .Callback((tblStudent student) =>
                      {
                          student.Id = expectedStudentId;
                      });

            // Act
            var actualStudentId = await studentService.CreateStudentAsync(newStudent);

            // Assert
            Assert.Equal(expectedStudentId, actualStudentId.Id);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsListOfStudents()
        {
            // Arrange
            var mockStudentRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService.BLL.Services.StudentService(mockStudentRepository.Object);

            // Create a list of expected students
            var expectedStudents = new List<StudentDto>
             {
                 new StudentDto { Id = 1, Name = "John Doe", RollNum = "S12345", Email = "john.doe@example.com" },
                 new StudentDto { Id = 2, Name = "Jane Smith", RollNum = "S67890", Email = "jane.smith@example.com" }
                 // Add more expected students as needed
             };

            // Setup the repository mock to return the expected list of students
            mockStudentRepository.Setup(repo => repo.GetAllAsync())
                                 .ReturnsAsync(expectedStudents.Select(student => new tblStudent
                                 {
                                     Id = student.Id,
                                     Name = student.Name,
                                     RollNum = student.RollNum,
                                     Email = student.Email
                                 }).ToList());

            // Act
            var actualStudents = await studentService.GetAllStudentsAsync();

            // Assert
            Assert.Equal(expectedStudents.Count, actualStudents.Count); // Verify that the counts match

            // Verify that each student in the expected list exists in the actual list
            foreach (var expectedStudent in expectedStudents)
            {
                var actualStudent = actualStudents.FirstOrDefault(s => s.Id == expectedStudent.Id);
                Assert.NotNull(actualStudent); // Verify that the student exists in the actual list
                Assert.Equal(expectedStudent.Name, actualStudent.Name); // Verify other properties as needed
                Assert.Equal(expectedStudent.RollNum, actualStudent.RollNum);
                Assert.Equal(expectedStudent.Email, actualStudent.Email);
            }
        }

    }
}
