using backend.Enums;

namespace backend.DTOs.Repair.Response
{
    public class RepairVoucherResponse
    {
        public string VoucherId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public MaintenanceStatus Status { get; set; }
        public string ProviderName { get; set; }
        public List<RepairRequestDetailResponse> Details { get; set; }
    }
}
