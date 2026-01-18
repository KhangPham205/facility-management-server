using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IPeriodicAuditRepository
    {
        Task<PeriodicAudit?> GetByIdAsync(string id);

        Task<PageVO<PeriodicAudit>> GetPagedAsync(EntityFilter<PeriodicAudit> filter, EntitySort<PeriodicAudit> sort, int pageNumber, int pageSize);
        Task AddAsync(PeriodicAudit periodicAudit);
        //Task UpdateAsync(PeriodicAudit periodicAudit);
        Task DeleteAsync(PeriodicAudit periodicAudit);

        Task<bool> ExistsAsync(string id);
    }
}