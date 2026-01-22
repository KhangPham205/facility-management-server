using backend.Data;
using backend.Models.Repair;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class RepairVoucherRepository : IRepairVoucherRepository
    {
        private readonly DataApplicationDbContext _context;
        public RepairVoucherRepository(DataApplicationDbContext context) => _context = context;
        public async Task<PageVO<RepairVoucher>> GetPagedAsync(EntityFilter<RepairVoucher> filter, EntitySort<RepairVoucher> sort, int page, int size)
        {
            var query = _context.RepairVouchers
                .Include(x => x.Creator)
                .Include(x => x.Invoice)
                .Include(x => x.Provider)
                .Include(x => x.Details)
                    .ThenInclude(d => d.Equipment)
                .AsQueryable();
            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<RepairVoucher>(page, size, total, content);
        }
        public async Task<RepairVoucher?> GetByIdAsync(string id)
        {
            return await _context.RepairVouchers
                .Include(x => x.Creator)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .FirstOrDefaultAsync(x => x.RepairId == id);
        }
        public async Task AddAsync(RepairVoucher voucher)
        {
            await _context.RepairVouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RepairVoucher voucher)
        {
            _context.RepairVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByRequestIdAsync(string requestId)
        {
            return await _context.RepairVouchers.AnyAsync(v => v.RequestId == requestId);
        }
    }
}
