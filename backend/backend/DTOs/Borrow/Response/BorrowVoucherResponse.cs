using backend.Enums;
using backend.Models;

namespace backend.DTOs.Borrow.Response
{
    public class BorrowVoucherResponse
    {
        public string BorrowId { get; set; }
        public string BorrowerName { get; set; }
        public BorrowStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        private string? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Note { get; set; }
        public List<BorrowVoucherDetailResponse> Details { get; set; }
    }
}
