using backend.Data;
using backend.Models.Borrow;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly DataApplicationDbContext _context;

        public BorrowRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageVO<BorrowVoucher>> GetPagedAsync(EntityFilter<BorrowVoucher> filter, EntitySort<BorrowVoucher> sort, int page, int size)
        {
            var query = _context.BorrowVouchers
                .Include(x => x.Borrower)
                .Include(x => x.Creator)
                .Include(x => x.Details)
                    .ThenInclude(d => d.Equipment)
                .AsQueryable();

            // Áp dụng Filter & Sort
            query = query.Where(filter).OrderBy(sort);

            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PageVO<BorrowVoucher>(page, size, total, content);
        }

        public async Task<BorrowVoucher?> GetByIdAsync(string id)
        {
            return await _context.BorrowVouchers
                .Include(x => x.Borrower)
                .Include(x => x.Creator)
                .Include(x => x.Approver)
                .Include(x => x.Details)
                    .ThenInclude(d => d.Equipment)
                .FirstOrDefaultAsync(x => x.BorrowId == id);
        }

        public async Task AddAsync(BorrowVoucher voucher)
        {
            await _context.BorrowVouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowVoucher voucher)
        {
            _context.BorrowVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}