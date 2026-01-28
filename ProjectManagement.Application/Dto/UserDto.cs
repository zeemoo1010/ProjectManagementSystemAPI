using ProjectManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Dto
{
    public class UserDto
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
