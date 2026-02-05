using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService _projectService, ILogger<ProjectController> _logger) : ControllerBase
    {
        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateProject: Invalid request model");
                return BadRequest(ModelState);
            }
            var result = await _projectService.CreateProjectAsync(projectDto, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("CreateProject failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-projects-by-id")]
        public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _projectService.GetProjectByIdAsync(id, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("GetProjectById failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects(CancellationToken cancellationToken)
        {
            var result = await _projectService.GetAllProjectsAsync(cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("GetAllProjects failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("update-project/{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto _projectDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("UpdateProject: Invalid request model");
                return BadRequest(ModelState);
            }
            var result = await _projectService.UpdateProjectAsync(id, _projectDto, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("UpdateProject failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("delete-project/{id}")]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
        {
            var result = await _projectService.DeleteProjectAsync(id, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("DeleteProject failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
