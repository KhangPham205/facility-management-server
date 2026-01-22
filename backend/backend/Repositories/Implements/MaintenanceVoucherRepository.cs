using backend.Data;
using backend.Models.Maintenance;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class MaintenanceVoucherRepository : IMaintenanceVoucherRepository
    {
        private readonly DataApplicationDbContext _context;
        public MaintenanceVoucherRepository(DataApplicationDbContext context) => _context = context;
        public async Task<PageVO<MaintenanceVoucher>> GetPagedAsync(EntityFilter<MaintenanceVoucher> filter, EntitySort<MaintenanceVoucher> sort, int page, int size)
        {
            var query = _context.MaintenanceVouchers
                .Include(x => x.Creator)
                .AsQueryable();
            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<MaintenanceVoucher>(page, size, total, content);
        }
        public async Task<MaintenanceVoucher?> GetByIdAsync(string id)
        {
            return await _context.MaintenanceVouchers
               .Include(x => x.Creator)
               .Include(x => x.Details).ThenInclude(d => d.Equipment)
               .FirstOrDefaultAsync(x => x.VoucherId == id);
        }
        public async Task AddAsync(MaintenanceVoucher voucher)
        {
            await _context.MaintenanceVouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(MaintenanceVoucher voucher)
        {
            _context.MaintenanceVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByRequestIdAsync(string requestId)
        {
            return await _context.MaintenanceVouchers.AnyAsync(x => x.RequestId == requestId);
        }
    }
}
