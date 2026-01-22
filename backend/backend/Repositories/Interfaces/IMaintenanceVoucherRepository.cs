using backend.Models.Maintenance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IMaintenanceVoucherRepository
    {
        Task<PageVO<MaintenanceVoucher>> GetPagedAsync(EntityFilter<MaintenanceVoucher> filter, EntitySort<MaintenanceVoucher> sort, int page, int size);
        Task<MaintenanceVoucher?> GetByIdAsync(string id);
        Task AddAsync(MaintenanceVoucher voucher);
        Task UpdateAsync(MaintenanceVoucher voucher);
        Task<bool> ExistsByRequestIdAsync(string requestId);
    }
}
