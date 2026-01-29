using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Guid> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
        Task<Comment?> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken = default);
        Task<List<Comment>> GetAllCommentsAsync(CancellationToken cancellationToken = default);
        Task UpdateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
        Task<bool> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default);
        Task<bool> CommentExistsAsync(Guid commentId, CancellationToken cancellationToken = default);
    }
}
