using backend.DTOs.user;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<PageVO<UserResponseDTO>> GetUsers(int page, int size)
        {
            var pagedResult = await _userRepo.GetUsersPagedAsync(page, size);

            var dtoList = pagedResult.Content.Select(u => new UserResponseDTO
            {
                UserId = u.UserId,
                Fullname = u.Fullname,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();

            return new PageVO<UserResponseDTO>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<UserResponseDTO?> GetUserById(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return new UserResponseDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDTO> CreateUser(CreateUserDTO dto)
        {
            if (await _userRepo.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("This email has been used.");
            }

            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Fullname = dto.Fullname,
                Email = dto.Email,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow,
                Password = PasswordHasher.Hash(dto.Password)
            };

            _userRepo.Add(user);

            return new UserResponseDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDTO> UpdateUser(string id, UpdateUserDTO dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) throw new Exception("This user does not exist.");

            user.Fullname = dto.Fullname;
            user.Role = dto.Role;

            await _userRepo.UpdateAsync(user);

            return new UserResponseDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) throw new Exception("This user does not exist.");

            await _userRepo.DeleteAsync(user);
        }
    }
}