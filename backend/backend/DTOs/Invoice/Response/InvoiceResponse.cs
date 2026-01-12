namespace backend.DTOs.Invoice.Response
{
    public class InvoiceResponse
    {
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Type { get; set; }
        public decimal TotalAmount { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
    }
}
