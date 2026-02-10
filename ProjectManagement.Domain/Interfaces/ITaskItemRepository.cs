using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
        Task<TaskItem?> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default);
        Task<List<TaskItem>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default);
        Task UpdateTaskItemAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default);
        Task<bool> TaskItemExistsAsync(string title, CancellationToken cancellationToken = default);
    }
}
