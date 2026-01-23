using backend.Data;
using backend.Enums;
using backend.Models.Liquidate;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class LiquidateRequestRepository : ILiquidateRequestRepository
    {
        private readonly DataApplicationDbContext _context;

        public LiquidateRequestRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LiquidateRequest?> GetByIdAsync(string id)
        {
            return await _context.LiquidateRequests
                .Include(l=>l.Creator)
                .Include(i => i.Details)
                    .ThenInclude(Details=>Details.Equipment)
                .Include(l=>l.Approver)
                .FirstOrDefaultAsync(l=>l.RequestId == id);
        }

        public async Task<PageVO<LiquidateRequest>> GetPagedAsync(
            EntityFilter<LiquidateRequest> filter,
            EntitySort<LiquidateRequest> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.LiquidateRequests.AsNoTracking().AsQueryable();

            query = query
                .Include(l => l.Creator)
                .Include(i => i.Details)
                    .ThenInclude(Details=>Details.Equipment)
                .Include(l => l.Approver);

            query = query.Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<LiquidateRequest>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(LiquidateRequest liquidateRequest)
        {
            await _context.LiquidateRequests.AddAsync(liquidateRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LiquidateRequest liquidateRequest)
        {
            _context.LiquidateRequests.Update(liquidateRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatusAsync(string liquidateRequestId, string approverId, VoucherStatus newStatus)
        {
            var rowsAffected = await _context.LiquidateRequests
                .Where(e => e.RequestId == liquidateRequestId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.Status, newStatus)
                    .SetProperty(e => e.ApprovedBy, approverId)
                    .SetProperty(e=>e.ApprovedAt,DateTime.Now)
                );

            return rowsAffected > 0;
        }

        public async Task DeleteAsync(LiquidateRequest liquidateRequest)
        {
            _context.LiquidateRequests.Remove(liquidateRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.LiquidateRequests.AnyAsync(e => e.RequestId == id);
        }
    }
}