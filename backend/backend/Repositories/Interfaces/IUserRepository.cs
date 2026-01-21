using backend.Models;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? GetByRefreshToken(string refreshToken);
        void Add(User user);
        Task SaveChangesAsync();
        Task<PageVO<User>> GetUsersPagedAsync(EntityFilter<User> filter, EntitySort<User> sort, int page, int size);
        Task<User?> GetByIdAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}