using backend.Models.User;

namespace backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? GetByRefreshToken(string refreshToken);
        void Add(User entity);
        void Save();
    }
}