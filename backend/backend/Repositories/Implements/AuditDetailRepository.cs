using backend.Data;
using backend.Models.Audit;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class AuditDetailRepository : IAuditDetailRepository
    {
        private readonly DataApplicationDbContext _context;

        public AuditDetailRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuditDetail?> GetByIdAsync(string auditId, string equipmentId)
        {
            return await _context.AuditDetails.FindAsync(auditId, equipmentId);
        }

        public async Task<PageVO<AuditDetail>> GetPagedAsync(
            EntityFilter<AuditDetail> filter,
            EntitySort<AuditDetail> sort,
            int pageNumber,
            int pageSize,
            string auditId)
        {
            var query = _context.AuditDetails.AsQueryable();

            query = query.Where(x => x.AuditId == auditId);

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<AuditDetail>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(AuditDetail auditDetail)
        {
            await _context.AuditDetails.AddAsync(auditDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuditDetail auditDetail)
        {
            _context.AuditDetails.Update(auditDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuditDetail auditDetail)
        {
            _context.AuditDetails.Remove(auditDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string auditId, string equipmentId)
        {
            return await _context.AuditDetails.AnyAsync(e => e.AuditId == auditId && e.EquipmentId == equipmentId);
        }
    }
}