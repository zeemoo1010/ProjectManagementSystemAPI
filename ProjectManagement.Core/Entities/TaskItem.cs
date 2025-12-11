using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        public Guid ProjectId { get; set; }
        public Guid? AssignedUserId { get; set; }

        public Project Project { get; set; }
        public User? AssignedUser { get; set; }

        public HashSet<Comment>? Comments { get; set; } = new HashSet<Comment>();
    }
}