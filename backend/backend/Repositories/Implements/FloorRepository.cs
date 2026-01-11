using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class FloorRepository : IFloorRepository
    {
        private readonly DataApplicationDbContext _context;

        public FloorRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Floor?> GetFloorByIdAsync(string floorId)
        {
            // Tìm kiếm tầng theo FloorId (Khóa chính)
            return await _context.Floors.FindAsync(floorId);
        }

        public async Task<IEnumerable<Floor>> GetAllFloorsAsync()
        {
            // Lấy toàn bộ danh sách tầng
            return await _context.Floors.ToListAsync();
        }

        public async Task<IEnumerable<Floor>> GetFloorsByBuildingIdAsync(string buildingId)
        {
            // Lọc danh sách tầng theo BuildingId (Mã tòa nhà)
            return await _context.Floors
                                 .Where(f => f.BuildingId == buildingId)
                                 .ToListAsync();
        }

        public async Task AddFloorAsync(Floor floor)
        {
            await _context.Floors.AddAsync(floor);
        }

        public async Task RemoveFloorAsync(Floor floor)
        {
            // Thực hiện xóa floor khỏi DbContext
            _context.Floors.Remove(floor);
            await Task.CompletedTask; // Vì phương thức này không có bản Async trực tiếp trong EF Core cho Remove
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Trả về true nếu có ít nhất một bản ghi được thay đổi trong database
            return await _context.SaveChangesAsync() > 0;
        }
    }
}