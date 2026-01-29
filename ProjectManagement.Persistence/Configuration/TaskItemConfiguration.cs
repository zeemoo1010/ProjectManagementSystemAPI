using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;


namespace ProjectManagement.Persistence.Configuration
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.DueDate)
                   .IsRequired(false);

            // Relationships

            // Task → Project (Many-to-One)
            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Task → Assigned User (Many-to-One, Optional)
            builder.HasOne(t => t.AssignedUser)
                   .WithMany(u => u.AssignedTasks)
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Task → Comments (One-to-Many)
            builder.HasMany(t => t.Comments)
                   .WithOne(c => c.TaskItem)
                   .HasForeignKey(c => c.TaskItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
