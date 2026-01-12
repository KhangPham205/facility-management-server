using backend.Enums;

namespace backend.DTOs.Repair.Request
{
    public class UpdateRepairVoucherStatusRequest
    {
        public MaintenanceStatus Status { get; set; } // Completed / Failed
    }
}
