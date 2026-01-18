using backend.Enums;

namespace backend.DTOs.Borrow.Request
{
    public class ApproveBorrowRequest
    {
        public BorrowStatus Status { get; set; }
    }
}
