using System;
using System.Collections.Generic;

namespace ProjectManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        // Relationships
        public HashSet<Project>? ManagedProjects { get; set; } = new HashSet<Project>();
        public HashSet<TaskItem>? AssignedTasks { get; set; } = new HashSet<TaskItem>();
    }

}