using backend.Enums;

namespace backend.DTOs.Transfer
{
    public class ApproveTransferDTO
    {
        public string ApproverId { get; set; }
        public VoucherStatus Status { get; set; }
        public string? Note { get; set; }
    }
}
