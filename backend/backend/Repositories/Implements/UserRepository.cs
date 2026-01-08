using backend.Data;
using backend.Models;
using backend.Models.User;
using backend.Repositories.Interfaces;

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
    }
}