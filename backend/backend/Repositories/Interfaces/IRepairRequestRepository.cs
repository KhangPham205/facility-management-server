using backend.Models.Repair;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IRepairRequestRepository
    {
        Task<PageVO<RepairRequest>> GetPagedAsync(EntityFilter<RepairRequest> filter, EntitySort<RepairRequest> sort, int page, int size);
        Task<RepairRequest?> GetByIdAsync(string id);
        Task AddAsync(RepairRequest request);
        Task UpdateAsync(RepairRequest request);
        Task DeleteAsync(RepairRequest request);
    }
}
