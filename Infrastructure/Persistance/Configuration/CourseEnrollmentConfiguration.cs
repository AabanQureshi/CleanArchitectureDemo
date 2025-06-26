using Domain.Entites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
        {
            builder.HasKey(ce => ce.Id);
            
            builder.Property(ce => ce.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(ce => ce.EnrollmentDate)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ce => ce.Status)
                .IsRequired()
                .HasDefaultValue(CourseEnrollmentStatus.Pending)
                .HasConversion<string>();
            
            builder.HasOne(cw => cw.Student)
                .WithMany(se => se.CourseEnrollments)
                .HasForeignKey(ce => ce.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ce => ce.Course)
                .WithMany(co => co.CourseEnrollments)
                .HasForeignKey(ce => ce.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
