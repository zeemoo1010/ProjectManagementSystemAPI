using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
