using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class UpdateMaintenanceRequestStatusRequest
    {
        public VoucherStatus Status { get; set; }
        public string ApprovedBy { get; set; }
    }
}
