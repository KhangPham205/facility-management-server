using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Invoice.Request
{
    public class InvoiceCreationRequest
    {
        [Required]
        public string VoucherDetailId { get; set; }

        [Required]
        [Range(0, 3)]
        public FunctionType Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}