using backend.Models;
using backend.vo;

namespace backend.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        Task<TransferVoucher?> GetByIdAsync(string id);
        Task<PageVO<TransferVoucher>> GetAllPagedAsync(int page, int size);
        Task AddAsync(TransferVoucher voucher);
        Task UpdateAsync(TransferVoucher voucher);
        Task DeleteAsync(TransferVoucher voucher);
    }
}
