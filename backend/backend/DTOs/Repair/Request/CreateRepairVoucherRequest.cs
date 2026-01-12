namespace backend.DTOs.Repair.Request
{
    public class CreateRepairVoucherRequest
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }

        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ProviderId { get; set; }
    }
}
