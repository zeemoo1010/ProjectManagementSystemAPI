using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Dto
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus Status { get; set; }

        public Guid ManagerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}
