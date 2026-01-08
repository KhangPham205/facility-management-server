namespace backend.DTOs.Borrow
{
    public class ApproveBorrowDTO
    {
        public string ApproverId { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
    }
}
