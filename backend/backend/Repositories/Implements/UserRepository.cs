using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataApplicationDbContext _context;

        public UserRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
            => _context.Users.FirstOrDefault(u => u.Email == email);

        public User? GetByRefreshToken(string refreshToken)
            => _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

        public void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<PageVO<User>> GetUsersPagedAsync(int page, int size)
        {
            var totalElements = await _context.Users.CountAsync();

            var skip = (page - 1) * size;

            var content = await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Skip(skip)
                .Take(size)
                .ToListAsync();

            return new PageVO<User>(page, size, totalElements, content);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}