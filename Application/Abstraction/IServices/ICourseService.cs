using Application.DTOs;
using Domain.Entites;

namespace Application.Abstraction.IServices
{
    public interface ICourseService
    {
        Task<Guid> AddCourseAsync(CourseDTO courseDto);
        Task DeleteCourseAsync(Guid id);
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task UpdateCourseAsync(Guid id, CourseDTO courseDto);
    }
}