using backend.Data;
using backend.Enums;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class ImportRequestRepository : IImportRequestRepository
    {
        private readonly DataApplicationDbContext _context;

        public ImportRequestRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImportRequest?> GetByIdAsync(string id)
        {
            return await _context.ImportRequests
                .Include(i=>i.Creator)
                .Include(i=>i.Approver)
                .Include(i=>i.Details)
                .FirstOrDefaultAsync(i=>i.RequestId == id);
        }

        public async Task<PageVO<ImportRequest>> GetPagedAsync(
            EntityFilter<ImportRequest> filter,
            EntitySort<ImportRequest> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ImportRequests.AsNoTracking().AsQueryable();

            query = query
                .Include(i => i.Creator)
                .Include(i => i.Approver)
                .Include(i=>i.Details);

            query = query.Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<ImportRequest>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(ImportRequest importRequest)
        {
            await _context.ImportRequests.AddAsync(importRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ImportRequest importRequest)
        {
            _context.ImportRequests.Update(importRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatusAsync(string importRequestId, string approverId, VoucherStatus newStatus)
        {
            var rowsAffected = await _context.ImportRequests
                .Where(e => e.RequestId == importRequestId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.Status, newStatus)
                    .SetProperty(e => e.ApprovedBy, approverId)
                    .SetProperty(e => e.ApprovedAt, DateTime.Now)
                );

            return rowsAffected > 0;
        }

        public async Task DeleteAsync(ImportRequest importRequest)
        {
            _context.ImportRequests.Remove(importRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.ImportRequests.AnyAsync(e => e.RequestId == id);
        }
    }
}