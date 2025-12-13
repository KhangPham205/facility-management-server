using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IToaRepository
    {
        // Lấy tất cả
        Task<IEnumerable<Toa>> GetAllAsync();

        // Lấy theo mã
        Task<Toa?> GetByIdAsync(string maToa);

        // Tạo mới
        Task AddAsync(Toa toa);

        // Xóa
        Task DeleteAsync(Toa toa);

        // Lưu thay đổi
        Task<bool> SaveChangesAsync();
    }
}
