using backend.DTOs.Equipment.Request;
using backend.Exceptions;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implements
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repo;

        public EquipmentService(IEquipmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EquipmentCategory>> GetAllCategories()
        {
            return await _repo.GetAllCategoriesAsync();
        }

        public async Task<EquipmentCategory> CreateCategory(CreateCategoryDTO DTO)
        {
            var existing = await _repo.GetCategoryByNameAsync(DTO.CategoryName);
            if (existing != null)
            {
                throw new BadRequestException("The device type already exists.");
            }

            var category = new EquipmentCategory
            {
                CategoryName = DTO.CategoryName,
                Description = DTO.Description
            };
            await _repo.AddCategoryAsync(category);
            return category;
        }

        public async Task DeleteCategory(string id)
        {
            // Validate: Không được xóa nếu đang có thiết bị sử dụng (Theo E2 - Exception Flows)
            var hasEquipment = await _repo.CategoryHasEquipmentAsync(id);
            if (hasEquipment)
            {
                throw new BadRequestException("This device cannot be deleted because another device is already in use.\r\n");
            }
            await _repo.DeleteCategoryAsync(id);
        }

        // --- Equipment Logic ---
        public async Task<IEnumerable<Equipment>> GetAllEquipments()
        {
            return await _repo.GetAllEquipmentsAsync();
        }

        public async Task<Equipment?> GetEquipmentById(string id)
        {
            return await _repo.GetEquipmentByIdAsync(id);
        }

        public async Task<Equipment> CreateEquipment(CreateEquipmentDTO DTO)
        {
            // Validate: Category có tồn tại không
            var category = await _repo.GetCategoryByIdAsync(DTO.CategoryId);
            if (category == null) throw new NotFoundException("This type of device does not exist.");

            var equipment = new Equipment
            {
                EquipmentName = DTO.EquipmentName,
                CategoryId = DTO.CategoryId,
                RoomId = DTO.RoomId,
                Quantity = DTO.Quantity,
                IsPublic = DTO.IsPublic,
                Description = DTO.Description,
                WarrantyExpiryDate = DTO.WarrantyExpiryDate,
                Status = Enums.EquipmentStatus.Available // Mặc định là Khả dụng
            };

            await _repo.AddEquipmentAsync(equipment);
            return equipment;
        }

        public async Task<Equipment> UpdateEquipmentStatus(string id, UpdateEquipmentStatusDTO DTO)
        {
            var equipment = await _repo.GetEquipmentByIdAsync(id);
            if (equipment == null) throw new NotFoundException("The device was not found.");

            equipment.Status = DTO.Status;
            await _repo.UpdateEquipmentAsync(equipment);
            return equipment;
        }
    }
}
