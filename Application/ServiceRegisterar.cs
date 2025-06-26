using Application.Abstraction.IServices;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServiceRegisterar
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            //services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
        }
    }
}
