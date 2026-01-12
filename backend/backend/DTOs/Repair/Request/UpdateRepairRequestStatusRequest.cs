using backend.Enums;

namespace backend.DTOs.Repair.Request
{
    public class UpdateRepairRequestStatusRequest
    {
        public VoucherStatus Status { get; set; }
        public string ApprovedBy { get; set; }
    }
}
