using backend.Data;
using backend.Models.TaiKhoan;
using backend.Repositories.Interfaces;

namespace backend.Repositories.Implements
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {

        private readonly DataApplicationDbContext _context;

        public TaiKhoanRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public TaiKhoan? GetByEmail(string email)
            => _context.TaiKhoan.FirstOrDefault(tk => tk.Email == email);
        public TaiKhoan? GetByRefreshToken(string refreshToken)
            => _context.TaiKhoan.FirstOrDefault(u => u.RefreshToken == refreshToken);
        public void Add(TaiKhoan entity)
        {
            _context.TaiKhoan.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
