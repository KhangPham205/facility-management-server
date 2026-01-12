using backend.Enums;

namespace backend.DTOs.Liquidate.Request
{
    public class UpdateLiquidateRequestStatusRequest
    {
        public VoucherStatus Status { get; set; }
        public string ApprovedBy { get; set; }
    }
}
