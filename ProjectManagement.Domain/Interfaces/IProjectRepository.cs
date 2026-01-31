using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project?> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
        Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
        Task<bool> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<bool> ProjectExistsAsync(string projectName, CancellationToken cancellationToken = default);
    }
}
