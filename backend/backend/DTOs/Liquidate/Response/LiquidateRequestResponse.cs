using backend.Enums;

namespace backend.DTOs.Liquidate.Response
{
    public class LiquidateRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<LiquidateRequestDetailResponse> Details { get; set; }
    }
}
