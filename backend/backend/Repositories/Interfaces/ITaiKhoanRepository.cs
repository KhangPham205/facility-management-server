using backend.Models.TaiKhoan;

namespace backend.Repositories.Interfaces
{
    public interface ITaiKhoanRepository
    {
        TaiKhoan? GetByEmail(string email);
        TaiKhoan? GetByRefreshToken(string refreshToken);
        void Add(TaiKhoan entity);
        void Save();
    }
}
