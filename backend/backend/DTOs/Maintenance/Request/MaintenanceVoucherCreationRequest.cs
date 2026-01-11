using backend.DTOs.VoucherDetail.Request;
using backend.Enums;

namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceVoucherCreationRequest
    {
        public string MaintenanceRequestId { get; set; } = null!;
        public string? InvoiceId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public MaintenanceStatus Status { get; set; }
        public List<VoucherDetailCreationRequest> VoucherDetails { get; set; } = new List<VoucherDetailCreationRequest>();
    }
}
