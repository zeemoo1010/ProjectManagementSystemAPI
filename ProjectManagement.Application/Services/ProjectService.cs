using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{

    //https://meet.google.com/rny-xnzw-cjq
    public class ProjectService(IProjectRepository _projectRepository) : IProjectService
    {
        public async Task<BaseResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto request, CancellationToken cancellationToken = default)
        {
            if(request == null)
            {
                return BaseResponse<ProjectDto>.FailureResponse("Project data cannot be null.");
            }
            var existingProject = await _projectRepository.ProjectExistsAsync(request.ProjectName, cancellationToken);

            if (existingProject)
            {
                return BaseResponse<ProjectDto>.FailureResponse("A project with the same name already exists.");
            }
            var project = new Project
            {
                Id = Guid.NewGuid(),
                ProjectName = request.ProjectName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                ManagerId = request.ManagerId,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy
            };


            var createdProject = await _projectRepository.CreateProjectAsync(project, cancellationToken);

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

            return BaseResponse<ProjectDto>.SuccessResponse(projectDto, "Project created successfully.");

        }

        public async Task<BaseResponse<bool>> DeleteProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (existingProject == null)
            {
                return BaseResponse<bool>.FailureResponse("Project not found.");
            }
            await _projectRepository.DeleteProjectAsync(projectId, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "Project deleted successfully.");
        }

        public async Task<BaseResponse<List<ProjectDto>>> GetAllProjectsAsync(CancellationToken cancellationToken = default)
        {
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
            return BaseResponse<List<ProjectDto>>.SuccessResponse(projectDtos);
        }

        public async Task<BaseResponse<ProjectDto>> GetProjectByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (project == null)
            {
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
            return BaseResponse<ProjectDto>.SuccessResponse(projectDto);
        }

        public async Task<BaseResponse<bool>> UpdateProjectAsync(Guid projectId, ProjectDto projectDto, CancellationToken cancellationToken = default)
        {var existingProject = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);
            if (existingProject == null)
            {
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
            return BaseResponse<bool>.SuccessResponse(true, "Project updated successfully.");
        }
    }
}
