using backend.Models;
using backend.vo;

namespace backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? GetByRefreshToken(string refreshToken);
        void Add(User user);
        void Save();
        Task<PageVO<User>> GetUsersPagedAsync(int page, int size);
        Task<User?> GetByIdAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}