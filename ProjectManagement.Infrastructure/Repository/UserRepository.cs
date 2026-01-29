using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Repository
{
    public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
    {
        public async Task<Guid> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await GetUserByIdAsync(userId, cancellationToken);
            if (user == null) return false;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
