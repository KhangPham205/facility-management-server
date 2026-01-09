using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly DataApplicationDbContext _context;

        public BorrowRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowVoucher?> GetByIdAsync(string id)
        {
            // Include BorrowDetails để lấy luôn danh sách thiết bị trong phiếu
            return await _context.BorrowVouchers
                .Include(b => b.BorrowDetails)
                .FirstOrDefaultAsync(b => b.BorrowId == id);
        }

        public async Task<IEnumerable<BorrowVoucher>> GetAllAsync()
        {
            return await _context.BorrowVouchers
                .Include(b => b.BorrowDetails)
                .OrderByDescending(b => b.CreatedAt) // Mới nhất lên đầu
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowVoucher>> GetByUserIdAsync(string userId)
        {
            return await _context.BorrowVouchers
                .Include(b => b.BorrowDetails)
                .Where(b => b.BorrowerId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(BorrowVoucher voucher)
        {
            _context.BorrowVouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowVoucher voucher)
        {
            _context.BorrowVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}