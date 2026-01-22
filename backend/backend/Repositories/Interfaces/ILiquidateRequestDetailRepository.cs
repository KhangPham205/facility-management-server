using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ILiquidateRequestDetailRepository
    {
        Task<LiquidateRequestDetail?> GetByIdAsync(string requestId, string equipmentId);

        Task<PageVO<LiquidateRequestDetail>> GetPagedAsync(EntityFilter<LiquidateRequestDetail> filter, EntitySort<LiquidateRequestDetail> sort, int pageNumber, int pageSize);
        Task AddAsync(LiquidateRequestDetail importRequestDetail);
        //Task UpdateAsync(LiquidateRequestDetail importRequestDetail);
        //Task<bool> UpdateStatusAsync(string importRequestDetailId, string approverId, VoucherStatus newStatus);
        Task DeleteAsync(LiquidateRequestDetail importRequestDetail);

        Task<bool> ExistsAsync(string id);
    }
}