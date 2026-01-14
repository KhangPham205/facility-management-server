using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Invoice.Request
{
    public class CreateInvoiceRequest
    {
        [Required(ErrorMessage = "Số hóa đơn là bắt buộc.")]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public FunctionType Type { get; set; } // Import, Maintenance...

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public string UnitId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string? Note { get; set; }
    }
}
