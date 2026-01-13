using backend.Data;
using backend.Models.Transfer;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class TransferVoucherRepository : ITransferVoucherRepository
    {
        private readonly DataApplicationDbContext _context;
        public TransferVoucherRepository(DataApplicationDbContext context) => _context = context;

        public async Task<PageVO<TransferVoucher>> GetPagedAsync(EntityFilter<TransferVoucher> filter, EntitySort<TransferVoucher> sort, int page, int size)
        {
            var query = _context.TransferVouchers
                .Include(x => x.Creator)
                .AsQueryable();

            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<TransferVoucher>(page, size, total, content);
        }

        public async Task<TransferVoucher?> GetByIdAsync(string id)
        {
            return await _context.TransferVouchers
               .Include(x => x.Creator)
               .Include(x => x.Details).ThenInclude(d => d.Equipment)
               .FirstOrDefaultAsync(x => x.TransferId == id);
        }

        public async Task AddAsync(TransferVoucher voucher)
        {
            await _context.TransferVouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByRequestIdAsync(string requestId)
        {
            return await _context.TransferVouchers.AnyAsync(x => x.RequestId == requestId);
        }
    }
}
