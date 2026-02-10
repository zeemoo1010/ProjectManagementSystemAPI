using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
        {
            if (!Enum.TryParse<UserRole>(request.Role, true, out var parsedRole))
            {
                return BadRequest("Invalid role");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Role = parsedRole
            };

            var token = _jwtService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }
}
