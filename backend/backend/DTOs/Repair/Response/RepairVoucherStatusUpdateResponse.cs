using backend.Enums;

namespace backend.DTOs.Repair.Response
{
    public class RepairVoucherStatusUpdateResponse
    {
        public string? StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
