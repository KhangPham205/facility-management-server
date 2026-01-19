using backend.Data;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class ExternalUnitRepository : IExternalUnitRepository
    {
        private readonly DataApplicationDbContext _context;
        public ExternalUnitRepository(DataApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PageVO<ExternalUnit>> GetPagedAsync(EntityFilter<ExternalUnit> filter, EntitySort<ExternalUnit> sort, int page, int size)
        {
            var query = _context.ExternalUnits.AsQueryable();
            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Apply sorting
            if (sort != null)
            {
                query = query.OrderBy(sort);
            }
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<ExternalUnit>(page, size, totalItems, items);
        }
        public async Task<ExternalUnit?> GetByIdAsync(string id)
        {
            return await _context.ExternalUnits.FindAsync(id);
        }
        public async Task AddAsync(ExternalUnit unit)
        {
            await _context.ExternalUnits.AddAsync(unit);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ExternalUnit unit)
        {
            _context.ExternalUnits.Update(unit);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ExternalUnit unit)
        {
            _context.ExternalUnits.Remove(unit);
            await _context.SaveChangesAsync();
        }
    }
}
