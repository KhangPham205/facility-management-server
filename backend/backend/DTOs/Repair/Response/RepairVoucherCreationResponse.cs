using backend.DTOs.VoucherDetail.Response;
using backend.Enums;

namespace backend.DTOs.Repair.Response
{
    public class RepairVoucherCreationResponse
    {
        public string RepairRequestId { get; set; } = null!;
        public string? InvoiceId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public MaintenanceStatus Status { get; set; }
        public List<VoucherDetailResponse> VoucherDetails { get; set; } = new List<VoucherDetailResponse>();
    }
}
