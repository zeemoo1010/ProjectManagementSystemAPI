using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{

    //https://meet.google.com/rny-xnzw-cjq
    public class ProjectService(IProjectRepository _projectRepository, IUserRepository _userRepository, ILogger<ProjectService> _logger) : IProjectService
    {
        public async Task<BaseResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting CreateProjectAsync for project: {ProjectName}", request.ProjectName);
            if (request == null)
            {
                _logger.LogWarning("CreateProjectAsync failed: Request is null.");
                return BaseResponse<ProjectDto>.FailureResponse("Project data cannot be null.");
            }

            var existingProject = await _projectRepository.ProjectExistsAsync(request.ProjectName, cancellationToken);

            if (existingProject)
            {
                _logger.LogWarning("CreateProjectAsync failed: Project with name '{ProjectName}' already exists.", request.ProjectName);
                return BaseResponse<ProjectDto>.FailureResponse("A project with the same name already exists.");
            }

            // ✅ VALIDATE MANAGER BEFORE SAVING
            var managerExists = await _userRepository.UserExistsByIdAsync(request.ManagerId, cancellationToken);

            if (!managerExists)
            {
                _logger.LogWarning("CreateProjectAsync failed: Manager with ID '{ManagerId}' does not exist.", request.ManagerId);
                return BaseResponse<ProjectDto>.FailureResponse("Selected manager does not exist.");
            }

            // ✅ CREATE ENTITY
            var project = new Project
            {
                Id = Guid.NewGuid(),
                ProjectName = request.ProjectName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                ManagerId = request.ManagerId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            // ✅ SAVE
            var createdProject = await _projectRepository.CreateProjectAsync(project, cancellationToken);

            // ✅ MAP DTO
            var projectDto = new ProjectDto
            {
                Id = createdProject.Id,
                ProjectName = createdProject.ProjectName,
                Description = createdProject.Description,
                StartDate = createdProject.StartDate,
                EndDate = createdProject.EndDate,
                Status = createdProject.Status,
                ManagerId = createdProject.ManagerId,
                CreatedAt = createdProject.CreatedAt,
                CreatedBy = createdProject.CreatedBy
            };
            _logger.LogInformation("CreateProjectAsync completed successfully for project: {ProjectName} with ID: {ProjectId}", projectDto.ProjectName, projectDto.Id);
            return BaseResponse<ProjectDto>.SuccessResponse(projectDto, "Project created successfully.");
        }


        public async Task<BaseResponse<bool>> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting DeleteProjectAsync for project ID: {ProjectId}", projectId);
            var existingProject = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (existingProject == null)
            {
                _logger.LogWarning("DeleteProjectAsync failed: Project with ID '{ProjectId}' not found.", projectId);
                return BaseResponse<bool>.FailureResponse("Project not found.");
            }
            await _projectRepository.DeleteProjectAsync(projectId, cancellationToken);
            _logger.LogInformation("DeleteProjectAsync completed successfully for project ID: {ProjectId}", projectId);
            return BaseResponse<bool>.SuccessResponse(true, "Project deleted successfully.");
        }

        public async Task<BaseResponse<List<ProjectDto>>> GetAllProjectsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting GetAllProjectsAsync");
            var projects = await _projectRepository.GetAllProjectsAsync(cancellationToken);
            var projectDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                ManagerId = p.ManagerId,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy
            }).ToList();
            _logger.LogInformation("GetAllProjectsAsync completed successfully. Total projects retrieved: {ProjectCount}", projectDtos.Count);
            return BaseResponse<List<ProjectDto>>.SuccessResponse(projectDtos);
        }

        public async Task<BaseResponse<ProjectDto>> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting GetProjectByIdAsync for project ID: {ProjectId}", projectId);
            var project = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (project == null)
            {
                _logger.LogWarning("GetProjectByIdAsync failed: Project with ID '{ProjectId}' not found.", projectId);
                return BaseResponse<ProjectDto>.FailureResponse("Project not found.");
            }
            var projectDto = new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                ManagerId = project.ManagerId,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy
            };
            _logger.LogInformation("GetProjectByIdAsync completed successfully for project ID: {ProjectId}", projectId);
            return BaseResponse<ProjectDto>.SuccessResponse(projectDto);
        }

        public async Task<BaseResponse<bool>> UpdateProjectAsync(Guid projectId, ProjectDto projectDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting UpdateProjectAsync for project ID: {ProjectId}", projectId);
            var existingProject = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (existingProject == null)
            {
                _logger.LogWarning("UpdateProjectAsync failed: Project with ID '{ProjectId}' not found.", projectId);
                return BaseResponse<bool>.FailureResponse("Project not found.");
            }
            existingProject.ProjectName = projectDto.ProjectName;
            existingProject.Description = projectDto.Description;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.Status = projectDto.Status;
            existingProject.ManagerId = projectDto.ManagerId;
            existingProject.CreatedAt = projectDto.CreatedAt;
            existingProject.CreatedBy = projectDto.CreatedBy;
            await _projectRepository.UpdateProjectAsync(existingProject, cancellationToken);
            _logger.LogInformation("UpdateProjectAsync completed successfully for project ID: {ProjectId}", projectId);
            return BaseResponse<bool>.SuccessResponse(true, "Project updated successfully.");
        }
    }
}
