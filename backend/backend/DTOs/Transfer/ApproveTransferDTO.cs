namespace backend.DTOs.Transfer
{
    public class ApproveTransferDTO
    {
        public string ApproverId { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
    }
}
