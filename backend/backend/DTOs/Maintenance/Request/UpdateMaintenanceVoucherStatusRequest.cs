using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class UpdateMaintenanceVoucherStatusRequest
    {
        public MaintenanceStatus Status { get; set; }
    }
}
