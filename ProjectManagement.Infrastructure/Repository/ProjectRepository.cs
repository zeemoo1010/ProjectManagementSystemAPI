using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Persistence.Context;

namespace ProjectManagement.Infrastructure.Repository
{
    public class ProjectRepository(ApplicationDbContext _dbContext) : IProjectRepository
    {
        public async Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _dbContext.Projects.AddAsync(project, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return project;
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await GetProjectByIdAsync(projectId, cancellationToken);
            if (project == null) return false;

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Projects.ToListAsync(cancellationToken);
        }

        public async Task<Project?> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        }

        public async Task<bool> ProjectExistsAsync(string projectName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Projects.AnyAsync(p => p.ProjectName == projectName, cancellationToken);
        }

        public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
