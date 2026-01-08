using backend.DTOs.Auth;

namespace backend.Services.Auth
{
    public interface IAuthService
    {
        AuthResponse Login(LoginDTO loginDto);
        void Register(RegisterDTO registerDto);
        AuthResponse RefreshToken(string refreshToken);
    }
}