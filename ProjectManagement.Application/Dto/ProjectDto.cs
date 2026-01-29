using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;

        public Guid ManagerId { get; set; } // Foreign Key

        public User Manager { get; set; }

        public HashSet<TaskItem>? Tasks { get; set; } = new HashSet<TaskItem>();

        // Audit Info
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }
}
