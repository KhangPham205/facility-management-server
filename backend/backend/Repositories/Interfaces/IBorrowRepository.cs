using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IBorrowRepository
    {
        Task<BorrowVoucher?> GetByIdAsync(string id);
        Task<IEnumerable<BorrowVoucher>> GetAllAsync();
        Task<IEnumerable<BorrowVoucher>> GetByUserIdAsync(string userId);
        Task AddAsync(BorrowVoucher voucher);
        Task UpdateAsync(BorrowVoucher voucher);
    }
}