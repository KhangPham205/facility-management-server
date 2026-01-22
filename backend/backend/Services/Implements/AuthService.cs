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

        public async Task<AuthResponse> Login(LoginDTO loginDto)
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
            await _repo.SaveChangesAsync();

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

            _repo.AddAsync(newUser);
            _repo.SaveChangesAsync();
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
            _repo.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                user = MapToUserDetailDTO(user)
            };
        }
        public async Task<string> ForgotPassword(string email)
        {
            var user = _repo.GetByEmail(email);
            if (user == null)
                throw new NotFoundException("The email address does not exist in the system.");

            // Tạo OTP ngẫu nhiên 6 số
            var otp = new Random().Next(100000, 999999).ToString();

            user.ResetToken = otp;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(5); // OTP hết hạn sau 5 phút

            await _repo.SaveChangesAsync();

            return otp;
        }

        public async Task ResetPassword(ResetPasswordDTO dto)
        {
            var user = _repo.GetByEmail(dto.Email);
            if (user == null)
                throw new NotFoundException("The user does not exist.");

            if (user.ResetToken != dto.Otp)
                throw new BadRequestException("The OTP code is incorrect.");

            if (user.ResetTokenExpiry < DateTime.UtcNow)
                throw new BadRequestException("The OTP code has expired. Please get a new code.");

            user.Password = PasswordHasher.Hash(dto.NewPassword);

            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _repo.SaveChangesAsync();
        }

        // Helper method to map User to UserDetailDTO
        private UserDetailDTO MapToUserDetailDTO(User user)
        {
            return new UserDetailDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
    }
}