using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IEquipmentCategoryRepository
    {
        Task<EquipmentCategory?> GetByIdAsync(string id);

        Task<PageVO<EquipmentCategory>> GetPagedAsync(EntityFilter<EquipmentCategory> filter, EntitySort<EquipmentCategory> sort, int pageNumber, int pageSize);
        Task AddAsync(EquipmentCategory equipmentCategory);
        Task UpdateAsync(EquipmentCategory equipmentCategory);
        Task DeleteAsync(EquipmentCategory equipmentCategory);

        Task<bool> ExistsAsync(string id);
    }
}