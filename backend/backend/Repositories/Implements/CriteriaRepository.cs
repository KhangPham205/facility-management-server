using backend.Data;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly DataApplicationDbContext _context;

        public CriteriaRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Criteria?> GetByIdAsync(string id)
        {
            return await _context.Criterias.FindAsync(id);
        }

        public async Task<PageVO<Criteria>> GetPagedAsync(
            EntityFilter<Criteria> filter,
            EntitySort<Criteria> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Criterias.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<Criteria>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(Criteria criteria)
        {
            await _context.Criterias.AddAsync(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Criteria> criterias)
        {
            await _context.Criterias.AddRangeAsync(criterias);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Criteria criteria)
        {
            _context.Criterias.Update(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Criteria criteria)
        {
            _context.Criterias.Remove(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Criterias.AnyAsync(e => e.CriteriaId == id);
        }
    }
}