using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceRequestStatusUpdateResponse
    {
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
}
