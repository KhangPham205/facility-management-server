using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.Models;
using backend.Models.Audit;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IInventoryAuditService
    {
        Task<PageVO<InventoryAuditResponse>> GetAll(EntityFilter<InventoryAudit> filter, EntitySort<InventoryAudit> sort, int page, int size);
        Task<InventoryAuditResponse?> GetById(string id);
        Task<InventoryAuditResponse> Create(CreateInventoryAuditRequest request);
        //Task<InventoryAuditResponse> Update(string id, UpdateInventoryAuditRequest request);
        Task Delete(string id);
    }
}