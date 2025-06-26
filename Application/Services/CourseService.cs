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
    public sealed class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Guid> AddCourseAsync(CourseDTO courseDto)
        {
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate
            };
            await _courseRepository.AddAsync(course).ConfigureAwait(false);
            await _courseRepository.SaveChangesAsync().ConfigureAwait(false);

            return course.Id;
        }
        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetAllAsync().ConfigureAwait(false);
        }
        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepository.GetByIdAsync(id).ConfigureAwait(false);
        }
        public async Task UpdateCourseAsync(Guid id, CourseDTO courseDto)
        {
            var course = await _courseRepository.GetByIdAsync(id).ConfigureAwait(false) ?? throw new KeyNotFoundException($"Course with ID {id} not found.");
            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.StartDate = courseDto.StartDate;
            course.EndDate = courseDto.EndDate;
            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id).ConfigureAwait(false) ?? throw new KeyNotFoundException($"Course with ID {id} not found.");
            _courseRepository.Remove(course);
            await _courseRepository.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}
