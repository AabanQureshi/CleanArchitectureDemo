using Application.Abstraction.IRepositories;
using Application.Abstraction.IServices;
using Application.DTOs;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;

        public StudentService(IStudentRepository studentRepository, ICourseEnrollmentRepository courseEnrollmentRepository)
        {
            _studentRepository = studentRepository;
            _courseEnrollmentRepository = courseEnrollmentRepository;
        }

        public async Task<Guid> AddStudentAsync(StudentDTO dTO)
        {
            var entity = new Student
            {
                Id = Guid.NewGuid(),
                Name = dTO.Name,
                Email = dTO.Email,
                DateOfBirth = dTO.DateOfBirth
            };

            var courseEnrollments = dTO.CourseIds.Select(courseId => new CourseEnrollment
            {
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                StudentId = entity.Id,
            }).ToList();

            var task1 = _courseEnrollmentRepository.AddRangeAsync(courseEnrollments);
            var task2 = _studentRepository.AddAsync(entity);

            await Task.WhenAll(task1, task2).ConfigureAwait(false);

            await _studentRepository.SaveChangesAsync().ConfigureAwait(false);
            return entity.Id;
        }
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateStudentAsync(Guid id, StudentDTO dTO)
        {
            var student = await _studentRepository.GetByIdAsync(id).ConfigureAwait(false) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
            student.Name = dTO.Name;
            student.Email = dTO.Email;
            student.DateOfBirth = dTO.DateOfBirth;
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id).ConfigureAwait(false) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
            _studentRepository.Remove(student);
            await _studentRepository.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
