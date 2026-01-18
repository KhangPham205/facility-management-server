using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class FloorRepository : IFloorRepository
    {
        private readonly DataApplicationDbContext _context;

        public FloorRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Floor?> GetByIdAsync(string id)
        {
            return await _context.Floors
                .Include(f=>f.Building)
                .FirstOrDefaultAsync(f=>f.FloorId == id);
        }

        public async Task<PageVO<Floor>> GetPagedAsync(
            EntityFilter<Floor> filter,
            EntitySort<Floor> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Floors.AsNoTracking().AsQueryable();

            query = query.Include(f => f.Building);

            query = query.Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<Floor>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(Floor floor)
        {
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Floor floor)
        {
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Floors.AnyAsync(e => e.FloorId == id);
        }
    }
}