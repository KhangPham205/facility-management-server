using backend.Data;
using backend.Enums;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataApplicationDbContext _context;

        public EquipmentRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Equipment?> GetByIdAsync(string id)
        {
            return await _context.Equipments
                .Include(e=>e.Category)
                .FirstOrDefaultAsync(e=>e.EquipmentId == id);
        }

        public async Task<PageVO<Equipment>> GetPagedAsync(
            EntityFilter<Equipment> filter,
            EntitySort<Equipment> sort,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Equipments.AsNoTracking().AsQueryable();

            query = query.Include(e=>e.Category);

            query = query.Where(filter);

            query = query.OrderBy(sort);

            var totalElements = await query.CountAsync();

            var content = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageVO<Equipment>(pageNumber, pageSize, totalElements, content);
        }

        public async Task AddAsync(Equipment equipment)
        {
            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatusAsync(string equipmentId, EquipmentStatus newStatus)
        {
            var rowsAffected = await _context.Equipments
                .Where(e => e.EquipmentId == equipmentId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.Status, newStatus)
                );

            return rowsAffected > 0;
        }

        public async Task DeleteAsync(Equipment equipment)
        {
            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Equipments.AnyAsync(e => e.EquipmentId == id);
        }
    }
}