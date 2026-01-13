using backend.Data;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DataApplicationDbContext _context;

        public BuildingRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Building?> GetByIdAsync(string id)
        {
            return await _context.Buildings.FindAsync(id);
        }

        public async Task<PageVO<Building>> GetPagedAsync(
            EntityFilter<Building> filter,
            EntitySort<Building> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Buildings.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<Building>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(Building building)
        {
            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Building building)
        {
            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Building building)
        {
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Buildings.AnyAsync(e => e.BuildingId == id);
        }
    }
}