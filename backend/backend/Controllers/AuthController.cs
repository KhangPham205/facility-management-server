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
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDTO request)
        {
            var result = await _authService.Login(request);

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
                Role = user.Role,
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            try
            {
                var otp = await _authService.ForgotPassword(dto.Email);

                return Ok(new
                {
                    message = "Mã xác thực đã được gửi (Giả lập).",
                    test_otp = otp
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            try
            {
                await _authService.ResetPassword(dto);
                return Ok(new { message = "Đổi mật khẩu thành công. Vui lòng đăng nhập lại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}