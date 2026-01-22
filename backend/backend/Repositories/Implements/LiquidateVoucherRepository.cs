using backend.Data;
using backend.Enums;
using backend.Models;
using backend.Models.Liquidate;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class LiquidateVoucherRepository : ILiquidateVoucherRepository
    {
        private readonly DataApplicationDbContext _context;

        public LiquidateVoucherRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LiquidateVoucher?> GetByIdAsync(string id)
        {
            return await _context.LiquidateVouchers
                .Include(l => l.Creator)
                .Include(i => i.Details)
                .Include(l => l.Invoice)
                    .ThenInclude(i=>i.Unit)
                .FirstOrDefaultAsync(l=>l.LiquidateId == id);
        }

        public async Task<PageVO<LiquidateVoucher>> GetPagedAsync(
            EntityFilter<LiquidateVoucher> filter,
            EntitySort<LiquidateVoucher> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.LiquidateVouchers.AsNoTracking().AsQueryable();

            query = query
                .Include(l => l.Creator)
                .Include(i => i.Details).ThenInclude(d => d.Equipment)
                .Include(l => l.Invoice)
                    .ThenInclude(i => i.Unit);

            query = query.Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<LiquidateVoucher>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(LiquidateVoucher liquidateVoucher)
        {
            await _context.LiquidateVouchers.AddAsync(liquidateVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LiquidateVoucher liquidateVoucher)
        {
            _context.LiquidateVouchers.Update(liquidateVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LiquidateVoucher liquidateVoucher)
        {
            _context.LiquidateVouchers.Remove(liquidateVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.LiquidateVouchers.AnyAsync(e => e.RequestId == id);
        }
    }
}