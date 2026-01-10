using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataApplicationDbContext _context;
        public RoomRepository(DataApplicationDbContext context) => 
            _context = context;

        public async Task<Room?> GetByIdAsync(string roomId) => 
            await _context.Rooms.FindAsync(roomId);
        public async Task<IEnumerable<Room>> GetAllAsync() => 
            await _context.Rooms.ToListAsync();
        public async Task<IEnumerable<Room>> GetByFloorIdAsync(string floorId) =>
            await _context.Rooms.Where(r => r.FloorId == floorId).ToListAsync();
        public async Task AddAsync(Room room) => 
            await _context.Rooms.AddAsync(room);
        public void Remove(Room room) => 
            _context.Rooms.Remove(room);
        public async Task<bool> SaveChangesAsync() => 
            await _context.SaveChangesAsync() > 0;
    }
}
