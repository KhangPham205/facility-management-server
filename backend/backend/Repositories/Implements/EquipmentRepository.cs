using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataApplicationDbContext _context;

        public EquipmentRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        // --- Category Impl ---
        public async Task<IEnumerable<EquipmentCategory>> GetAllCategoriesAsync()
        {
            return await _context.EquipmentCategories.ToListAsync();
        }

        public async Task<EquipmentCategory?> GetCategoryByIdAsync(string id)
        {
            return await _context.EquipmentCategories.FindAsync(id);
        }

        public async Task<EquipmentCategory?> GetCategoryByNameAsync(string name)
        {
            return await _context.EquipmentCategories
                .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == name.ToLower());
        }

        public async Task AddCategoryAsync(EquipmentCategory category)
        {
            _context.EquipmentCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(EquipmentCategory category)
        {
            _context.EquipmentCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await _context.EquipmentCategories.FindAsync(id);
            if (category != null)
            {
                _context.EquipmentCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CategoryHasEquipmentAsync(string categoryId)
        {
            return await _context.Equipments.AnyAsync(e => e.CategoryId == categoryId);
        }

        // --- Equipment Impl ---
        public async Task<IEnumerable<Equipment>> GetAllEquipmentsAsync()
        {
            // Include Category để lấy tên loại khi hiển thị
            return await _context.Equipments.Include(e => e.Category).ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(string id)
        {
            return await _context.Equipments.Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.EquipmentId == id);
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentsByRoomAsync(string roomId)
        {
            return await _context.Equipments.Where(e => e.RoomId == roomId).ToListAsync();
        }

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEquipmentAsync(string id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}