using backend.DTOs.VoucherDetail.Request;
using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceRequestCreationRequest
    {
        public string CreatedBy { get; set; } = null!; // Mã người yêu cầu
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? Reason { get; set; }
        public List<VoucherDetailCreationRequest> VoucherDetails { get; set; } = new List<VoucherDetailCreationRequest>();
    }
}
