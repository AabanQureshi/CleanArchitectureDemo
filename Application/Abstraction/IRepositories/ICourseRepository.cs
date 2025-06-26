using Domain.Entites;

namespace Application.Abstraction.IRepositories
{
    public interface ICourseRepository : IGenericRepository<Course, Guid>
    {
    }
}
