using backend.DTOs.Borrow.Request;
using backend.DTOs.Borrow.Response;
using backend.Models.Borrow;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IBorrowService
    {
        Task<PageVO<BorrowVoucherResponse>> GetAll(EntityFilter<BorrowVoucher> filter, EntitySort<BorrowVoucher> sort, int page, int size);
        Task<BorrowVoucherResponse> GetById(string id);

        Task<BorrowVoucherResponse> Create(string createdBy, CreateBorrowRequest request);
        Task<BorrowVoucherResponse> Approve(string id, ApproveBorrowRequest request);
        Task<BorrowVoucherResponse> Return(string id); // Trả toàn bộ
    }
}