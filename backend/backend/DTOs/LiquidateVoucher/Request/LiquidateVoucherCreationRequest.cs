using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.LiquidateVoucher.Request
{
    public class LiquidateVoucherCreationRequest
    {
        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public string VoucherDetailId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string? Reason { get; set; }
    }
}