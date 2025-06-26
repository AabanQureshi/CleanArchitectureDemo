using Application.Abstraction.IRepositories;
using Domain.Entites;
using Infrastructure.Persistance;
using Infrastructure.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CourseEnrollmentRepository(ApplicationDbContext context) : GenericRepository<CourseEnrollment, int>(context), ICourseEnrollmentRepository
    {

        public async Task AddRangeAsync(IEnumerable<CourseEnrollment> entities)
        {
            await _dbSet.AddRangeAsync(entities).ConfigureAwait(false);
        }
    }
}
