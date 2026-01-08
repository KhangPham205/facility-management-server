using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IEquipmentRepository
    {
        // Category
        Task<IEnumerable<EquipmentCategory>> GetAllCategoriesAsync();
        Task<EquipmentCategory?> GetCategoryByIdAsync(string id);
        Task<EquipmentCategory?> GetCategoryByNameAsync(string name); // Check trùng tên
        Task AddCategoryAsync(EquipmentCategory category);
        Task UpdateCategoryAsync(EquipmentCategory category);
        Task DeleteCategoryAsync(string id);

        // Equipment
        Task<IEnumerable<Equipment>> GetAllEquipmentsAsync();
        Task<Equipment?> GetEquipmentByIdAsync(string id);
        Task<IEnumerable<Equipment>> GetEquipmentsByRoomAsync(string roomId);
        Task AddEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(Equipment equipment);
        Task DeleteEquipmentAsync(string id);
        Task<bool> CategoryHasEquipmentAsync(string categoryId); // Check trước khi xóa Category
    }
}
