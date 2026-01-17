using backend.Data;
using backend.Models.Audit;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class InventoryAuditRepository : IInventoryAuditRepository
    {
        private readonly DataApplicationDbContext _context;

        public InventoryAuditRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryAudit?> GetByIdAsync(string id)
        {
            return await _context.InventoryAudits.FindAsync(id);
        }

        public async Task<PageVO<InventoryAudit>> GetPagedAsync(
            EntityFilter<InventoryAudit> filter,
            EntitySort<InventoryAudit> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.InventoryAudits.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<InventoryAudit>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(InventoryAudit inventoryAudit)
        {
            await _context.InventoryAudits.AddAsync(inventoryAudit);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(InventoryAudit inventoryAudit)
        //{
        //    _context.InventoryAudits.Update(inventoryAudit);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteAsync(InventoryAudit inventoryAudit)
        {
            _context.InventoryAudits.Remove(inventoryAudit);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.InventoryAudits.AnyAsync(e => e.PeriodId == id);
        }
    }
}