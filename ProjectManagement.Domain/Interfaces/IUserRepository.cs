using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> UserExistByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> UserExistsByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
