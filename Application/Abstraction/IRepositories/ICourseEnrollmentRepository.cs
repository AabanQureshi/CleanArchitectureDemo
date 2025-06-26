using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.IRepositories
{
    public interface ICourseEnrollmentRepository : IGenericRepository<CourseEnrollment, int>
    {
        Task AddRangeAsync(IEnumerable<CourseEnrollment> entities);
    }
}
