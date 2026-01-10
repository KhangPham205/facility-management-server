using backend.Enums;

namespace backend.DTOs.Repair.Request
{
    public class RepairRequestStatusUpdateRequest
    {
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
}
