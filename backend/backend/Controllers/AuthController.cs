using backend.Constants;
using backend.DTOs.Auth;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route(ApiEndpoints.Auth)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(401)]
        public ActionResult<AuthResponse> Login(LoginDTO dto)
        {
            var result = _authService.Login(dto);
            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(MessageDTO), 200)]
        [ProducesResponseType(400)]
        public ActionResult<MessageDTO> Register(RegisterDTO dto)
        {
            _authService.Register(dto);
            return Ok(new MessageDTO("Registered successfully"));
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDetailDTO), 200)]
        [ProducesResponseType(401)]
        public ActionResult<UserDetailDTO> Me()
        {
            // Lấy User từ HttpContext (đã được middleware gán vào)
            var user = HttpContext.Items["User"] as User;

            if (user == null)
                return Unauthorized(new MessageDTO("Invalid or missing token."));

            var userDetail = new UserDetailDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };

            return Ok(userDetail);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(400)]
        public ActionResult<AuthResponse> Refresh([FromBody] RefreshTokenDTO dto)
        {
            var result = _authService.RefreshToken(dto.RefreshToken);
            return Ok(result);
        }
    }
}