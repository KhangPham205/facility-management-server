using backend.Data;
using backend.Models.Maintenance;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class MaintenanceRequestRepository : IMaintenanceRequestRepository
    {
        private readonly DataApplicationDbContext _context;
        public MaintenanceRequestRepository(DataApplicationDbContext context) => _context = context;
        public async Task<PageVO<MaintenanceRequest>> GetPagedAsync(EntityFilter<MaintenanceRequest> filter, EntitySort<MaintenanceRequest> sort, int page, int size)
        {
            var query = _context.MaintenanceRequests
                .Include(x => x.Creator)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .AsQueryable();
            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<MaintenanceRequest>(page, size, total, content);
        }
        public async Task<MaintenanceRequest?> GetByIdAsync(string id)
        {
            return await _context.MaintenanceRequests
                .Include(x => x.Creator)
                .Include(x => x.Approver)
                .Include(x => x.Details).ThenInclude(d => d.Equipment)
                .FirstOrDefaultAsync(x => x.RequestId == id);
        }
        public async Task AddAsync(MaintenanceRequest request)
        {
            await _context.MaintenanceRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(MaintenanceRequest request)
        {
            _context.MaintenanceRequests.Update(request);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(MaintenanceRequest request)
        {
            _context.MaintenanceRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
