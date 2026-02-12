using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dto;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpPost("creat-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto _userDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.CreateUserAsync(_userDto, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);

        }


        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserByIdAsync(id, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }


        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllUsersAsync(cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] CreateUserDto _userDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.UpdateUserAsync(id, _userDto, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellation)
        {
            var result = await _userService.DeleteUserAsync(id, cancellation);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);

        }
    }
}
