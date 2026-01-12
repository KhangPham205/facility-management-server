namespace backend.DTOs.Maintenance.Request
{
    public class CreateMaintenanceVoucherRequest
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }

        // Thông tin hóa đơn/chi phí dự kiến
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ProviderId { get; set; } // Đơn vị nào thực hiện?
    }
}
