using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataApplicationDbContext _context;

        public RoomRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> GetByIdAsync(string id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<PageVO<Room>> GetPagedAsync(
            EntityFilter<Room> filter,
            EntitySort<Room> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Rooms.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<Room>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Rooms.AnyAsync(e => e.RoomId == id);
        }
    }
}