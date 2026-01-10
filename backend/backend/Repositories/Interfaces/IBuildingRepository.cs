using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IBuildingRepository
    {
        // Lấy tất cả tòa nhà
        Task<IEnumerable<Building>> GetAllAsync();

        // Lấy theo ID (thay thế cho maToa)
        Task<Building?> GetByIdAsync(string id);

        // Tạo mới tòa nhà
        Task AddAsync(Building building);

        // Xóa tòa nhà
        Task DeleteAsync(Building building);

        // Lưu các thay đổi vào Database
        Task<bool> SaveChangesAsync();
    }
}