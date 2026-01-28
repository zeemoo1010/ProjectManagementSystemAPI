using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Core.Entities;


namespace ProjectManagement.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.Role)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            // Relationships

            // User → Comments (One-to-Many)
            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // User → ManagedProjects (One-to-Many)
            builder.HasMany(u => u.ManagedProjects)
                   .WithOne(p => p.Manager)
                   .HasForeignKey(p => p.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // User → AssignedTasks (One-to-Many)
            builder.HasMany(u => u.AssignedTasks)
                   .WithOne(t => t.AssignedUser)
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
