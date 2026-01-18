using backend.Data;
using backend.Models.Repair;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private readonly DataApplicationDbContext _context;
        public RepairRequestRepository(DataApplicationDbContext context) => _context = context;

        public async Task<PageVO<RepairRequest>> GetPagedAsync(EntityFilter<RepairRequest> filter, EntitySort<RepairRequest> sort, int page, int size)
        {
            var query = _context.RepairRequests
                .Include(x => x.Creator)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .AsQueryable();
            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PageVO<RepairRequest>(page, size, total, content);
        }
        public async Task<RepairRequest?> GetByIdAsync(string id)
        {
            return await _context.RepairRequests
                .Include(x => x.Creator)
                .Include(x => x.Approver)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .FirstOrDefaultAsync(x => x.RequestId == id);
        }
        public async Task AddAsync(RepairRequest request)
        {
            await _context.RepairRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(RepairRequest request)
        {
            _context.RepairRequests.Update(request);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(RepairRequest request)
        {
            _context.RepairRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
