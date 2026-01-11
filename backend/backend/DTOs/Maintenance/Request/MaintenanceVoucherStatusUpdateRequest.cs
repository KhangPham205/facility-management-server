using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceVoucherStatusUpdateRequest
    {
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
