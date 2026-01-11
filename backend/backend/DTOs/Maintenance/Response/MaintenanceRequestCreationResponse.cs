using backend.DTOs.VoucherDetail.Response;
using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceRequestCreationResponse
    {
        public string CreatedBy { get; set; } = null!; // Mã người yêu cầu
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ReqMaintenanceStatus Status { get; set; } = ReqMaintenanceStatus.Pending;
        public string? Reason { get; set; }
        public List<VoucherDetailResponse> VoucherDetails { get; set; } = new List<VoucherDetailResponse>();
    }
}
