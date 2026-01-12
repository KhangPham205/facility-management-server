using backend.Data;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class FundSourceRepository : IFundSourceRepository
    {
        private readonly DataApplicationDbContext _context;

        public FundSourceRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FundSource?> GetByIdAsync(string id)
        {
            return await _context.FundSources.FindAsync(id);
        }

        public async Task<PageVO<FundSource>> GetPagedAsync(
            EntityFilter<FundSource> filter,
            EntitySort<FundSource> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.FundSources.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<FundSource>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(FundSource fundSource)
        {
            await _context.FundSources.AddAsync(fundSource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FundSource fundSource)
        {
            _context.FundSources.Update(fundSource);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FundSource fundSource)
        {
            _context.FundSources.Remove(fundSource);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.FundSources.AnyAsync(e => e.SourceId == id);
        }
    }
}