namespace backend.DTOs.Borrow.Request
{
    public class CreateBorrowRequest
    {
        public string BorrowerId { get; set; } // Người mượn
        public string? Note { get; set; }
        public DateTime? ReturnDate { get; set; } // Ngày dự kiến trả
        public List<BorrowDetailDto> Details { get; set; }
    }
}
