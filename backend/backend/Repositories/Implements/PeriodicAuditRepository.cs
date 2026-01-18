using backend.Data;
using backend.Models.Audit;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class PeriodicAuditRepository : IPeriodicAuditRepository
    {
        private readonly DataApplicationDbContext _context;

        public PeriodicAuditRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PeriodicAudit?> GetByIdAsync(string id)
        {
            return await _context.PeriodicAudits.FindAsync(id);
        }

        public async Task<PageVO<PeriodicAudit>> GetPagedAsync(
            EntityFilter<PeriodicAudit> filter,
            EntitySort<PeriodicAudit> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.PeriodicAudits.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<PeriodicAudit>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(PeriodicAudit periodicAudit)
        {
            await _context.PeriodicAudits.AddAsync(periodicAudit);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(PeriodicAudit periodicAudit)
        //{
        //    _context.PeriodicAudits.Update(periodicAudit);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteAsync(PeriodicAudit periodicAudit)
        {
            _context.PeriodicAudits.Remove(periodicAudit);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.PeriodicAudits.AnyAsync(e => e.PeriodId == id);
        }
    }
}