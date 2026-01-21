using backend.DTOs.Auth;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDTO loginDto);
        void Register(RegisterDTO registerDto);
        AuthResponse RefreshToken(string refreshToken);
        Task<string> ForgotPassword(string email);
        Task ResetPassword(ResetPasswordDTO dto);
    }
}