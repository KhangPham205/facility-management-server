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
    public class LiquidateVoucherDetailRepository : ILiquidateVoucherDetailRepository
    {
        private readonly DataApplicationDbContext _context;

        public LiquidateVoucherDetailRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LiquidateVoucherDetail?> GetByIdAsync(string voucherId, string equipmentId)
        {
            return await _context.LiquidateVoucherDetails
                .Include(l => l.Equipment)
                .FirstOrDefaultAsync(l => l.LiquidateId == voucherId && l.EquipmentId == equipmentId);
        }

        public async Task<PageVO<LiquidateVoucherDetail>> GetPagedAsync(
            EntityFilter<LiquidateVoucherDetail> filter,
            EntitySort<LiquidateVoucherDetail> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.LiquidateVoucherDetails.AsQueryable();

            query = query
                .Include(l=>l.Equipment)
                .Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<LiquidateVoucherDetail>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(LiquidateVoucherDetail importVoucherDetail)
        {
            await _context.LiquidateVoucherDetails.AddAsync(importVoucherDetail);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(LiquidateVoucherDetail importVoucherDetail)
        //{
        //    _context.LiquidateVoucherDetails.Update(importVoucherDetail);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> UpdateStatusAsync(string importVoucherDetailId, string approverId, VoucherStatus newStatus)
        //{
        //    var rowsAffected = await _context.LiquidateVoucherDetails
        //        .Where(e => e.VoucherId == importVoucherDetailId)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(e => e.Status, newStatus)
        //            .SetProperty(e => e.ApprovedBy, approverId)
        //        );

        //    return rowsAffected > 0;
        //}

        public async Task DeleteAsync(LiquidateVoucherDetail importVoucherDetail)
        {
            _context.LiquidateVoucherDetails.Remove(importVoucherDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.LiquidateVoucherDetails.AnyAsync(e => e.LiquidateId == id);
        }
    }
}