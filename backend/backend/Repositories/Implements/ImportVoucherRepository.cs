using backend.Data;
using backend.Enums;
using backend.Models;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class ImportVoucherRepository : IImportVoucherRepository
    {
        private readonly DataApplicationDbContext _context;

        public ImportVoucherRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImportVoucher?> GetByIdAsync(string id)
        {
            return await _context.ImportVouchers.FindAsync(id);
        }

        public async Task<PageVO<ImportVoucher>> GetPagedAsync(
            EntityFilter<ImportVoucher> filter,
            EntitySort<ImportVoucher> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ImportVouchers.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<ImportVoucher>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(ImportVoucher importVoucher)
        {
            await _context.ImportVouchers.AddAsync(importVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ImportVoucher importVoucher)
        {
            _context.ImportVouchers.Update(importVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ImportVoucher importVoucher)
        {
            _context.ImportVouchers.Remove(importVoucher);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.ImportVouchers.AnyAsync(e => e.RequestId == id);
        }
    }
}