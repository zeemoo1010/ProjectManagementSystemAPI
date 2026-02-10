using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class UserService(IUserRepository _userRepository, ILogger<UserService> _logger) : IUserService
    {
        public async Task<BaseResponse<Guid>> CreateUserAsync(CreateUserDto userDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating a new user with email: {Email}", userDto.Email);
            if (userDto == null)
            {
                _logger.LogWarning("CreateUserAsync can not be null.");
                return BaseResponse<Guid>.FailureResponse("User data is null.");
            }
            var emailExist = await _userRepository.UserExistByEmailAsync(userDto.Email, cancellationToken);
            if (emailExist)
            {
                _logger.LogWarning("Email {Email} already exists.", userDto.Email);
                return BaseResponse<Guid>.FailureResponse("Email already exists.");
            }
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                FullName = userDto.FullName,
                Role = userDto.Role,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.CreateUserAsync(newUser, cancellationToken);
            _logger.LogInformation("User with email {Email} created successfully with ID: {UserId}", userDto.Email, newUser.Id);
            return BaseResponse<Guid>.SuccessResponse(newUser.Id, "User created successfully.");
        }

        public async Task<BaseResponse<bool>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", userId);
            var existingUser = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                return BaseResponse<bool>.FailureResponse("User not found.");
            }
            var result = await _userRepository.DeleteUserAsync(userId, cancellationToken);
            _logger.LogInformation("User with ID {UserId} deleted successfully.", userId);
            return BaseResponse<bool>.SuccessResponse(true, "User deleted successfully.");
        }

        public async Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving all users.");
            var users = await _userRepository.GetAllUsersAsync(cancellationToken);
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                });
            }
            _logger.LogInformation("Retrieved {UserCount} users successfully.", userDtos.Count);
            return BaseResponse<List<UserDto>>.SuccessResponse(userDtos, "Users retrieved successfully.");
        }

        public async Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving user with ID: {UserId}", userId);
            var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                return BaseResponse<UserDto>.FailureResponse("User not found.");
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
            _logger.LogInformation("User with ID {UserId} retrieved successfully.", userId);
            return BaseResponse<UserDto>.SuccessResponse(userDto, "User retrieved successfully.");
        }

        public async Task<BaseResponse<bool>> UpdateUserAsync(Guid userId, CreateUserDto userDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", userId);
            var existingUser = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                return BaseResponse<bool>.FailureResponse("User not found.");
            }
            existingUser.Email = userDto.Email;
            existingUser.FullName = userDto.FullName;
            existingUser.Role = userDto.Role;
            await _userRepository.UpdateUserAsync(existingUser, cancellationToken);
            _logger.LogInformation("User with ID {UserId} updated successfully.", userId);
            return BaseResponse<bool>.SuccessResponse(true, "User updated successfully.");
        }

    }
}
