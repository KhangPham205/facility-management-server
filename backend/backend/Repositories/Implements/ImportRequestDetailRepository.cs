using backend.Data;
using backend.Enums;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class ImportRequestDetailRepository : IImportRequestDetailRepository
    {
        private readonly DataApplicationDbContext _context;

        public ImportRequestDetailRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImportRequestDetail?> GetByIdAsync(string id)
        {
            return await _context.ImportRequestDetails.FindAsync(id);
        }

        public async Task<PageVO<ImportRequestDetail>> GetPagedAsync(
            EntityFilter<ImportRequestDetail> filter,
            EntitySort<ImportRequestDetail> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ImportRequestDetails.AsQueryable();

            query = query.Where(filter);

            var totalElements = await query.CountAsync();

            query = query.OrderBy(sort);


            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<ImportRequestDetail>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(ImportRequestDetail importRequestDetail)
        {
            await _context.ImportRequestDetails.AddAsync(importRequestDetail);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateAsync(ImportRequestDetail importRequestDetail)
        //{
        //    _context.ImportRequestDetails.Update(importRequestDetail);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> UpdateStatusAsync(string importRequestDetailId, string approverId, VoucherStatus newStatus)
        //{
        //    var rowsAffected = await _context.ImportRequestDetails
        //        .Where(e => e.RequestId == importRequestDetailId)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(e => e.Status, newStatus)
        //            .SetProperty(e => e.ApprovedBy, approverId)
        //        );

        //    return rowsAffected > 0;
        //}

        public async Task DeleteAsync(ImportRequestDetail importRequestDetail)
        {
            _context.ImportRequestDetails.Remove(importRequestDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.ImportRequestDetails.AnyAsync(e => e.RequestId == id);
        }
    }
}