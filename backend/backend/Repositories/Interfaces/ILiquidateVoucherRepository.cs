using backend.Enums;
using backend.Models;
using backend.Models.Liquidate;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ILiquidateVoucherRepository
    {
        Task<LiquidateVoucher?> GetByIdAsync(string id);

        Task<PageVO<LiquidateVoucher>> GetPagedAsync(EntityFilter<LiquidateVoucher> filter, EntitySort<LiquidateVoucher> sort, int pageNumber, int pageSize);
        Task AddAsync(LiquidateVoucher liquidateVoucher);
        Task UpdateAsync(LiquidateVoucher liquidateVoucher);
        Task DeleteAsync(LiquidateVoucher liquidateVoucher);

        Task<bool> ExistsAsync(string id);
    }
}