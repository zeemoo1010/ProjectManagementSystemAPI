using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService _commentService) : ControllerBase
    {
        [HttpPost("create-comment")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _commentService.CreateCommentAsync(request, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("get-comment-by-id")]
        public async Task<IActionResult> GetCommentById(Guid commentId, CancellationToken cancellationToken)
        {
            var result = await _commentService.GetCommentByIdAsync(commentId, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-all-comments")]
        public async Task<IActionResult> GetAllComments(CancellationToken cancellationToken)
        {
            var result = await _commentService.GetAllCommentsAsync(cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("update-comment/{commentId}")]
        public async Task<IActionResult> UpdateComent(Guid id, [FromBody] CreateCommentDto _commentDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _commentService.UpdateCommentAsync(id, _commentDto, cancellationToken);
            if (!result.Success)
            {
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
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
