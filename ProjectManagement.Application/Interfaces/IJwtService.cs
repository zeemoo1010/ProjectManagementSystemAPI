using ProjectManagement.Application.Dto;

namespace ProjectManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(GenerateTokenRequest request);
    }
}
