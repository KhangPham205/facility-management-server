using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Invoice.Response
{
    public class Invoice
    {
        public string InvoiceId { get; set; }

        public string VoucherDetailId { get; set; }

        public FunctionType Type { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // thuộc tính ngoài model
    }
}