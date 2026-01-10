using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Invoice.Request
{
    public class InvoiceUpdateRequest
    {
        [Required]
        public string VoucherDetailId { get; set; }

        [Required]
        public FunctionType Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}