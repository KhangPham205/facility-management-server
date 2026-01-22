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
    public class LiquidateRequestDetailRepository : ILiquidateRequestDetailRepository
    {
        private readonly DataApplicationDbContext _context;

        public LiquidateRequestDetailRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LiquidateRequestDetail?> GetByIdAsync(string requestId, string equipmentId)
        {
            return await _context.LiquidateRequestDetails
                .Include(l => l.Equipment)
                .FirstOrDefaultAsync(l => l.RequestId == requestId && l.EquipmentId == equipmentId);
        }

        public async Task<PageVO<LiquidateRequestDetail>> GetPagedAsync(
            EntityFilter<LiquidateRequestDetail> filter,
            EntitySort<LiquidateRequestDetail> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.LiquidateRequestDetails.AsNoTracking().AsQueryable();

            query = query
                .Include(l=>l.Equipment)
                .Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<LiquidateRequestDetail>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(LiquidateRequestDetail importRequestDetail)
        {
            await _context.LiquidateRequestDetails.AddAsync(importRequestDetail);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(LiquidateRequestDetail importRequestDetail)
        //{
        //    _context.LiquidateRequestDetails.Update(importRequestDetail);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> UpdateStatusAsync(string importRequestDetailId, string approverId, VoucherStatus newStatus)
        //{
        //    var rowsAffected = await _context.LiquidateRequestDetails
        //        .Where(e => e.RequestId == importRequestDetailId)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(e => e.Status, newStatus)
        //            .SetProperty(e => e.ApprovedBy, approverId)
        //        );

        //    return rowsAffected > 0;
        //}

        public async Task DeleteAsync(LiquidateRequestDetail importRequestDetail)
        {
            _context.LiquidateRequestDetails.Remove(importRequestDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.LiquidateRequestDetails.AnyAsync(e => e.RequestId == id);
        }
    }
}