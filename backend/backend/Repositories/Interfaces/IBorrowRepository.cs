using backend.Models.Borrow;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IBorrowRepository
    {
        Task<PageVO<BorrowVoucher>> GetPagedAsync(EntityFilter<BorrowVoucher> filter, EntitySort<BorrowVoucher> sort, int page, int size);
        Task<BorrowVoucher?> GetByIdAsync(string id);
        Task AddAsync(BorrowVoucher voucher);
        Task UpdateAsync(BorrowVoucher voucher);
    }
}
