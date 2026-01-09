using backend.DTOs.Auth;
using backend.Exceptions;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;

namespace backend.Services.Implements
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

            var accessToken = _jwt.GenerateToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _repo.Save();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                user = MapToUserDetailDTO(user)
            };
        }


        public void Register(RegisterDTO dto)
        {
            if (_repo.GetByEmail(dto.Email) != null)
                throw new BadRequestException("Email already exists!");

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
                throw new BadRequestException("Invalid refresh token.");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new BadRequestException("Refresh token expired.");

            var newAccessToken = _jwt.GenerateToken(user);
            var newRefreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _repo.Save();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                user = MapToUserDetailDTO(user)
            };
        }

        // Helper method to map User to UserDetailDTO
        private UserDetailDTO MapToUserDetailDTO(User user)
        {
            return new UserDetailDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt
            };
        }
    }
}