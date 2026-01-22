using backend.Enums;

namespace backend.DTOs.Transfer.Request
{
    public class ApproveTransferRequest
    {
        public VoucherStatus Status { get; set; }
    }
}
