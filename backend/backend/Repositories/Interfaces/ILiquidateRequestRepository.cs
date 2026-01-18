using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ILiquidateRequestRepository
    {
        Task<LiquidateRequest?> GetByIdAsync(string id);

        Task<PageVO<LiquidateRequest>> GetPagedAsync(EntityFilter<LiquidateRequest> filter, EntitySort<LiquidateRequest> sort, int pageNumber, int pageSize);
        Task AddAsync(LiquidateRequest importRequest);
        Task UpdateAsync(LiquidateRequest importRequest);
        Task<bool> UpdateStatusAsync(string importRequestId, string approverId, VoucherStatus newStatus);
        Task DeleteAsync(LiquidateRequest importRequest);

        Task<bool> ExistsAsync(string id);
    }
}