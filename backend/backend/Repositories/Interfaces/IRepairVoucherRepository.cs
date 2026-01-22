using backend.Models.Repair;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IRepairVoucherRepository
    {
        Task<PageVO<RepairVoucher>> GetPagedAsync(EntityFilter<RepairVoucher> filter, EntitySort<RepairVoucher> sort, int page, int size);
        Task<RepairVoucher?> GetByIdAsync(string id);
        Task AddAsync(RepairVoucher voucher);
        Task UpdateAsync(RepairVoucher voucher);
        Task<bool> ExistsByRequestIdAsync(string requestId);
    }
}
