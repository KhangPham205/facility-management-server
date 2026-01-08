namespace backend.DTOs.Borrow
{
    public class BorrowRequestDTO
    {
        public string BorrowerId { get; set; }
        public string? Purpose { get; set; }
        public List<BorrowItemDTO> Items { get; set; } // Danh sách thiết bị mượn
    }
}
