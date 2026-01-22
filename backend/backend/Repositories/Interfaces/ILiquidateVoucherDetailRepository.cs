using backend.Enums;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ILiquidateVoucherDetailRepository
    {
        Task<LiquidateVoucherDetail?> GetByIdAsync(string voucherId, string equipmentId);

        Task<PageVO<LiquidateVoucherDetail>> GetPagedAsync(EntityFilter<LiquidateVoucherDetail> filter, EntitySort<LiquidateVoucherDetail> sort, int pageNumber, int pageSize);
        Task AddAsync(LiquidateVoucherDetail importVoucherDetail);
        //Task UpdateAsync(LiquidateVoucherDetail importVoucherDetail);
        //Task<bool> UpdateStatusAsync(string importVoucherDetailId, string approverId, VoucherStatus newStatus);
        Task DeleteAsync(LiquidateVoucherDetail importVoucherDetail);

        Task<bool> ExistsAsync(string id);
    }
}