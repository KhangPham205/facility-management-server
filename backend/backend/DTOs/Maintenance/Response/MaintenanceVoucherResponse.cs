using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceVoucherResponse
    {
        public string VoucherId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public MaintenanceStatus Status { get; set; }
    }
}
