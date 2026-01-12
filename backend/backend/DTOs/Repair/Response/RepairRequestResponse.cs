using backend.Enums;

namespace backend.DTOs.Repair.Response
{
    public class RepairRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<RepairRequestDetailResponse> Details { get; set; }
    }
}
