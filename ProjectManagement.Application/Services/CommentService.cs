using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class CommentService(ICommentRepository _commentRepository, ILogger<CommentService> _logger) : ICommentService
    {
        public async Task<BaseResponse<Guid>> CreateCommentAsync(CreateCommentDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("CreateComment started");
            if (request == null)
            {
                _logger.LogError("CreateComment failed: request is null");
                return BaseResponse<Guid>.FailureResponse("Comment cannot be null");
            }

            var existingComment = await _commentRepository.CommentExistsAsync(request.UserId, cancellationToken);
            if (existingComment)
            {
                _logger.LogWarning("CreateComment failed: Comment already exists for user {UserId}", request.UserId);
                return BaseResponse<Guid>.FailureResponse("Comment already exists.");
            }

            var dto = new Comment
            {
                Message = request.Message,
                CreatedAt = request.CreatedAt,
                TaskItemId = request.TaskItemId,
                UserId = request.UserId,
                TaskItem = request.TaskItem,
                User = request.User
            };
            var commentId = await _commentRepository.CreateCommentAsync(dto, cancellationToken);
            _logger.LogInformation("CreateComment succeeded: Comment {CommentId} created", commentId);
            return BaseResponse<Guid>.SuccessResponse(dto.Id, "Comment created successfully.");
        }



        public async Task<BaseResponse<bool>> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DeleteComment started for CommentId: {CommentId}", commentId);
            var existingComment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
            if (existingComment == null)
            {
                _logger.LogWarning("DeleteComment failed: Comment with id {CommentId} not found", commentId);
                return BaseResponse<bool>.FailureResponse("Comment not found.");
            }
            await _commentRepository.DeleteCommentAsync(commentId, cancellationToken);
            _logger.LogInformation("DeleteComment succeeded for CommentId: {CommentId}", commentId);
            return BaseResponse<bool>.SuccessResponse(true, "Comment deleted successfully.");
        }

        public async Task<BaseResponse<List<CommentDto>>> GetAllCommentsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("GetAllComments started");
            var comment = await _commentRepository.GetAllCommentsAsync(cancellationToken);
            var commentDtos = comment.Select(c => new CommentDto
            {
                Id = c.Id,
                Message = c.Message,
                CreatedAt = c.CreatedAt,
                TaskItemId = c.TaskItemId,
                UserId = c.UserId,
                TaskItem = c.TaskItem,
                User = c.User
            }).ToList();
            _logger.LogInformation("GetAllComments succeeded: {Count} comments retrieved", commentDtos.Count);
            return BaseResponse<List<CommentDto>>.SuccessResponse(commentDtos);
        }

        public async Task<BaseResponse<CommentDto>> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("GetCommentById started for CommentId: {CommentId}", commentId);
            var comment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
            if (comment == null)
            {
                _logger.LogWarning("GetCommentById failed: Comment with id {CommentId} not found", commentId);
                return BaseResponse<CommentDto>.FailureResponse("Comment not found.");
            }
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                Message = comment.Message,
                CreatedAt = comment.CreatedAt,
                TaskItemId = comment.TaskItemId,
                UserId = comment.UserId,
                TaskItem = comment.TaskItem,
                User = comment.User
            };
            _logger.LogInformation("GetCommentById succeeded for CommentId: {CommentId}", commentId);
            return BaseResponse<CommentDto>.SuccessResponse(commentDto);
        }

        public async Task<BaseResponse<bool>> UpdateCommentAsync(Guid commentId, CreateCommentDto commentDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("UpdateComment started for CommentId: {CommentId}", commentId);
            var updateComment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
            if (updateComment == null)
            {
                _logger.LogWarning("UpdateComment failed: Comment with id {CommentId} not found", commentId);
                return BaseResponse<bool>.FailureResponse("Comment not found.");
            }
            updateComment.Message = commentDto.Message;
            updateComment.CreatedAt = commentDto.CreatedAt;
            updateComment.TaskItemId = commentDto.TaskItemId;
            updateComment.UserId = commentDto.UserId;
            updateComment.TaskItem = commentDto.TaskItem;
            updateComment.User = commentDto.User;
            await _commentRepository.UpdateCommentAsync(updateComment, cancellationToken);
            _logger.LogInformation("UpdateComment succeeded for CommentId: {CommentId}", commentId);
            return BaseResponse<bool>.SuccessResponse(true, "Comment updated successfully.");
        }
    }
}
