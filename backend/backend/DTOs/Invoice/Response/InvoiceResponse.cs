using backend.Models.Finance;
using DTOs.ExternalUnit.Response;

namespace backend.DTOs.Invoice.Response
{
    public class InvoiceResponse
    {
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ExternalUnitResponse Unit { get; set; }
    }
}
