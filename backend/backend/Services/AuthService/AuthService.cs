using backend.DTOs.Auth;
using backend.Exceptions;
using backend.Models.TaiKhoan;
using backend.Repositories.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ITaiKhoanRepository _repo;
        private readonly JwtUtils _jwt;

        public AuthService(ITaiKhoanRepository repo, JwtUtils jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public AuthResponse Login(LoginDTO loginDTO)
        {
            var user = _repo.GetByEmail(loginDTO.Email);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (user == null || !PasswordHasher.Verify(loginDTO.MatKhau, user.MatKhau))
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

            var newAccount = new TaiKhoan
            {
                TenTK = dto.TenTK,
                Email = dto.Email,
                MatKhau = PasswordHasher.Hash(dto.MatKhau),
                VaiTro = dto.VaiTro,
                NgayTao = DateTime.UtcNow
            };

            _repo.Add(newAccount);
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
