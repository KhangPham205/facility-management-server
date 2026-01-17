using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Liquidate.Request
{
    public class UpdateLiquidateRequestStatusRequest
    {
        [Required]
        public VoucherStatus Status { get; set; }

        [Required]
        public string ApprovedBy { get; set; }
    }
}
