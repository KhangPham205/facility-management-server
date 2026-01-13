using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly DataApplicationDbContext _context;

        public RoomTypeRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoomType?> GetByIdAsync(string id)
        {
            return await _context.RoomTypes.FindAsync(id);
        }

        public async Task<PageVO<RoomType>> GetPagedAsync(
            EntityFilter<RoomType> filter,
            EntitySort<RoomType> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.RoomTypes.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<RoomType>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomType roomType)
        {
            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.RoomTypes.AnyAsync(e => e.RoomTypeId == id);
        }
    }
}