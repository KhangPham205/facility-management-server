using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Models;
using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IPeriodicAuditService
    {
        Task<PageVO<PeriodicAuditResponse>> GetAll(EntityFilter<PeriodicAudit> filter, EntitySort<PeriodicAudit> sort, int page, int size);
        Task<PeriodicAuditResponse?> GetById(string id);
        Task<PeriodicAuditResponse> Create(CreatePeriodicAuditRequest request);
        //Task<PeriodicAuditResponse> Update(string id, UpdatePeriodicAuditRequest request);
        Task Delete(string id);
    }
}