using backend.Enums;

namespace backend.DTOs.Repair.Request
{
    public class RepairVoucherStatusUpdateRequest
    {
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
