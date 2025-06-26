using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(c => c.Description)
                .HasMaxLength(1000);
            
            builder.Property(c => c.StartDate)
                .IsRequired();
            
            builder.Property(c => c.EndDate)
                .IsRequired();
            
            builder.HasMany(c => c.CourseEnrollments)
                .WithOne(ce => ce.Course)
                .HasForeignKey(ce => ce.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
