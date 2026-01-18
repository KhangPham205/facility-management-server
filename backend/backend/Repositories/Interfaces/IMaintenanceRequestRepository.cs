using backend.Models.Maintenance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IMaintenanceRequestRepository
    {
        Task<PageVO<MaintenanceRequest>> GetPagedAsync(EntityFilter<MaintenanceRequest> filter, EntitySort<MaintenanceRequest> sort, int page, int size);
        Task<MaintenanceRequest?> GetByIdAsync(string id);
        Task AddAsync(MaintenanceRequest request);
        Task UpdateAsync(MaintenanceRequest request);
        Task DeleteAsync(MaintenanceRequest request);
    }
}
