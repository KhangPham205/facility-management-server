using backend.DTOs.Auth;
using backend.Exceptions; // Assuming you have custom exceptions
using backend.Models;
using backend.Models.User;
using backend.Repositories.Interfaces;
using backend.Utils;

namespace backend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly JwtUtils _jwt;

        public AuthService(IUserRepository repo, JwtUtils jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public AuthResponse Login(LoginDTO loginDto)
        {
            var user = _repo.GetByEmail(loginDto.Email);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (!PasswordHasher.Verify(loginDto.Password, user.Password))
                throw new UnauthorizedException("Email or password is incorrect.");

            var token = _jwt.GenerateToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _repo.Save();

            return new AuthResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                User = user
            };
        }

        public void Register(RegisterDTO dto)
        {
            if (_repo.GetByEmail(dto.Email) != null)
                throw new Exception("Email already exists!");

            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(), // Generating String ID as per DB schema
                Fullname = dto.Fullname,
                Email = dto.Email,
                Password = PasswordHasher.Hash(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _repo.Add(newUser);
            _repo.Save();
        }

        public AuthResponse RefreshToken(string refreshToken)
        {
            var user = _repo.GetByRefreshToken(refreshToken);

            if (user == null)
                throw new Exception("Invalid refresh token.");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new Exception("Refresh token expired.");

            var newAccessToken = _jwt.GenerateToken(user);
            var newRefreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _repo.Save();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                User = user
            };
        }
    }
}