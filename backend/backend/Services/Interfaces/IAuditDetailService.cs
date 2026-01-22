using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Models;
using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IAuditDetailService
    {
        Task<PageVO<AuditDetailResponse>> GetAll(EntityFilter<AuditDetail> filter, EntitySort<AuditDetail> sort, int page, int size, string auditId);
        Task<AuditDetailResponse?> GetById(string auditId, string equipmentId);
        Task<AuditDetailResponse> Create(string auditId, string equipmentId, CreateAuditDetailRequest request);
        Task<AuditDetailResponse> Update(string auditId, string equipmentId, UpdateAuditDetailRequest request);
        Task Delete(string auditId, string equipmentId);
    }
}