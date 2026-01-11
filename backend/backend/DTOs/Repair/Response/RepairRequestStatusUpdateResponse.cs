using backend.Enums;

namespace backend.DTOs.Repair.Response
{
    public class RepairRequestStatusUpdateResponse
    {
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
}
