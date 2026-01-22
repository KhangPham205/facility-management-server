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
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        public string UnitId { get; set; }
    }
}
