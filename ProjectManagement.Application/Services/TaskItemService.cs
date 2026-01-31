using Azure.Core;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class TaskItemService(ITaskItemRepository _taskItemRepository) : ITaskItemService
    {
        public async Task<BaseResponse<Guid>> CreateTaskItemAsync(CreateTaskItemDto request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return BaseResponse<Guid>.FailureResponse("Task item data cannot be null.");
            }
            var existentTaskItem = await _taskItemRepository.TaskItemExistsAsync(request.Title, cancellationToken);
            if (existentTaskItem)
            {
                return BaseResponse<Guid>.FailureResponse("A task item with the same title already exists.");
            }
            var dto = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                DueDate = request.DueDate,
                ProjectId = request.ProjectId,
                AssignedUserId = request.AssignedUserId,

            };
            await _taskItemRepository.CreateTaskItemAsync(dto);
            return BaseResponse<Guid>.SuccessResponse(dto.Id, "Task Item created succesfully");

        }

        public Task<BaseResponse<bool>> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<TaskItemDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TaskItemDto>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> TaskItemExistsAsync(string title, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateTaskItemAsync(Guid taskItemId, CreateTaskItemDto taskItemDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
