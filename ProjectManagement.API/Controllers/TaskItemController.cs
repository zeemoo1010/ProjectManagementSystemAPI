using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController(ITaskItemService _taskItemService) : ControllerBase
    {
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto taskItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskItemService.CreateTaskItemAsync(taskItemDto, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-task-by-id")]
        public async Task<IActionResult> GetTaskItemById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _taskItemService.GetTaskItemByIdAsync(id, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetAllTaskItems(CancellationToken cancellationToken)
        {
            var result = await _taskItemService.GetAllTaskItemsAsync(cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        [HttpPut("update-task/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] CreateTaskItemDto _taskItemDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskItemService.UpdateTaskItemAsync(id, _taskItemDto, cancellationToken);
            if (!result.Success)
            {
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
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
