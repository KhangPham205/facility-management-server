using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IAuditDetailRepository
    {
        Task<AuditDetail?> GetByIdAsync(string auditId, string equipmentId);

        Task<PageVO<AuditDetail>> GetPagedAsync(EntityFilter<AuditDetail> filter, EntitySort<AuditDetail> sort, int pageNumber, int pageSize, string auditId);
        //Task AddAsync(AuditDetail auditDetail);
        Task UpdateAsync(AuditDetail auditDetail);
        Task DeleteAsync(AuditDetail auditDetail);

        Task<bool> ExistsAsync(string auditId, string equipmentId);
    }
}