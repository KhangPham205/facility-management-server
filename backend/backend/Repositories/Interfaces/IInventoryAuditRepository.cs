using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IInventoryAuditRepository
    {
        Task<InventoryAudit?> GetByIdAsync(string id);

        Task<PageVO<InventoryAudit>> GetPagedAsync(EntityFilter<InventoryAudit> filter, EntitySort<InventoryAudit> sort, int pageNumber, int pageSize);
        Task AddAsync(InventoryAudit inventoryAudit);
        //Task UpdateAsync(InventoryAudit inventoryAudit);
        Task DeleteAsync(InventoryAudit inventoryAudit);

        Task<bool> ExistsAsync(string id);
    }
}