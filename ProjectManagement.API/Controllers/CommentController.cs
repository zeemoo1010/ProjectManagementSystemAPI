using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService _commentService, ILogger<CommentController> _logger) : ControllerBase
    {
        [HttpPost("create-comment")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateComment: Invalid request model");
                return BadRequest(ModelState);
            }

            var result = await _commentService.CreateCommentAsync(request, cancellationToken);

            if (!result.Success)
            {
                _logger.LogWarning("CreateComment failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            return Ok(result);
        }


        [HttpDelete("delete-comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId, CancellationToken cancellationToken)
        {
            var result = await _commentService.DeleteCommentAsync(commentId, cancellationToken);
            if (!result.Success)
            {
                _logger.LogWarning("DeleteComment failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        //[HttpGet("get-comments-by-task/{taskId}")]
        //public async Task<IActionResult> GetCommentsByTask(Guid taskId, CancellationToken cancellationToken)
        //{
        //    var result = await _commentService.GetCommentsByTaskAsync(taskId, cancellationToken);
        //    if (!result.Success)
        //    {
        //        _logger.LogWarning("GetCommentsByTask failed: {Message}", result.Message);
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result);
        //}
    }
}
