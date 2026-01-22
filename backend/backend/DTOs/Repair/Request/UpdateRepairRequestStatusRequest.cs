using backend.Enums;

namespace backend.DTOs.Repair.Request
{
    public class UpdateRepairRequestStatusRequest
    {
        public VoucherStatus Status { get; set; }
    }
}
