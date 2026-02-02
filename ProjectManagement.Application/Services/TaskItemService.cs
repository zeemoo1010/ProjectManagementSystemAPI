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

        public async Task<BaseResponse<bool>> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            var existingTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (existingTaskItem == null)
            {
                return BaseResponse<bool>.FailureResponse("Task item not found.");
            }
            await _taskItemRepository.DeleteTaskItemAsync(taskItemId, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "Task item deleted successfully.");
        }

        public async Task<BaseResponse<List<TaskItemDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default)
        {
            var taskItems = await _taskItemRepository.GetAllTaskItemsAsync(cancellationToken);
            var taskItemDtos = taskItems.Select(ti => new TaskItemDto
            {
                Id = ti.Id,
                Title = ti.Title,
                Description = ti.Description,
                Priority = ti.Priority,
                Status = ti.Status,
                CreatedAt = ti.CreatedAt,
                DueDate = ti.DueDate,
                ProjectId = ti.ProjectId,
                AssignedUserId = ti.AssignedUserId
            }).ToList();
            return BaseResponse<List<TaskItemDto>>.SuccessResponse(taskItemDtos, "Task items retrieved successfully.");
        }

        public async Task<BaseResponse<TaskItemDto>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (taskItem == null)
            {
                return BaseResponse<TaskItemDto>.FailureResponse("Task item not found.");
            }
            var taskItemDto = new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Priority = taskItem.Priority,
                Status = taskItem.Status,
                CreatedAt = taskItem.CreatedAt,
                DueDate = taskItem.DueDate,
                ProjectId = taskItem.ProjectId,
                AssignedUserId = taskItem.AssignedUserId
             };
            return BaseResponse<TaskItemDto>.SuccessResponse(taskItemDto, "Task item retrieved successfully.");
        }


        public async Task<BaseResponse<bool>> UpdateTaskItemAsync(Guid taskItemId, CreateTaskItemDto taskItemDto, CancellationToken cancellationToken = default)
        {
            var existingTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (existingTaskItem == null)
            {
                return BaseResponse<bool>.FailureResponse("Task item not found.");
            }
            existingTaskItem.Title = taskItemDto.Title;
            existingTaskItem.Description = taskItemDto.Description;
            existingTaskItem.Priority = taskItemDto.Priority;
            existingTaskItem.Status = taskItemDto.Status;
            existingTaskItem.DueDate = taskItemDto.DueDate;
            existingTaskItem.ProjectId = taskItemDto.ProjectId;
            existingTaskItem.AssignedUserId = taskItemDto.AssignedUserId;
            await _taskItemRepository.UpdateTaskItemAsync(existingTaskItem, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "Task item updated successfully.");
        }
    }
}
