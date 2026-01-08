using backend.DTOs.Borrow;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IBorrowService
    {
        Task<BorrowVoucher> CreateRequest(BorrowRequestDTO dto);
        Task<BorrowVoucher> ApproveRequest(string voucherId, ApproveBorrowDTO dto);
        Task<BorrowVoucher> ReturnDevice(string voucherId);
        Task<IEnumerable<BorrowVoucher>> GetHistory();
    }
}