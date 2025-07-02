using Application.DTOs;
using Domain.Entites;
using Shared.Result;

namespace Application.Abstraction.IServices
{
    public interface IStudentService
    {
        Task<Guid> AddStudentAsync(StudentDTO dTO);
        Task DeleteStudentAsync(Guid id);
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Result> UpdateStudentAsync(Guid id, StudentDTO dTO);
    }
}