namespace backend.DTOs.Transfer
{
    public class ApproveTransferDTO
    {
        public string ApproverId { get; set; } // ID người duyệt
        public bool IsApproved { get; set; }   // True: Duyệt, False: Từ chối
        public string? Note { get; set; }
    }
}
