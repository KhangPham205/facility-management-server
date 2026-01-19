using backend.Enums;
using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<Equipment?> GetByIdAsync(string id);

        Task<PageVO<Equipment>> GetPagedAsync(EntityFilter<Equipment> filter, EntitySort<Equipment> sort, int pageNumber, int pageSize);
        Task AddAsync(Equipment equipment);
        Task AddRangeAsync(IEnumerable<Equipment> entities);
        Task UpdateAsync(Equipment equipment);
        Task<bool> UpdateStatusAsync(string equipmentId, EquipmentStatus newStatus);
        Task DeleteAsync(Equipment equipment);

        Task<bool> ExistsAsync(string id);
    }
}