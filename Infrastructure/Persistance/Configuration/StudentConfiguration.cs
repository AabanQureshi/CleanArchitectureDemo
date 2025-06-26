using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            builder.HasMany(s => s.CourseEnrollments)
                .WithOne(ce => ce.Student)
                .HasForeignKey(ce => ce.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
