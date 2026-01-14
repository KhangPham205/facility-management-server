using backend.Data;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class EquipmentCategoryRepository : IEquipmentCategoryRepository
    {
        private readonly DataApplicationDbContext _context;

        public EquipmentCategoryRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EquipmentCategory?> GetByIdAsync(string id)
        {
            return await _context.EquipmentCategories.FindAsync(id);
        }

        public async Task<PageVO<EquipmentCategory>> GetPagedAsync(
            EntityFilter<EquipmentCategory> filter,
            EntitySort<EquipmentCategory> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.EquipmentCategories.AsQueryable();

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<EquipmentCategory>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(EquipmentCategory equipmentCategory)
        {
            await _context.EquipmentCategories.AddAsync(equipmentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EquipmentCategory equipmentCategory)
        {
            _context.EquipmentCategories.Update(equipmentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EquipmentCategory equipmentCategory)
        {
            _context.EquipmentCategories.Remove(equipmentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.EquipmentCategories.AnyAsync(e => e.EquipmentCategoryId == id);
        }
    }
}