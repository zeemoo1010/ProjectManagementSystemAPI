using ProjectManagement.Application.Dto;

namespace ProjectManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<Guid>> CreateUserAsync(CreateUserDto userDto,CancellationToken cancellationToken = default);
        Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid userId,CancellationToken cancellationToken = default);
        Task<BaseResponse<List<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UserExistsAsync(Guid userId,CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> UpdateUserAsync(Guid userId,CreateUserDto userDto,CancellationToken cancellationToken = default);
        Task<BaseResponse<bool>> DeleteUserAsync(Guid userId,CancellationToken cancellationToken = default);
    }
}
