using backend.Data;
using backend.DTOs.Import.Request;
using backend.Enums;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class ImportVoucherDetailRepository : IImportVoucherDetailRepository
    {
        private readonly DataApplicationDbContext _context;

        public ImportVoucherDetailRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImportVoucherDetail?> GetByIdAsync(string id)
        {
            return await _context.ImportVoucherDetails
                .Include(i=>i.Equipment)
                .FirstOrDefaultAsync(i=>i.EquipmentId == id);
        }

        public async Task<PageVO<ImportVoucherDetail>> GetPagedAsync(
            EntityFilter<ImportVoucherDetail> filter,
            EntitySort<ImportVoucherDetail> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ImportVoucherDetails.AsNoTracking().AsQueryable();

            query = query
                .Include(i=>i.Equipment)
                .Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<ImportVoucherDetail>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(ImportVoucherDetail importVoucherDetail)
        {
            await _context.ImportVoucherDetails.AddAsync(importVoucherDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsVoucherDetailValidAsync(string requestId, ImportVoucherDetailDto voucherDetail)
        {
            // Kiểm tra xem trong Request đó có yêu cầu thiết bị tên này không
            bool exists = await _context.ImportRequestDetails
                .AnyAsync(x => x.RequestId == requestId &&
                               x.EquipmentName == voucherDetail.EquipmentName);

            return exists;
        }

        //public async Task UpdateAsync(ImportVoucherDetail importVoucherDetail)
        //{
        //    _context.ImportVoucherDetails.Update(importVoucherDetail);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> UpdateStatusAsync(string importVoucherDetailId, string approverId, VoucherStatus newStatus)
        //{
        //    var rowsAffected = await _context.ImportVoucherDetails
        //        .Where(e => e.VoucherId == importVoucherDetailId)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(e => e.Status, newStatus)
        //            .SetProperty(e => e.ApprovedBy, approverId)
        //        );

        //    return rowsAffected > 0;
        //}

        public async Task DeleteAsync(ImportVoucherDetail importVoucherDetail)
        {
            _context.ImportVoucherDetails.Remove(importVoucherDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.ImportVoucherDetails.AnyAsync(e => e.ImportId == id);
        }
    }
}