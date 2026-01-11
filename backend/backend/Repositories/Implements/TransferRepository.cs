using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DataApplicationDbContext _context;

        public TransferRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransferVoucher?> GetByIdAsync(string id)
        {
            return await _context.TransferVouchers.FindAsync(id);
        }

        public async Task<PageVO<TransferVoucher>> GetAllPagedAsync(int page, int size)
        {
            var totalElements = await _context.TransferVouchers.CountAsync();
            var skip = (page - 1) * size;

            var content = await _context.TransferVouchers
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(size)
                .ToListAsync();

            return new PageVO<TransferVoucher>(page, size, totalElements, content);
        }

        public async Task AddAsync(TransferVoucher voucher)
        {
            _context.TransferVouchers.Add(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransferVoucher voucher)
        {
            _context.TransferVouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TransferVoucher voucher)
        {
            _context.TransferVouchers.Remove(voucher);
            await _context.SaveChangesAsync();
        }
    }
}