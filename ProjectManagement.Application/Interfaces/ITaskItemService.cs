using ProjectManagement.Application.Dto;

namespace ProjectManagement.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<BaseResponse<Guid>> CreateTaskItemAsync(CreateTaskItemDto request, CancellationToken cancellationToken = default);
        Task<BaseResponse<TaskItemDto>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default);
        Task<BaseResponse<List<TaskItemDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UpdateTaskItemAsync(Guid taskItemId, CreateTaskItemDto taskItemDto, CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default);
    }
}
