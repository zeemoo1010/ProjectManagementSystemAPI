using System;
using ProjectManagement.Core.Entities;

namespace ProjectManagement.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid TaskItemId { get; set; }
        public Guid UserId { get; set; }

        public TaskItem TaskItem { get; set; }
        public User User { get; set; }
    }
}