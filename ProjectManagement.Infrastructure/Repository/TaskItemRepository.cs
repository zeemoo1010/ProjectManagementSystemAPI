using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Persistence.Context;

namespace ProjectManagement.Infrastructure.Repository
{
    public class TaskItemRepository(ApplicationDbContext _dbContext) : ITaskItemRepository
    {
        public async Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
        {
            await _dbContext.TaskItems.AddAsync(taskItem, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return taskItem;
        }

        public async Task<bool> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            var taskItem = await GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (taskItem == null) return false;

            _dbContext.TaskItems.Remove(taskItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<TaskItem>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.TaskItems.ToListAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == taskItemId, cancellationToken);
        }

        public async Task<bool> TaskItemExistsAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TaskItems.AnyAsync(t => t.Title == title, cancellationToken);
        }

        public async Task UpdateTaskItemAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
        {
            _dbContext.TaskItems.Update(taskItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
