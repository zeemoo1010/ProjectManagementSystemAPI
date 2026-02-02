using ProjectManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Interfaces
{
    public interface ICommentService
    {
        Task<BaseResponse<Guid>> CreateCommentAsync(CreateCommentDto request, CancellationToken cancellationToken = default);
        Task<BaseResponse<CommentDto>> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken = default);
        Task<BaseResponse<List<CommentDto>>> GetAllCommentsAsync(CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UpdateCommentAsync(Guid commentId, CreateCommentDto commentDto, CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default);
    }
}
