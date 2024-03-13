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
    }
}
