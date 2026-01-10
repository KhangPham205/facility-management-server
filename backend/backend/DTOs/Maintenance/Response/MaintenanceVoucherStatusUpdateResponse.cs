using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceVoucherStatusUpdateResponse
    {
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
