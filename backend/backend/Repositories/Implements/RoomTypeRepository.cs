using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly DataApplicationDbContext _context;

        public RoomTypeRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoomType?> GetByIdAsync(string roomTypeId)
        {
            return await _context.RoomTypes.FindAsync(roomTypeId);
        }

        public async Task<IEnumerable<RoomType>> GetAllAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        public async Task AddAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
        }

        public void Remove(RoomType roomType)
        {
            _context.RoomTypes.Remove(roomType);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}