﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentService.BLL.Models;
using StudentService.BLL.Interfaces;
using StudentService.DAL.Entities;
using StudentService.DAL.Repositories;

namespace StudentService.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(MapToDto).ToList();
        }

        public async Task<StudentDto> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return student == null ? null : MapToDto(student);
        }

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = MapToEntity(studentDto);
              await _studentRepository.AddAsync(student);
            return MapToDto(student);
        }

        public async Task<StudentDto> UpdateStudentAsync(int studentId, StudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentId);
            if (existingStudent == null)
            {
                throw new Exception($"Student with ID {studentId} not found.");
            }

            var updatedStudent = MapToEntity(studentDto);
            updatedStudent.Id = studentId;

            await _studentRepository.UpdateAsync(updatedStudent);

            return MapToDto(updatedStudent);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentId);
            if (existingStudent == null)
            {
                throw new Exception($"Student with ID {studentId} not found.");
            }

            await _studentRepository.DeleteAsync(existingStudent);
        }

        private static StudentDto MapToDto(tblStudent student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                RollNum = student.RollNum,
                Email = student.Email,
                IsActive = student.IsActive
            };
        }

        private static tblStudent MapToEntity(StudentDto studentDto)
        {
            return new tblStudent
            {
                Id = studentDto.Id,
                Name = studentDto.Name,
                RollNum = studentDto.RollNum,
                Email = studentDto.Email,
                IsActive = studentDto.IsActive
            };
        }
    }

}
