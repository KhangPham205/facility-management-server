using backend.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using backend.Models.Area;

namespace backend.Repositories.Implements
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DataApplicationDbContext _context;

        public BuildingRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Building>> GetAllAsync()
        {
            // Giả định DbSet trong DbContext đã đổi tên thành Buildings
            return await _context.Buildings.ToListAsync();
        }

        public async Task<Building?> GetByIdAsync(string id)
        {
            return await _context.Buildings.FindAsync(id);
        }

        public async Task AddAsync(Building building)
        {
            await _context.Buildings.AddAsync(building);
        }

        public async Task DeleteAsync(Building building)
        {
            // Xóa trực tiếp từ DbSet
            _context.Buildings.Remove(building);
            await Task.CompletedTask; // Đảm bảo tính bất đồng bộ nếu cần
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Trả về true nếu có ít nhất 1 dòng dữ liệu được thay đổi
            return await _context.SaveChangesAsync() > 0;
        }
    }
}