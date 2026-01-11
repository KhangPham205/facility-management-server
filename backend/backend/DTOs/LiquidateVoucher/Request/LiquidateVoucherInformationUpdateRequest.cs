using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.LiquidateVoucher.Request
{
    public class LiquidateVoucherInformationUpdateRequest
    {
        [Required(ErrorMessage = "InvoiceId không được để trống")]
        public string InvoiceId { get; set; }

        [Required(ErrorMessage = "VoucherDetailId không được để trống")]
        public string VoucherDetailId { get; set; }

        [StringLength(500, ErrorMessage = "Lý do không được vượt quá 500 ký tự")]
        public string? Reason { get; set; }
    }
}