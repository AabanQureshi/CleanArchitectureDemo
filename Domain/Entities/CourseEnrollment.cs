using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class CourseEnrollment
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public CourseEnrollmentStatus Status { get; set; } = CourseEnrollmentStatus.Pending;

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
