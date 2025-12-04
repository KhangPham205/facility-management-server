using backend.DTOs.Auth;

namespace backend.Services.AuthService
{
    public interface IAuthService
    {
        AuthResponse Login(LoginDTO loginDTO);
        void Register(RegisterDTO registerDTO);
        AuthResponse RefreshToken(string refreshToken);
    }
}
