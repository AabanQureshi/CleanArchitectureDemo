using Application.Abstraction.IRepositories;
using Domain.Entites;
using Infrastructure.Persistance;
using Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository(ApplicationDbContext context) : GenericRepository<Student, Guid>(context), IStudentRepository
    {
    }

}
