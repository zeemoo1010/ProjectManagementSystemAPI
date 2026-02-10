using ProjectManagement.Application.Dto;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectService
    {
        Task<BaseResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto request, CancellationToken cancellationToken = default);
        Task<BaseResponse<ProjectDto>> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<BaseResponse<List<ProjectDto>>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UpdateProjectAsync(Guid projectId, ProjectDto projectDto, CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
