using backend.DTOs.Auth;
using backend.Models.TaiKhoan;
using backend.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var token = _auth.Login(dto);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            _auth.Register(dto);
            return Ok("Registered successfully");
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var user = HttpContext.Items["User"] as TaiKhoan;

            if (user == null)
                return Unauthorized("Invalid or missing token.");

            return Ok(new
            {
                id = user.Id,
                tenTK = user.TenTK,
                email = user.Email,
                vaiTro = user.VaiTro,
                ngayTao = user.NgayTao
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenDTO dto)
        {
            var result = _auth.RefreshToken(dto.RefreshToken);
            return Ok(result);
        }


    }
}