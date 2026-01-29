using ProjectManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectService
    {
        Task<BaseResponse<Guid>> CreateProjectAsync(CreateProjectDto projectDto, CancellationToken cancellationToken = default);
        Task<BaseResponse<ProjectDto>> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<BaseResponse<List<ProjectDto>>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> ProjectExistsAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UpdateProjectAsync(Guid projectId, CreateProjectDto projectDto, CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
