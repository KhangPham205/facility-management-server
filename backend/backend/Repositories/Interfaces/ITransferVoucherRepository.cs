using backend.Models.Transfer;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface ITransferVoucherRepository
    {
        Task<PageVO<TransferVoucher>> GetPagedAsync(EntityFilter<TransferVoucher> filter, EntitySort<TransferVoucher> sort, int page, int size);
        Task<TransferVoucher?> GetByIdAsync(string id);
        Task AddAsync(TransferVoucher voucher);
        Task<bool> ExistsByRequestIdAsync(string requestId);
    }
}
