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
    public class CourseRepository(ApplicationDbContext context) : GenericRepository<Course, Guid>(context), ICourseRepository
    {
    }
}
