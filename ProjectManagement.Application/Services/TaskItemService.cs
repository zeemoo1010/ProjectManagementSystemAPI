using Azure.Core;
using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class TaskItemService(ITaskItemRepository _taskItemRepository, ILogger<TaskItemService> _logger) : ITaskItemService
    {
        public async Task<BaseResponse<Guid>> CreateTaskItemAsync(CreateTaskItemDto request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating a new task item with title: {Title}", request.Title);
            if (request == null)
            {
                _logger.LogWarning("CreateTaskItemAsync called with null request.");
                return BaseResponse<Guid>.FailureResponse("Task item data cannot be null.");
            }
            var existentTaskItem = await _taskItemRepository.TaskItemExistsAsync(request.Title, cancellationToken);
            if (existentTaskItem)
            {
                _logger.LogWarning("A task item with the title '{Title}' already exists.", request.Title);
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
            _logger.LogInformation("Task item with title '{Title}' created successfully with ID: {Id}", request.Title, dto.Id);
            return BaseResponse<Guid>.SuccessResponse(dto.Id, "Task Item created succesfully");

        }

        public async Task<BaseResponse<bool>> DeleteTaskItemAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting task item with ID: {Id}", taskItemId);
            var existingTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (existingTaskItem == null)
            {
                _logger.LogWarning("Task item with ID: {Id} not found for deletion.", taskItemId);
                return BaseResponse<bool>.FailureResponse("Task item not found.");
            }
            await _taskItemRepository.DeleteTaskItemAsync(taskItemId, cancellationToken);
            _logger.LogInformation("Task item with ID: {Id} deleted successfully.", taskItemId);
            return BaseResponse<bool>.SuccessResponse(true, "Task item deleted successfully.");
        }

        public async Task<BaseResponse<List<TaskItemDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving all task items.");
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
            _logger.LogInformation("Retrieved {Count} task items successfully.", taskItemDtos.Count);
            return BaseResponse<List<TaskItemDto>>.SuccessResponse(taskItemDtos, "Task items retrieved successfully.");
        }

        public async Task<BaseResponse<TaskItemDto>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving task item with ID: {Id}", taskItemId);
            var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (taskItem == null)
            {
                _logger.LogWarning("Task item with ID: {Id} not found.", taskItemId);
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
            _logger.LogInformation("Task item with ID: {Id} retrieved successfully.", taskItemId);
            return BaseResponse<TaskItemDto>.SuccessResponse(taskItemDto, "Task item retrieved successfully.");
        }


        public async Task<BaseResponse<bool>> UpdateTaskItemAsync(Guid taskItemId, CreateTaskItemDto taskItemDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating task item with ID: {Id}", taskItemId);
            var existingTaskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskItemId, cancellationToken);
            if (existingTaskItem == null)
            {
                _logger.LogWarning("Task item with ID: {Id} not found for update.", taskItemId);
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
            _logger.LogInformation("Task item with ID: {Id} updated successfully.", taskItemId);
            return BaseResponse<bool>.SuccessResponse(true, "Task item updated successfully.");
        }
    }
}
