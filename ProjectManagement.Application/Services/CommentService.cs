using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class CommentService(ICommentRepository _commentRepository) : ICommentService
    {
        public async Task<BaseResponse<bool>> CommentExistsAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var exists = await _commentRepository.CommentExistsAsync(commentId, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(exists);
        }

        public async Task<BaseResponse<Guid>> CreateCommentAsync(CreateCommentDto request, CancellationToken cancellationToken = default)
        {
                if (request == null)
                {
                    return BaseResponse<Guid>.FailureResponse("Comment cannot be null");
                }
           
            var existingComment = await _commentRepository.CommentExistsAsync(request.UserId, cancellationToken);
            if (existingComment)
            {
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
            return BaseResponse<Guid>.SuccessResponse(dto.Id, "Comment created successfully.");
        }
        
           

        public async Task<BaseResponse<bool>> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var existingComment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
                        if (existingComment == null)
            {
                return BaseResponse<bool>.FailureResponse("Comment not found.");
            }
            await _commentRepository.DeleteCommentAsync(commentId, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "Comment deleted successfully.");
        }

        public async Task<BaseResponse<List<CommentDto>>> GetAllCommentsAsync(CancellationToken cancellationToken = default)
        {
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
            return BaseResponse<List<CommentDto>>.SuccessResponse(commentDtos);
        }

        public async Task<BaseResponse<CommentDto>> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
            if (comment == null)
            {
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
            return BaseResponse<CommentDto>.SuccessResponse(commentDto);
        }

        public async Task<BaseResponse<bool>> UpdateCommentAsync(Guid commentId, CreateCommentDto commentDto, CancellationToken cancellationToken = default)
        {
            var updateComment = await _commentRepository.GetCommentByIdAsync(commentId, cancellationToken);
            if(updateComment == null)
            {
                return BaseResponse<bool>.FailureResponse("Comment not found.");
            }
            updateComment.Message = commentDto.Message;
            updateComment.CreatedAt = commentDto.CreatedAt;
            updateComment.TaskItemId = commentDto.TaskItemId;
            updateComment.UserId = commentDto.UserId;
            updateComment.TaskItem = commentDto.TaskItem;
            updateComment.User = commentDto.User;
            await _commentRepository.UpdateCommentAsync(updateComment, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "Comment updated successfully.");
        }
    }
}
