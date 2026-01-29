using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Dto
{
    public class CreateTaskItemDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; }

        //public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public Guid ProjectId { get; set; }

        public Guid? AssignedUserId { get; set; }
    }
}
