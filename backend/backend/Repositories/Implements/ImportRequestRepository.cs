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
            return await _context.ImportRequests.FindAsync(id);
        }

        public async Task<PageVO<ImportRequest>> GetPagedAsync(
            EntityFilter<ImportRequest> filter,
            EntitySort<ImportRequest> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ImportRequests.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

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

        public async Task<bool> UpdateStatusAsync(string importRequestId, VoucherStatus newStatus)
        {
            var rowsAffected = await _context.ImportRequests
                .Where(e => e.RequestId == importRequestId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.Status, newStatus)
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