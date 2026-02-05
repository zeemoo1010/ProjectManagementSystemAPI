using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController(ITaskItemService _taskItemService, ILogger<TaskItemController> _logger) : ControllerBase
    {
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto taskItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateTask: Invalid request model");
                return BadRequest(ModelState);
            }
            var result = await _taskItemService.CreateTaskItemAsync(taskItemDto, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("CreateTask failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        //[HttpGet("get-tasks-by-project-id")]
        //public async Task<IActionResult> GetTaskItemByProjectId(Guid projectId, CancellationToken cancellationToken)
        //{
        //    var result = await _taskItemService.GetTaskItemByProjectIdAsync(projectId, cancellationToken);
        //    if (!result.Success)
        //    {
        //        _logger.LogWarning("GetTasksByProjectId failed: {Message}", result.Message);
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result);
        //}

        [HttpGet("get-task-by-id")]
        public async Task<IActionResult> GetTaskItemById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _taskItemService.GetTaskItemByIdAsync(id, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("GetTaskById failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        //[HttpGet("get-tasks-by-user-id")]
        //public async Task<IActionResult> GetTTaskItemByUserId(Guid userId, CancellationToken cancellationToken)
        //{
        //    var result = await _taskItemService.GetTaskItemByUserIdAsync(userId, cancellationToken);
        //    if (!result.Success)
        //    {
        //        _logger.LogWarning("GetTasksByUserId failed: {Message}", result.Message);
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result);
        //}

        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetAllTaskItems(CancellationToken cancellationToken)
        {
            var result = await _taskItemService.GetAllTaskItemsAsync(cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("GetAllTasks failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        [HttpPut("update-task/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] CreateTaskItemDto _taskItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("UpdateTask: Invalid request model");
                return BadRequest(ModelState);
            }
            var result = await _taskItemService.UpdateTaskItemAsync(id, _taskItemDto, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("UpdateTask failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("delete-task/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
        {
            var result = await _taskItemService.DeleteTaskItemAsync(id, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("DeleteTask failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
