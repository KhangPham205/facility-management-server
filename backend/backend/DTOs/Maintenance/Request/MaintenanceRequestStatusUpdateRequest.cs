using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceRequestStatusUpdateRequest
    {
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
}
