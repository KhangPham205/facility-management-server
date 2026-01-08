using backend.DTOs.Equipment;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IEquipmentService
    {
        // Category
        Task<IEnumerable<EquipmentCategory>> GetAllCategories();
        Task<EquipmentCategory> CreateCategory(CreateCategoryDTO dto);
        Task DeleteCategory(string id);

        // Equipment
        Task<IEnumerable<Equipment>> GetAllEquipments();
        Task<Equipment?> GetEquipmentById(string id);
        Task<Equipment> CreateEquipment(CreateEquipmentDTO dto);
        Task<Equipment> UpdateEquipmentStatus(string id, UpdateEquipmentStatusDTO dto);
    }
}
