using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Application.Services
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task<BaseResponse<Guid>> CreateUserAsync(CreateUserDto userDto, CancellationToken cancellationToken = default)
        {
            if(userDto == null)
            {
                return BaseResponse<Guid>.FailureResponse("User data is null.");
            }
            var emailExist = await _userRepository.UserExistByEmailAsync(userDto.Email, cancellationToken);
            if (emailExist)
            {
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
            return BaseResponse<Guid>.SuccessResponse(newUser.Id, "User created successfully.");
        }

        public async Task<BaseResponse<bool>> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (existingUser == null)
            {
                return BaseResponse<bool>.FailureResponse("User not found.");
            }
            var result = await _userRepository.DeleteUserAsync(userId, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "User deleted successfully.");
        }

        public async Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
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
            return BaseResponse<List<UserDto>>.SuccessResponse(userDtos, "Users retrieved successfully.");
        }

        public async Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
            {
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
            return BaseResponse<UserDto>.SuccessResponse(userDto, "User retrieved successfully.");
        }

        public async Task<BaseResponse<bool>> UpdateUserAsync(Guid userId, CreateUserDto userDto, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (existingUser == null)
            {
                return BaseResponse<bool>.FailureResponse("User not found.");
            }
            existingUser.Email = userDto.Email;
            existingUser.FullName = userDto.FullName;
            existingUser.Role = userDto.Role;
            await _userRepository.UpdateUserAsync(existingUser, cancellationToken);
            return BaseResponse<bool>.SuccessResponse(true, "User updated successfully.");
        }

    }
}
