using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;


namespace ProjectManagement.Persistence.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure (EntityTypeBuilder <Project> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.ProjectName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.Status)
                   .IsRequired();

            builder.Property(p => p.StartDate)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            builder.Property(p => p.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(150);

            // Relationships

            // Project → Manager (User)
            builder.HasOne(p => p.Manager)
                   .WithMany(u => u.ManagedProjects)
                   .HasForeignKey(p => p.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Project → Tasks (One-to-Many)
            builder.HasMany(p => p.Tasks)
                   .WithOne(t => t.Project)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
