using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; } // Enum instead of string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public HashSet<Project>? ManagedProjects { get; set; } = new HashSet<Project>();
        public HashSet<TaskItem>? AssignedTasks { get; set; } = new HashSet<TaskItem>();
    }

}