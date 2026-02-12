using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IJwtService _jwtService) : ControllerBase
    {
        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
        {
            try
            {
                var token = _jwtService.GenerateToken(request);
                return Ok(new { Token = token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
