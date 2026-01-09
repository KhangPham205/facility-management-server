using backend.Constants;
using backend.DTOs.Auth;
using backend.DTOs.user;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Users)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PageVO<UserResponseDTO>>> GetUsers([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _userService.GetUsers(page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(string id)
            {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> Create([FromBody] CreateUserDTO dto)
        {
            try
            {
                var res = await _userService.CreateUser(dto);
                return CreatedAtAction(
                    nameof(GetUsers),
                    new { id = res.UserId },
                    res
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Update(string id, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(id, dto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}