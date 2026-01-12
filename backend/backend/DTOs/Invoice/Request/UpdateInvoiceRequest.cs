namespace backend.DTOs.Invoice.Request
{
    public class UpdateInvoiceRequest
    {
        public string? InvoiceNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Note { get; set; }
    }
}
