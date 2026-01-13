using backend.Data;
using backend.Models.Transfer;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class TransferRequestRepository : ITransferRequestRepository
    {
        private readonly DataApplicationDbContext _context;
        public TransferRequestRepository(DataApplicationDbContext context) => _context = context;

        public async Task<PageVO<TransferRequest>> GetPagedAsync(EntityFilter<TransferRequest> filter, EntitySort<TransferRequest> sort, int page, int size)
        {
            var query = _context.TransferRequests
                .Include(x => x.Creator)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .AsQueryable();

            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PageVO<TransferRequest>(page, size, total, content);
        }

        public async Task<TransferRequest?> GetByIdAsync(string id)
        {
            return await _context.TransferRequests
                .Include(x => x.Creator)
                .Include(x => x.Approver)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .FirstOrDefaultAsync(x => x.RequestId == id);
        }

        public async Task AddAsync(TransferRequest request)
        {
            await _context.TransferRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransferRequest request)
        {
            _context.TransferRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TransferRequest request)
        {
            _context.TransferRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}