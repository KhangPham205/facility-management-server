namespace backend.DTOs.Borrow.Request
{
    public class UpdateBorrowRequest
    {
        public string BorrowId { get; set; } // Mã mượn
        public DateTime ReturnDate { get; set; } // Ngày thực tế trả

    }
}
