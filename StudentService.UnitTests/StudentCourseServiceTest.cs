using Moq;
using Shared.Contracts.Models;
using StudentService.BLL.Services;
using StudentService.DAL.Contexts;
using StudentService.DAL.Entities;
using StudentService.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.UnitTests
{
    public class StudentCourseServiceTest
    {
        [Fact]
        public async Task AssignCourseToStudentAsync_WithValidStudentCourse_ReturnsMappedDto()
        {
            // Arrange
            var mockStudentCourseRepository = new Mock<IStudentCourseRepository>();
            var mockDbContext = new Mock<StudentDbContext>();
            var studentCourseService = new StudentCourseService(mockStudentCourseRepository.Object);

            var studentCourseDto = new StudentCourseDto
            {
                StudentId = 1,
                CourseId = 2
                // Add other properties as needed
            };

            var expectedStudentCourseEntity = new tblStudentCourse
            {
                Id = 1,
                StudentId = 1,
                CourseId = 2
                // Add other properties as needed
            };

            // Setup the mock repository to return the expected response
            mockStudentCourseRepository.Setup(repo => repo.AddAsync(It.IsAny<tblStudentCourse>()))
                                       .ReturnsAsync("success");

            // Act
            var actualStudentCourseDto = await studentCourseService.AssignCourseToStudentAsync(studentCourseDto);

            // Assert
            Assert.NotNull(actualStudentCourseDto); // Verify that a student course dto is returned
                                                    // Add more assertions as needed to verify properties of the returned dto
        }

    }
}
