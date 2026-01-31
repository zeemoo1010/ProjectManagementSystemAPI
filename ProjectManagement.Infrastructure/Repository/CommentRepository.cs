using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Persistence.Context;

namespace ProjectManagement.Infrastructure.Repository
{
    public class CommentRepository(ApplicationDbContext _dbContext) : ICommentRepository
    {
        public async Task<bool> CommentExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Comments.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Comment> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await GetCommentByIdAsync(commentId, cancellationToken);
            if (comment == null) return false;

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Comment>> GetAllCommentsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Comments.ToListAsync(cancellationToken);
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);
        }

        public async Task UpdateCommentAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
