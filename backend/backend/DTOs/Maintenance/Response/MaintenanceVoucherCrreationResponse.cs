using backend.DTOs.VoucherDetail.Request;
using backend.DTOs.VoucherDetail.Response;
using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceVoucherCrreationResponse
    {
        public string MaintenanceRequestId { get; set; } = null!;
        public string? InvoiceId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public MaintenanceStatus Status { get; set; }
        public List<VoucherDetailResponse> VoucherDetails { get; set; } = new List<VoucherDetailResponse>();
    }
}
