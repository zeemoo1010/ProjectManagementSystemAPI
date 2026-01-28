using ProjectManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Dto
{
    public class CommentDto
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
