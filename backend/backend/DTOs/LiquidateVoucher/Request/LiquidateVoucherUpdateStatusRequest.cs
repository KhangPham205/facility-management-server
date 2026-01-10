using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.LiquidateVoucher.Request
{
    public class LiquidateVoucherUpdateStatusRequest
    {
        [Required]
        public VoucherStatus Status { get; set; }

        [Required]
        public string StatusUpdatedBy { get; set; }

        public string? Reason { get; set; }
    }
}