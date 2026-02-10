using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
